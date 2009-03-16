﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Anolis.Core.Packages;
using System.IO;
using W3b.Wizards;


namespace Anolis.Installer.Pages {
	
	public partial class InstallingPage : BaseInteriorPage {
		
		public InstallingPage() {
			InitializeComponent();
			
			this.PageLoad += new EventHandler(InstallingPage_PageLoad);
			this.__showLog.Click += new EventHandler(__showLog_Click);
			
			__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			__bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(__bw_RunWorkerCompleted);
		}
		
		private void __showLog_Click(object sender, EventArgs e) {
			
			StringBuilder sb = new StringBuilder();
			foreach(LogItem item in PackageInfo.Package.Log) {
				
				sb.Append( item.Severity.ToString() );
				sb.Append(" ");
				sb.Append( item.Message );
				sb.Append("\r\n");
			}
			
			__packageMessages.Text = sb.ToString();
			if( __packageMessages.Text.Length > 1 ) __packageMessages.SelectionStart = __packageMessages.Text.Length - 1;
			__packageMessages.ScrollToCaret();
			
			__packageMessages.Visible = true;
		}
		
		private void InstallingPage_PageLoad(object sender, EventArgs e) {
			
			WizardForm.EnablePrev = false;
			WizardForm.EnableNext = false;
			
			PackageInfo.Package.ProgressEvent += new EventHandler<PackageProgressEventArgs>(Package_ProgressEvent);
			
			__bw.RunWorkerAsync();
		}
		
		private void __bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			
			Exception ex = e.Error;
			if(ex != null) {
				
				Invoke( new MethodInvoker( delegate() {
					
					throw new Exception("Error during installation", ex);
					
				}));
				
			}
			
			// dump log to HDD
			
			using(StreamWriter wtr = new StreamWriter( System.IO.Path.Combine( Environment.CurrentDirectory,"AnolisInstaller.log"), true)) {
				
				wtr.WriteLine("Anolis.Resourcer - Package.Execute() Completed at " + DateTime.Now.ToString("s"));
				
				foreach(LogItem item in PackageInfo.Package.Log) {
					
					wtr.Write( item.Severity );
					wtr.Write(" - ");
					wtr.WriteLine( item.Message );
				}
				
			}
			
		}
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			CreatePointA();
			
			try {
			
				PackageInfo.Package.Execute();
				
			} finally {
				
				CreatePointB();
			}
			
			Invoke( new MethodInvoker( delegate() {
				
				WizardForm.LoadPage( Program.PageZFinished );
			}));
			
		}
		
		private void Package_ProgressEvent(object sender, PackageProgressEventArgs e) {
			
			Invoke( new MethodInvoker( delegate() {
				
				__progress.Value = e.Percentage;
				__statusLabel.Text = String.Format("{0}% complete - {1}", e.Percentage, e.Message );
				
			}));
			
		}
		
		public override BaseWizardPage PrevPage {
			get { return null; }
		}
		
		public override BaseWizardPage NextPage {
			get { return null; }
		}
		
#region System Restore
		
		private void CreatePointA() {
			
			if( PackageInfo.SystemRestore ) {
				
				Invoke( new MethodInvoker( delegate() {
					
					__progress.Style = ProgressBarStyle.Marquee;
					__statusLabel.Text = "Creating System Restore Point";
					
				}));
				
				String pointName = "Installed Anolis Package '" + PackageInfo.Package.RootSet.Name + '\'';
				
				PackageUtility.CreateSystemRestorePoint( pointName, PackageUtility.SystemRestoreType.ApplicationInstall, PackageUtility.SystemRestoreEventType.BeginSystemChange );
				
				Invoke( new MethodInvoker( delegate() {
					
					__progress.Style = ProgressBarStyle.Blocks;
					
				}));
				
			}
			
		}
		
		private void CreatePointB() {
			
			if( PackageInfo.SystemRestore ) {
				
				Invoke( new MethodInvoker( delegate() {
					
					__progress.Style = ProgressBarStyle.Marquee;
					__statusLabel.Text = "Finishing System Restore Point";
					
				}));
				
				String pointName = "Installed Anolis Package '" + PackageInfo.Package.RootSet.Name + '\'';
				
				PackageUtility.CreateSystemRestorePoint( pointName, PackageUtility.SystemRestoreType.ApplicationInstall, PackageUtility.SystemRestoreEventType.EndSystemChange );
				
				Invoke( new MethodInvoker( delegate() {
					
					__progress.Style = ProgressBarStyle.Blocks;
					
				}));
				
			}
			
		}
		
#endregion
		
	}
}
