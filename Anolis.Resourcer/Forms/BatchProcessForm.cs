using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Resourcer.CommandLine;

using S = Anolis.Resourcer.Settings.Settings;

namespace Anolis.Resourcer {
	
	public partial class BatchProcessForm : BaseForm {
		
		public BatchProcessForm() {
			
			InitializeComponent();
			
			this.__sourceBrowse.Click += new EventHandler(__sourceBrowse_Click);
			this.__reportBrowse.Click += new EventHandler(__reportBrowse_Click);
			this.__exportBrowse.Click += new EventHandler(__exportBrowse_Click);

			this.__reportEnable.CheckedChanged += new EventHandler(__reportEnable_CheckedChanged);
			
			this.__sourceDirectory.Validating += new CancelEventHandler(__sourceDirectory_Validating);
			this.__reportFilename .Validating += new CancelEventHandler(__reportFilename_Validating);
			this.__exportDirectory.Validating += new CancelEventHandler(__exportDirectory_Validating);
			
			this.__process.Click += new EventHandler(__process_Click);
			
			this.__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			this.__bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(__bw_RunWorkerCompleted);
			
			this.Load += new EventHandler(BatchProcessForm_Load);
			this.FormClosing += new FormClosingEventHandler(BatchProcessForm_FormClosing);
			
			SetEnabled(false);
		}
		
		private void BatchProcessForm_Load(object sender, EventArgs e) {
			LoadOptionsFromSettings();
		}
		
		private void BatchProcessForm_FormClosing(object sender, FormClosingEventArgs e) {
			SaveOptionsToSettings();
		}
		
		private void LoadOptionsFromSettings() {
			
			__sourceDirectory.Text = S.Default.BatchSource;
			__sourceFilter.Text    = S.Default.BatchFilter;
			__reportFilename.Text  = S.Default.BatchReport;
			__exportDirectory.Text = S.Default.BatchExport;
		}
		
		private void SaveOptionsToSettings() {
			
			S.Default.BatchSource = __sourceDirectory.Text;
			S.Default.BatchFilter = __sourceFilter.Text;
			S.Default.BatchReport = __reportFilename.Text;
			S.Default.BatchExport = __exportDirectory.Text;
		}
		
#region UI Events
		
		private void __sourceBrowse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__sourceDirectory.Text = __fbd.SelectedPath;
			}
			
		}
		
		private void __reportBrowse_Click(object sender, EventArgs e) {
			
			if( __sfd.ShowDialog(this) == DialogResult.OK ) {
				
				__reportFilename.Text = __sfd.FileName;
			}
			
		}
		
		private void __exportBrowse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__exportDirectory.Text = __fbd.SelectedPath;
			}
			
		}
		
		private void __reportEnable_CheckedChanged(object sender, EventArgs e) {
			
			SetEnabled(false);
		}
		
		private void __process_Click(Object sender, EventArgs e) {
			
			DoBatchProcess();
		}
		
	#region Validation
		
		private void __sourceDirectory_Validating(object sender, CancelEventArgs e) {
			
			String dirPath = __sourceDirectory.Text;
			
			if( !Directory.Exists( dirPath ) ) {
				
				__error.SetError( __sourceDirectory, "Specified directory does not exist" );
				e.Cancel = true;
				
			} else {
				
				__error.SetError( __sourceDirectory, "" );
			}
			
		}
		
		private void __exportDirectory_Validating(object sender, CancelEventArgs e) {
			
			if( __exportDirectory.Text.Length == 0 ) {
				
				e.Cancel = true;
				
				__error.SetError( __exportDirectory, "Set a directory to export to" );
				
			}
			
		}
		
		private void __reportFilename_Validating(object sender, CancelEventArgs e) {
			
			if( !__reportEnable.Checked ) return;
			
			if( __reportFilename.Text.Length == 0 ) {
				
				e.Cancel = true;
				
				__error.SetError( __reportFilename, "Choose a report filename" );
				
			}
		}
		
	#endregion
		
		private void SetEnabled(Boolean busy) {
			
			__sourceGrp .Enabled = !busy;
			__optionsGrp.Enabled = !busy;
			
			__reportFilename   .Enabled = __reportEnable.Checked;
			__reportBrowse     .Enabled = __reportEnable.Checked;
			__reportFilenameLbl.Enabled = __reportEnable.Checked;
			
			__progGrp.Enabled = busy;
		}
		
#endregion
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			BatchOptions options = e.Argument as BatchOptions;
			
			BatchProcess process = new BatchProcess();
			process.MajorProgressChanged += new EventHandler(process_MajorProgressChanged);
			process.MinorProgressChanged += new EventHandler(process_MinorProgressChanged);
			
			process.Process( options );
			
		}
		
		private void __bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			
			SetEnabled(false);
		}
		
		private void process_MajorProgressChanged(object sender, EventArgs e) {
			BeginInvoke( new MethodInvoker( delegate() {
				
				__progOverall.Value = (sender as BatchProcess).MajorProgressPercentage;
			}));
		}
		private void process_MinorProgressChanged(object sender, EventArgs e) {
			BeginInvoke( new MethodInvoker( delegate() {
				
				__progSource.Value = (sender as BatchProcess).MinorProgressPercentage;
			}));
		}
		
		private void DoBatchProcess() {
			
			if( !ValidateChildren() ) return;
			
			SetEnabled(true);
			
			BatchOptions options = new BatchOptions();
			options.SourceDirectory = new DirectoryInfo( __sourceDirectory.Text );
			options.SourceFilter    = __sourceFilter.Text;
			options.SourceRecurse   = __sourceRecurse.Checked;
			
			options.ReportCreate    = __reportEnable.Checked;
			if( options.ReportCreate )
				options.ReportFile  = new FileInfo( __reportFilename.Text );
			
			options.ExportDirectory = new DirectoryInfo( __exportDirectory.Text );	
			options.ExportAllLangs  = __exportLangs.Checked;
			options.ExportNonVisual = __exportNonvisual.Checked;
			options.ExportIcons     = __exportIconSubimages.Checked;
			
			__bw.RunWorkerAsync( options );
		}
		
		
	}
}
