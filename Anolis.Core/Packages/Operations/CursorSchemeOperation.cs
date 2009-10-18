using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;

using Microsoft.Win32;

using P = System.IO.Path;


namespace Anolis.Core.Packages.Operations {
	
	public class CursorSchemeOperation : Operation {
		
		private List<CursorScheme> _schemes;
		
		public CursorSchemeOperation(Group parent, XmlElement element) :  base(parent, element) {
			
			_schemes = new List<CursorScheme>();
			
			CursorScheme scheme = new CursorScheme(parent.Package.RootDirectory, element );
			
			_schemes.Add( scheme );
		}
		
		public CursorSchemeOperation(Group parent) : base(parent) {
			_schemes = new List<CursorScheme>();
		}
		
		public override String OperationName {
			get { return "Cursor scheme"; }
		}
		
		public override String ToString() {
			return OperationName + " : " + this.Name;
		}
		
		public override Boolean Merge(Operation operation) {
			
			CursorSchemeOperation other = operation as CursorSchemeOperation;
			if(other == null) return false;
			
			_schemes.AddRange( other._schemes );
			
			return true;
		}
		
		public override void Execute() {
			
			Backup( Package.ExecutionInfo.BackupGroup );
			
			for(int i=0;i<_schemes.Count;i++) {
				
				CursorScheme scheme = _schemes[i];
				
				scheme.Install();
				scheme.Register();
				
				BackupDeleteScheme( Package.ExecutionInfo.BackupGroup, scheme );
				
				if(i == _schemes.Count - 1)
					scheme.MakeActive();
				
			}
			
			
		}
		
		private void Backup(Group backupGroup) {
			
			if( backupGroup == null ) return;
			
			// save the current scheme config
			RegistryKey currentCursorsKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors", false);
			
			CursorScheme currentScheme = new CursorScheme( currentCursorsKey );
			
			CursorSchemeOperation op = new CursorSchemeOperation(backupGroup);
			op._schemes.Add( currentScheme );
			
			backupGroup.Operations.Add( op );
		}
		
		private void BackupDeleteScheme(Group backupGroup, CursorScheme scheme) {
			
			if( backupGroup == null ) return;
			
			RegistryOperation deleteSchemeOp = new RegistryOperation(backupGroup);
			deleteSchemeOp.RegKey   = @"HKEY_CURRENT_USER\Control Panel\Cursors\Schemes";
			deleteSchemeOp.RegName  = scheme.SchemeName;
			deleteSchemeOp.RegValue = "###ANOLISREMOVE####";
			deleteSchemeOp.RegKind  = RegistryValueKind.String;
			
			backupGroup.Operations.Add( deleteSchemeOp );
			
			foreach(CursorEntry entry in scheme.Cursors) {
				
				if( File.Exists( entry.CursorFilename ) ) {
					
					FileOperation op = new FileOperation(backupGroup, entry.CursorFilename);
					backupGroup.Operations.Add( op );
				}
				
			}
			
		}
		
		public override void Write(XmlElement parent) {
			
			foreach(CursorScheme scheme in _schemes) {
				
				XmlElement element = CreateElement(parent, "cursorScheme", 
					"schemeName", scheme.SchemeName
				);
				
				foreach(CursorEntry entry in scheme.Cursors) {
					
					if(entry == null) return;
					
					XmlElement entryElement = CreateElement(element, "cursor");
					AddAttribute(entryElement, "type", entry.CursorType.ToString());
					AddAttribute(entryElement, "src" , entry.CursorFilename);
					
				}
				
			}
			
		}
		
		private class CursorScheme {
			
			public String        SchemeName;
			public CursorEntry[] Cursors = new CursorEntry[15];
			
			public CursorScheme(DirectoryInfo root, XmlElement element) {
				
				SchemeName = element.GetAttribute("schemeName");
				
				foreach(XmlNode node in element.ChildNodes) {
					
					if(node.NodeType != XmlNodeType.Element) continue;
					
					XmlElement child = node as XmlElement;
					
					//////////////////////////////////////////////////
					
					CursorType type = (CursorType)Enum.Parse(typeof(CursorType), child.GetAttribute("type") );
					String     path = PackageUtility.ResolvePath( child.GetAttribute("src"), root.FullName);
					
					int idx = (int)type;
					
					this.Cursors[ idx ] = new CursorEntry() { CursorType = type, CursorFilename = path };
				}
				
				Array.Sort( Cursors );
				
			}
			
