using System;
using System.Globalization;
using System.IO;
using System.Xml;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Core.Utility;

using P = System.IO.Path;

using System.Collections.Generic;

namespace Anolis.Core.Packages.Operations {
	
	public class PatchOperation : Operation {
		
		private String _saveTo;
		
		public PatchOperation(Package package, XmlElement operationElement) : base(package, operationElement) {
			
			Resources = new List<PatchResource>();
			
			base.Path = operationElement.GetAttribute("path");
			
			_saveTo   = operationElement.GetAttribute("saveTo");
			if(_saveTo.Length > 0)
				_saveTo = PackageUtility.ResolvePath( _saveTo );
			else _saveTo = null;
			
			foreach(XmlNode node in operationElement.ChildNodes) {
				
				if(node.NodeType != XmlNodeType.Element) continue;
				
				XmlElement child = node as XmlElement;
				
				if( child.Name != "res" ) continue;
				
				PatchResource res = new PatchResource() {
					Type = child.GetAttribute("type"),
					Name = child.GetAttribute("name"),
					Lang = child.GetAttribute("lang"),
					File = child.GetAttribute("src"),
					Add  = child.GetAttribute("add") == "true"
				};
				
				res.File = P.Combine( package.RootDirectory.FullName, res.File );
				
				Resources.Add( res );
			}
			
		}
		
		public override void Execute() {
			
			if( !File.Exists( Path ) ) {
				
				Package.Log.Add( new LogItem(LogSeverity.Error, "Source File not found: " + Path) );
				return;
			}
			
			// copy the file first
			
			String workOnThis;
			
			if( _saveTo != null ) {
				
				workOnThis = _saveTo;
				
			} else {
				
				workOnThis = Path + ".anofp"; // "Anolis File Pending"
			}
			
			if(File.Exists( workOnThis )) Package.Log.Add( LogSeverity.Warning, "Overwritten *.anofp: " + workOnThis);
			File.Copy( Path, workOnThis, true );
			
			// TODO: Oh, I need to copy it to the uninstallation directory too
			
			// for now, use lazy-load under all circumstances. In future analyse the Resources list to see if it's necessary or not
			
			try {
				
				using(ResourceSource source = ResourceSource.Open(workOnThis, false, ResourceSourceLoadMode.LazyLoadData)) {
					
					foreach(PatchResource res in Resources) {
						
						if( !File.Exists( res.File ) ) {
							Package.Log.Add( LogSeverity.Error, "Data File not found: " + res.File );
							continue;
						}
						
						ResourceTypeIdentifier typeId = ResourceTypeIdentifier.CreateFromString( res.Type, true );
						ResourceIdentifier     nameId = ResourceIdentifier.CreateFromString( res.Name );
						UInt16                 langId = String.IsNullOrEmpty( res.Lang ) ? UInt16.MaxValue : UInt16.Parse( res.Lang, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture );
						
						if(	langId == UInt16.MaxValue ) { // if the lang="" attribute was not specified
							
							ResourceName name = source.GetName(typeId, nameId);
							
							if(name == null) { // if the specified name does not exist
								
								if( res.Add ) {
									
									UInt16 sysLang = GetSystemLangId();
									
									ResourceData data = ResourceData.FromFileToAdd( res.File, sysLang, source );
									source.Add( typeId, nameId, sysLang, data );
									
								} else {
									
									Package.Log.Add( LogSeverity.Warning, "Name not found: " + source.Name + '\\' + typeId.ToString() + '\\' + nameId.FriendlyName );
								}
								
								
							} else {
								
								foreach(ResourceLang lang in name.Langs) {
									
									ResourceData data = ResourceData.FromFileToAdd( res.File, lang.LanguageId, source );
									lang.SwapData( data );
									
								}
								
							}
							
						} else { // if the lang="" attribute was specified
							
							ResourceLang lang = source.GetLang( typeId, nameId, langId );
							if(lang == null) {
								
								ResourceData data = ResourceData.FromFileToAdd( res.File, langId, source );
								source.Add( typeId, nameId, langId, data );
								
							} else {
								
								ResourceData data = ResourceData.FromFileToUpdate( res.File, lang );
								lang.SwapData( data );
							}
							
						}
						
					}
					
					source.CommitChanges();
				}
				
				PackageUtility.AddPfroEntry( workOnThis, Path );
				
			} catch(Exception aex) {
				
				Package.Log.Add( LogSeverity.Error, "Patch Exception: " + aex.Message );
				
				if( File.Exists( workOnThis ) ) File.Delete( workOnThis );
				
				throw;
			}
			
		}
		
		private static UInt16 GetSystemLangId() {
			
			return PackageUtility.GetSystemInstallLanguage();
		}
		
		protected override String OperationName {
			get { return "Res patch"; }
		}
		
		public override Boolean Merge(Operation operation) {
			
			// TODO check condition first
			return false;
		}
		
		internal List<PatchResource> Resources { get; private set; }
		
	}
	
	internal class PatchResource {
		
		public String Type { get; set; }
		
		public String Name { get; set; }
		
		public String Lang { get; set; }
		
		public String File { get; set; }
		
		/// <summary>True if the resource is to be added as opposed to updated.</summary>
		public Boolean Add { get; set; }
	}
	
}
