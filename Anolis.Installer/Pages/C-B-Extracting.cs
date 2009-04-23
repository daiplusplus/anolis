using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Anolis.Core.Packages;
using System.IO;
using W3b.Wizards.WindowsForms;
namespace Anolis.Installer.Pages {
	
	public partial class ExtractingPage : BaseInteriorPage {
		
		public ExtractingPage() {
			InitializeComponent();
			
			this.Load += new EventHandler(Extracting_Load);
			this.PageLoad += new EventHandler(ExtractingPage_PageLoad);
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "C_B"; } }
		
		private void ExtractingPage_PageLoad(object sender, EventArgs e) {
			
			
		}
		
		private void Extracting_Load(object sender, EventArgs e) {
			
			WizardForm.EnablePrev = false;
			WizardForm.EnableNext = false;
			
			// Begin extraction
			if( PackageInfo.Source == PackageSource.File ) {
				
				InstantiatePackage( PackageInfo.SourcePath );
				
			} else {
				
				PackageInfo.Archive.PackageProgressEvent += new EventHandler<PackageProgressEventArgs>(Archive_PackageProgressEvent);
				PackageInfo.Archive.BeginPackageExtract( new Action<String>( Archive_Completed ) );
				
			}
			
		}
		
		private void Archive_PackageProgressEvent(object sender, PackageProgressEventArgs e) {
			
			this.Invoke( new MethodInvoker( delegate() {
				
				__statusLbl.Text = e.Message;
				__progress.Value = e.Percentage;
				
			} ) );
			
		}
		
		private void Archive_Completed(String destDir) {
			
			this.Invoke( new MethodInvoker( delegate() {
				
				if( destDir != null ) {
					
					__statusLbl.Text = InstallerResources.GetString("C_B_instantiating");
					
					InstantiatePackage( destDir );
					
				} else {
					
					// the previous PackageProgressEvent method call will contain the error string, so don't set anything and display a message to the user
					MessageBox.Show(this, InstallerResources.GetString("C_B_error"), "Anolis", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					
				}
				
			} ) );
			
		}
		
		private void InstantiatePackage(String path) {
			
			try {
				
				PackageInfo.Package = path.EndsWith("/") || path.EndsWith("\\") ?
					Package.FromDirectory( path ) : Package.FromFile( path );
				
			} catch( PackageValidationException pve ) {
				
				__packageMessages.Visible = true;
				
				StringBuilder sb = new StringBuilder();
				sb.AppendLine( pve.Message );
				foreach(System.Xml.Schema.ValidationEventArgs ve in pve.ValidationErrors) {
					sb.Append( ve.Severity );
					sb.Append(" ");
					sb.Append( ve.Message );
					sb.Append(" ");
					if( ve.Exception != null ) {
						sb.Append( ve.Exception.Message );
						sb.Append(" (");
						sb.Append( ve.Exception.LineNumber );
						sb.Append(", ");
						sb.Append( ve.Exception.LinePosition );
						sb.Append(")");
					}
					
					sb.AppendLine();
				}
				
				__packageMessages.Text = sb.ToString();
				
				return;
				
				
			} catch(PackageException pe ) {
				
				__packageMessages.Visible = true;
				
				__packageMessages.Text = pe.GetType().Name + " " + pe.Message;
				
				return;
				
			} 
//#if DEBUG
//			catch(Exception ex) {
//				
//				String x = ex.Message;
//				
//				return;
//				
//			}
//#endif
			
			// verify all the referenced files are there
			
			//PackageInfo.Package.
			
			WizardForm.LoadPage( Program.PageCCUpdatePackage );
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
	}
}
