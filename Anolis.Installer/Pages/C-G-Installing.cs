using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Anolis.Packages;
using Anolis.Core.Utility;

using W3b.Wizards.WindowsForms;
using Anolis.Core;

namespace Anolis.Installer.Pages {
	
	public partial class InstallingPage : BaseInteriorPage {
		
		public InstallingPage() {
			InitializeComponent();
			
			this.PageLoad += new EventHandler(InstallingPage_PageLoad);
			
			__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			__bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(__bw_RunWorkerCompleted);
		}
		
		protected override String LocalizePrefix { get { return "C_G"; } }
		
		protected override void Localize() {
			base.Localize();
			
			if( InstallationInfo.ProgramMode == ProgramMode.UninstallPackage ) {
				
				if( InstallerResources.IsCustomized ) {
					
					PageSubtitle = InstallerResources.GetString("E_B_Title_Cus"   , InstallerResources.CustomizedSettings.InstallerName);
					PageSubtitle = InstallerResources.GetString("E_B_Subtitle_Cus", InstallerResources.CustomizedSettings.InstallerName);
					
				} else {
					
					PageTitle    = InstallerResources.GetString("E_B_Title");
					PageSubtitle = InstallerResources.GetString("E_B_Subtitle");
				}
				
			} else {
				
				if( InstallerResources.IsCustomized ) {
					PageSubtitle = InstallerResources.GetString("C_G_Subtitle_Cus", InstallerResources.CustomizedSettings.InstallerName);
				}
				
			}
			
			
		}
		
		private void InstallingPage_PageLoad(object sender, EventArgs e) {
			
			WizardForm.EnableBack   = false;
			WizardForm.EnableNext   = false;
			WizardForm.EnableCancel = false; // no point re-enabling it since you can't go back and the next page is the Finish page
			
			PackageInfo.Package.ProgressEvent += new EventHandler<PackageProgressEventArgs>(Package_ProgressEvent);
			
			__bw.RunWorkerAsync();
		}
		
		private void __bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			
			Exception ex = e.Error;
			if(ex != null) {
				
				InstallationInfo.WriteException( ex );
				
				InstallationInfo.InstallationAborted = true;
			}
			
			WizardForm.LoadPage( Program.PageFFinished );
			
		}
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			PackageExecutionSettings settings = new PackageExecutionSettings();
			settings.LiteMode = PackageInfo.LiteMode;
			
			if( PackageInfo.I386Install ) {
				
				settings.ExecutionMode = PackageExecutionMode.CDImage;
				settings.I386Directory = PackageInfo.I386Directory;
			
			} else {
				
				settings.ExecutionMode            = PackageExecutionMode.Regular;
				settings.CreateSystemRestorePoint = PackageInfo.SystemRestore;
				settings.BackupDirectory          = PackageInfo.BackupPath != null ? new DirectoryInfo( PackageInfo.BackupPath ) : null;
			}
			
			PackageInfo.Package.Execute(settings);
			
			PackageInfo.RequiresRestart = PackageInfo.Package.ExecutionInfo.RequiresRestart;
			
			///////////////////////////////
			// Clean Up Extracted and Temporary Files
			
			if( PackageInfo.Source == PackageSource.Archive || PackageInfo.Source == PackageSource.Embedded ) {
				
				BeginInvoke( new MethodInvoker( delegate() {
					
					__progress.Style = ProgressBarStyle.Marquee;
					__statusLbl.Text = InstallerResources.GetString("C_G_cleanup");
					
				}));
				
				PackageInfo.Package.DeleteFiles();
				
			}
			
		}
		
		private void Package_ProgressEvent(object sender, PackageProgressEventArgs e) {
			
			if( !IsHandleCreated ) return;
			
			BeginInvoke( new MethodInvoker( delegate() {
				
				if( e.Percentage == -1 ) {
					
					__progress.Style = ProgressBarStyle.Marquee;
					
					__statusLbl.Text = e.Message;
					
				} else {
					
					__progress.Style = ProgressBarStyle.Blocks;
					__progress.Value = e.Percentage;
					
					__statusLbl.Text = String.Format( InstallerResources.GetString("C_G_status") , e.Percentage, e.Message );
				}
				
				
			}));
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
		
	}
}
