using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Anolis.Core.Packages;
using System.IO;
using W3b.Wizards;
namespace Anolis.Gui.Pages {
	
	public partial class ExtractingPage : BaseInteriorPage {
		
		public ExtractingPage() {
			InitializeComponent();
			
			this.Load += new EventHandler(Extracting_Load);
		}
		
		private void Extracting_Load(object sender, EventArgs e) {
			
			WizardForm.EnablePrev = false;
			WizardForm.EnableNext = false;
			
			// Begin extraction
			
			PackageInfo.Archive.PackageProgressEvent += new EventHandler<PackageProgressEventArgs>(Archive_PackageProgressEvent);
			PackageInfo.Archive.BeginPackageExtract( new Action<String>( Archive_Completed ) );
			
			
		}
		
		private void Archive_PackageProgressEvent(object sender, PackageProgressEventArgs e) {
			
			this.Invoke( new MethodInvoker( delegate() {
				
				__statusLabel.Text = e.Message;
				__progress.Value = e.Percentage;
				
			} ) );
			
		}
		
		private void Archive_Completed(String destDir) {
			
			this.Invoke( new MethodInvoker( delegate() {
				
				if( destDir != null ) {
					
					__statusLabel.Text = "Instantiating Package";
					
					PackageInfo.Package = Package.FromFile( Path.Combine( destDir, "Package.xml" ) );
					
					WizardForm.LoadPage( Program.PageIEModifyPackage );
					
				} else {
					
					__statusLabel.Text = "Error during extraction";
					
				}
				
			} ) );
			
			
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
	}
}
