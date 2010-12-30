using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Anolis.Packages;
using W3b.Wizards.WindowsForms;

namespace Anolis.Installer.Pages {
	
	public partial class ExtractingPage : BaseInteriorPage {
		
		public ExtractingPage() {
			InitializeComponent();
			
			this.PageLoad += new EventHandler(ExtractingPage_PageLoad);
		}
		
		protected override String LocalizePrefix { get { return "C_B"; } }
		
		protected override void Localize() {
			base.Localize();
			
			if( InstallerResources.IsCustomized ) {
				
				PageTitle    = InstallerResources.GetString("C_B_Title_Cus"   , InstallerResources.CustomizedSettings.InstallerName);
				PageSubtitle = InstallerResources.GetString("C_B_Subtitle_Cus", InstallerResources.CustomizedSettings.InstallerFullName);
			}
			
		}
		
		private void ExtractingPage_PageLoad(object sender, EventArgs e) {
			
			_prev                 = null;
			WizardForm.EnableBack = false;
			WizardForm.EnableNext = false;
			
			// Begin extraction
			switch(PackageInfo.Source) {
				
				case PackageSource.File:
					
					// don't call InstantiatePackage from Load (use PageLoad) because it loads the next wizard page before this one finishes loading
					InstantiatePackage( PackageInfo.SourcePath );
					
					break;
					
				case PackageSource.Archive:
				case PackageSource.Embedded:
					
					PackageInfo.Archive.PackageProgressEvent += new EventHandler<PackageProgressEventArgs>(Archive_PackageProgressEvent);
					PackageInfo.Archive.BeginPackageExtract( new Action<String>( Archive_Completed ) );
					
					break;
			}
		}
		
		private void Archive_PackageProgressEvent(object sender, PackageProgressEventArgs e) {
			
			// only fire it if the percentage has moved more than 1%
			float oldPerc = __progress.Value;
			float newPerc = e.Percentage;
			
			if( oldPerc == newPerc ) return; // if percs are the same
			// if newperc is less than 1% different than oldperc, but only if newperc is less than 95%
			if( newPerc < 95 && ( newPerc < oldPerc + 1 ) ) return;
			
			BeginInvoke( new MethodInvoker( delegate() {
				
				__statusLbl.Text = e.Message;
				__progress.Value = e.Percentage;
				
			} ) );
			
		}
		
		private void Archive_Completed(String destDir) {
			
			this.Invoke( new MethodInvoker( delegate() {
				
				if( destDir != null ) {
					
					__statusLbl.Text = InstallerResources.GetString("C_B_instantiating");
					
					String packageFileName = Path.Combine( destDir, "package.xml" );
					
					if( !File.Exists( packageFileName ) ) {
						
						MessageBox.Show(this, InstallerResources.GetString("C_B_errorPackageXmlNotFound"), "Anolis", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						return;
					}
					
					InstantiatePackage( packageFileName );
					
				} else {
					
					String message;
					
					if( InstallerResources.IsCustomized ) {
						
						message = InstallerResources.GetString("C_B_error_Cus", InstallerResources.CustomizedSettings.InstallerFullName);
					} else {
						
						message = InstallerResources.GetString("C_B_error");
					}
					
					// the previous PackageProgressEvent method call will contain the error string, so don't set anything and display a message to the user
					MessageBox.Show(this, message, "Anolis", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					
				}
				
			} ) );
			
		}
		
		private void InstantiatePackage(String path) {
			
			try {
				
				PackageInfo.Package = Package.FromFile( path );
				
				if( InstallationInfo.UseSelector == null ) {
					InstallationInfo.UseSelector = PackageInfo.Package.Presets.Count > 0;
				}
				
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
				__packageMessages.Visible = true;
				
				_prev = Program.PageCASelectPackage;
				WizardForm.EnableBack = true;
				
				return;
				
				
			} catch(Exception ex) {
				
				StringBuilder sb = new StringBuilder();
				while(ex != null) {
					
					sb.Append( ex.GetType().Name );
					sb.Append( " - " );
					sb.AppendLine( ex.Message );
					sb.AppendLine( ex.StackTrace );
					sb.AppendLine();
					ex = ex.InnerException;
				}
				
				__packageMessages.Text = sb.ToString();
				__packageMessages.Visible = true;
				
				_prev = Program.PageCASelectPackage;
				WizardForm.EnableBack = true;
				
				return;
				
			}
			
			//////////////////////////////////////////////////
			
			if( !PackageInfo.IgnoreCondition ) {
				
				EvaluationResult result = PackageInfo.Package.Evaluate();
				
				if( result == EvaluationResult.False || result == EvaluationResult.FalseParent ) {
					
					String message = PackageInfo.Package.ConditionDesc;
					
					MessageBox.Show(this, message, "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					
					InstallationInfo.FailedCondition = true;
					
				} else if( result == EvaluationResult.Error ) {
					
					MessageBox.Show(this, InstallerResources.GetString("C_B_conditionError"), "Anolis Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					WizardForm.LoadPage( Program.PageCASelectPackage );
					return;
					
				}
				
			}
			
			WizardForm.LoadPage( Program.PageCCUpdatePackage );
			
		}
		
		private BaseWizardPage _prev = null;
		
		public override BaseWizardPage PrevPage {
			get { return _prev; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
	}
}