			public CursorScheme(RegistryKey key) {
				
				SchemeName = (String)key.GetValue(null);
				
				String[] vnames = key.GetValueNames();
				
				String[] typeNames = Enum.GetNames( typeof(CursorType) );
				
				foreach(String vname in vnames) {
					
					if( !typeNames.Contains( vname ) ) continue;
					
					String path = (String)key.GetValue(vname);
					
					CursorType type = (CursorType)Enum.Parse( typeof(CursorType), vname );
					
					int i = (int)type;
					if( i > Cursors.Length ) continue;
					
					Cursors[i] = new CursorEntry() { CursorType = type, CursorFilename = path };
				}
				
			}
			
			public CursorScheme(String name, String schemeLine) {
				
				SchemeName = name;
				
				String[] paths = schemeLine.Split(',');
				for(int i=0;(i<paths.Length || i<Cursors.Length);i++) {
					
					Cursors[i] = new CursorEntry() {
						CursorFilename = paths[i],
						CursorType     = (CursorType)i
					};
					
				}
				
			}
			
			public void Install() {
				
				// initially the CursorFilename refer to the local path
				// this method copies the files over to the Cursors directory and updates the Filenames
				
				String cursorsDirPath = PackageUtility.ResolvePath(@"%windir%\Cursors");
				
				DirectoryInfo cursorsDir = new DirectoryInfo( cursorsDirPath );
				DirectoryInfo schemeDir  = cursorsDir.CreateSubdirectory( SchemeName );
				
				foreach(CursorEntry cursor in Cursors) {
					if(cursor == null) continue;
					
					String src  = cursor.CursorFilename;
					
					String dest = P.Combine( schemeDir.FullName, P.GetFileName( cursor.CursorFilename ) );
					
					File.Copy( src, dest );
					
					cursor.CursorFilename = dest;
				}
				
			}
			
			public void Register() {
				// registers this scheme in the currentuser reg hive
				
				RegistryKey schemesKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors\Schemes");
				schemesKey.SetValue( SchemeName, CreateLine(), RegistryValueKind.String );
				schemesKey.Close();
				
			}
			
			private String CreateLine() {
				
				StringBuilder sb = new StringBuilder();
				
				// the line must be in order
				Array.Sort( Cursors );
				
				Int32 lastType = -1;
				for(int i=0;i<Cursors.Length;i++) {
					
					CursorEntry cursor = Cursors[i];
					if( cursor != null ) {
						
						int type = (int)cursor.CursorType;
						
						if( type != lastType + 1 ) {
							int difference = type - (lastType + 1);
							for(int j=0;i<difference;j++) sb.Append(',');
						}
						
						////////////////////////////////////////////
						
						sb.Append( cursor.CursorFilename );
					}
					
					if( i<Cursors.Length-1) sb.Append(',');
				}
				
				return sb.ToString();
			}
			
			public void MakeActive() {
				
				// delete all current values
				RegistryKey activeCursorsKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors", true);
				
				String[] names = activeCursorsKey.GetValueNames();
				foreach(String name in names) activeCursorsKey.DeleteValue(name, false);
				
				activeCursorsKey.SetValue(null, SchemeName, RegistryValueKind.String);
				activeCursorsKey.SetValue("Scheme Source", 1, RegistryValueKind.DWord); // 0 = Windows Default, 1 = User Scheme, 2 = System Scheme
				
				foreach(CursorEntry cursor in Cursors) {
					
					if(cursor == null) continue;
					
					String valName = cursor.CursorType.ToString();
					String valValu = cursor.CursorFilename;
					
					activeCursorsKey.SetValue(valName, valValu, RegistryValueKind.String);
				}
				
				activeCursorsKey.Close();
				
			}
			
			
		}
		
		private class CursorEntry : IComparable<CursorEntry> {
			public CursorType CursorType;
			public String     CursorFilename;
			
			public Int32 CompareTo(CursorEntry other) {
				if( other == null ) return 1;
				return CursorType.CompareTo( other.CursorType );
			}
		}
		
		public enum CursorType {
			Arrow       = 0,
			Help        = 1,
			AppStarting = 2,
			Wait        = 3,
			Crosshair   = 4,
			IBeam       = 5,
			NWPen       = 6,
			No          = 7,
			SizeNS      = 8,
			SizeWE      = 9,
			SizeNWSE    = 10,
			SizeNESW    = 11,
			SizeAll     = 12,
			UpArrow     = 13,
			Hand        = 14
		}
		
	}
	
}
