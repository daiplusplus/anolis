using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Anolis.Core.Packages;
using System.IO;

namespace Anolis.Gui.Pages {
	
	public partial class ExtractingPage : BaseInteriorPage {
		
		public ExtractingPage() {
			InitializeComponent();
			
			this.Load += new EventHandler(Extracting_Load);
			
			WizardForm.EnablePrev = false;
			WizardForm.EnableNext = false;
		}
		
		private void Extracting_Load(object sender, EventArgs e) {
			
			// Begin extraction
			
			PackageInfo.Archive.PackageProgressEvent += new EventHandler<PackageProgressEventArgs>(Archive_PackageProgressEvent);
			PackageInfo.Archive.BeginPackageExtract( new Action<String>( Archive_Completed ) );
			
			
		}
		
		private void Archive_PackageProgressEvent(object sender, PackageProgressEventArgs e) {
			
			__statusLabel.Text = e.Message;
			__progress.Value = e.Percentage;
		}
		
		private void Archive_Completed(String destDir) {
			
			__statusLabel.Text = "Instantiating Package";
			
			PackageInfo.Package = Package.FromFile( Path.Combine( destDir, "Package.xml" ) );
			
			WizardForm.LoadPage( Program.PageIEModifyPackage );
			
		}
		
		public override W3b.Wizards.WizardPage PrevPage {
			get { return null; }
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return null; }
		}
		
	}
}
