using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Anolis.Core.Packages;
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
			
			Localize();
		}
		
		protected override String LocalizePrefix { get { return "C_G"; } }
		
		private void InstallingPage_PageLoad(object sender, EventArgs e) {
			
			WizardForm.EnableBack = false;
			WizardForm.EnableNext = false;
			
			PackageInfo.Package.ProgressEvent += new EventHandler<PackageProgressEventArgs>(Package_ProgressEvent);
			
			__bw.RunWorkerAsync();
		}
		
		private void __bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			
			Exception ex = e.Error;
			if(ex != null) {
				
				Invoke( new MethodInvoker( delegate() {
					
					throw new AnolisException("Error during installation", ex);
					
				}));
				
			}
			
		}
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			PackageExecutionSettings settings = new PackageExecutionSettings();
			
			if( PackageInfo.I386Install ) {
				
				settings.ExecutionMode = PackageExecutionMode.I386;
				settings.I386Directory = PackageInfo.I386Directory;
			
			} else {
				
				settings.ExecutionMode            = PackageExecutionMode.Regular;
				settings.CreateSystemRestorePoint = PackageInfo.SystemRestore;
				settings.BackupDirectory          = PackageInfo.BackupPath != null ? new DirectoryInfo( PackageInfo.BackupPath ) : null;
			}
			
			PackageInfo.Package.Execute(settings);
			
			PackageInfo.RequiresRestart = (Program.ProgramMode == ProgramMode.InstallPackage) && PackageInfo.Package.ExecutionInfo.RequiresRestart;
			
			Invoke( new MethodInvoker( delegate() {
				
				WizardForm.LoadPage( Program.PageFFinished );
			}));
			
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
