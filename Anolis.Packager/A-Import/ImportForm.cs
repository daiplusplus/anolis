using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Core.Packages;
using Anolis.Core.Packages.Operations;
using System.IO;

namespace Anolis.Packager {
	
	public partial class ImportForm : Form {
		
		public ImportForm() {
			
			InitializeComponent();
			
			__browse.Click += new EventHandler(__browse_Click);
			
			__import.Click += new EventHandler(__import_Click);
			
		}
		
		private void __browse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__root.Text = __fbd.SelectedPath;
			}
			
		}
		
		private void __import_Click(object sender, EventArgs e) {
			
			BuildPackage();
			
		}
		
		private void BuildPackage() {
			
			DirectoryInfo root = new DirectoryInfo( __root.Text );
			if( !root.Exists ) {
				
				MessageBox.Show(this, "The specified directory, " + root.Name + ", does not exist. The Import process cannot continue", "Anolis Packager", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}
			
			String packageXmlFileName = Path.Combine( root.FullName, "Package.xml" );
			
			if( File.Exists( packageXmlFileName ) ) {
				
				DialogResult r = MessageBox.Show(this, "A Package.xml file already exists in the selected folder. It will be overwritten. Do you want to continue?", "Anolis Packager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if( r != DialogResult.Yes ) return;
				
			}
			
			_ignoredFiles.Clear();
			
			Package package = new Package(root);
			
			ProcessDirectory( package.RootGroup, root );
			
			package.Write( packageXmlFileName );
			
			using(StreamWriter wtr = new StreamWriter( packageXmlFileName + ".ignored.txt")) {
				
				foreach(FileInfo ignored in _ignoredFiles) {
					
					wtr.WriteLine( ignored.FullName );
				}
				
			}
			
		}
		
		private List<FileInfo> _ignoredFiles = new List<FileInfo>();
		
		private void ProcessDirectory(Group group, DirectoryInfo directory) {
			
			// only act if directory's name matches a filename
			
			String relativeFn = directory.FullName.Substring(group.Package.RootDirectory.FullName.Length);
			
			if( directory.Name.IndexOf('.') > -1 ) {
				
				ResPatchOperation op = new ResPatchOperation(group, relativeFn.Substring(1) );
				
				FileInfo[] files = directory.GetFiles();
				Array.Sort( files, new Comparison<FileInfo>( CompareFiles ) );
				
				foreach(FileInfo file in files) {
					
					String type;
					
					String ext = file.Extension.ToUpperInvariant();
					switch(ext) {
						case ".ICO":
							type = "icon";
							break;
							
						case ".BMP":
							type = "bitmap";
							break;
							
						case ".AVI":
						case ".PNG":
						case ".GIF":
						case ".JPG":
						case ".JPEG":
							type = ext.Substring(1);
							break;
							
						case ".CUR":
							type = "cursor";
							break;
							
						default:
							type = null;
							_ignoredFiles.Add( file );
							
							break;
							
					}
					
					if(type != null) {
						
						PatchResource patch = new PatchResource();
						patch.Type = type;
						patch.Name = Path.GetFileNameWithoutExtension( file.FullName );
						patch.Source = Path.Combine( relativeFn, file.Name ).Substring(1);
						
						op.ResourceSet.Resources.Add( patch );
					}
					
				}
				
				group.Operations.Add( op );
				
			}//if
			
			foreach(DirectoryInfo child in directory.GetDirectories()) {
				
				ProcessDirectory( group, child );
			}
			
			// also, build a list of ignored directories and files
			
		}
		
		private Int32 CompareFiles(FileInfo x, FileInfo y) {
			
			Int32 extC = x.Extension.CompareTo( y.Extension );
			if( extC != 0 ) return extC;
			
			Int32 xN, yN;
			if( Int32.TryParse( Path.GetFileNameWithoutExtension( x.Name ), out xN ) && Int32.TryParse( Path.GetFileNameWithoutExtension( y.Name ), out yN ) ) {
				
				return xN.CompareTo( yN );
			}
			
			return x.Name.CompareTo( y.Name );
			
		}
		
	}
}
