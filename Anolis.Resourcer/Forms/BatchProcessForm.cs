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
using Anolis.Core.Source;

namespace Anolis.Resourcer {
	
	public partial class BatchProcessForm : BaseForm {
		
		public BatchProcessForm() {
			
			InitializeComponent();
			
			this.__sourceFileRad.CheckedChanged += new EventHandler(__sourceFileRad_CheckedChanged);
			
			this.__sourceFileBrowse.Click += new EventHandler(__sourceFileBrowse_Click);
			this.__sourceDirBrowse .Click += new EventHandler(__sourceDirBrowse_Click);
			this.__exportBrowse    .Click += new EventHandler(__exportBrowse_Click);
			
			this.__sourceFile     .Validating += new CancelEventHandler(__sourceFile_Validating);
			this.__sourceDir      .Validating += new CancelEventHandler(__sourceDir_Validating);
			this.__exportDirectory.Validating += new CancelEventHandler(__exportDirectory_Validating);
			
			this.__exportNonvisual    .CheckedChanged += new EventHandler(__exportNonvisual_CheckedChanged);
			this.__exportNonvisualSize.CheckedChanged += new EventHandler(__exportNonvisualSize_EnabledCheckedChanged);
			this.__exportNonvisualSize.EnabledChanged += new EventHandler(__exportNonvisualSize_EnabledCheckedChanged);
			
			this.__process.Click += new EventHandler(__process_Click);
			this.__close  .Click += new EventHandler(__close_Click);
			
			this.__bw.DoWork             += new DoWorkEventHandler(__bw_DoWork);
			this.__bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(__bw_RunWorkerCompleted);
			
			this.Load += new EventHandler(BatchProcessForm_Load);
			this.FormClosing += new FormClosingEventHandler(BatchProcessForm_FormClosing);
			
			__sourceFileRad.Checked = true;
			
			SetEnabled(false);
		}
		
		private void BatchProcessForm_Load(object sender, EventArgs e) {
			LoadOptionsFromSettings();
		}
		
		private void BatchProcessForm_FormClosing(object sender, FormClosingEventArgs e) {
			SaveOptionsToSettings();
		}
		
		private void LoadOptionsFromSettings() {
			
			__sourceDir.Text = S.Default.BatchSource;
			__sourceFilter.Text    = S.Default.BatchFilter;
			__exportDirectory.Text = S.Default.BatchExport;
			
			__exportNonvisual.Checked      = S.Default.BatchExportNonVis;
			__exportCommonplace.Checked    = S.Default.BatchExportNonVisCommon;
			__exportNonvisualSize.Checked  = S.Default.BatchExportNonVisSize;
			__exportNonvisualSizeNum.Value = S.Default.BatchExportNonVisSizeKB;
			__exportIconSubimages.Checked  = S.Default.BatchExportSubimage;
		}
		
		private void SaveOptionsToSettings() {
			
			S.Default.BatchSource = __sourceDir.Text;
			S.Default.BatchFilter = __sourceFilter.Text;
			S.Default.BatchExport = __exportDirectory.Text;
			
			S.Default.BatchExportNonVis       = __exportNonvisual.Checked;
			S.Default.BatchExportNonVisCommon = __exportCommonplace.Checked;
			S.Default.BatchExportNonVisSize   = __exportNonvisualSize.Checked;
			S.Default.BatchExportNonVisSizeKB = (uint)__exportNonvisualSizeNum.Value;
			S.Default.BatchExportSubimage     = __exportIconSubimages.Checked;
		}
		
#region UI Events
		
		private void __sourceFileRad_CheckedChanged(object sender, EventArgs e) {
			
			Boolean fileSel = __sourceFileRad.Checked;
			
			__sourceFileLbl   .Enabled = fileSel;
			__sourceFile      .Enabled = fileSel;
			__sourceFileBrowse.Enabled = fileSel;
			
			__sourceDirLbl    .Enabled = !fileSel;
			__sourceDir       .Enabled = !fileSel;
			__sourceDirBrowse .Enabled = !fileSel;
			
			__sourceFilter    .Enabled = !fileSel;
			__sourceFilterLbl .Enabled = !fileSel;
			__sourceRecurse   .Enabled = !fileSel;
			
			if( IsHandleCreated ) ValidateChildren();
			
		}
		
		private void __exportNonvisual_CheckedChanged(object sender, EventArgs e) {
			
			__exportCommonplace  .Enabled = (sender as CheckBox).Checked;
			__exportNonvisualSize.Enabled = (sender as CheckBox).Checked;
		}
		
		private void __exportNonvisualSize_EnabledCheckedChanged(object sender, EventArgs e) {
			
			CheckBox s = sender as CheckBox;
			
			__exportNonvisualSizeNum.Enabled = s.Enabled && s.Checked;
			__exportNonvisualSizeLbl.Enabled = s.Enabled && s.Checked;
		}
		
		private void __sourceFileBrowse_Click(object sender, EventArgs e) {
			
			IList<ResourceSourceFactory> factories = ResourceSourceFactory.GetFactories();
			
			String filter = String.Empty;
			foreach(ResourceSourceFactory factory in factories) filter += factory.OpenFileFilter + '|';
			
			filter += "All Files (*.*)|*.*";
			__ofd.Filter = filter;
			
			if(__ofd.ShowDialog(this) == DialogResult.OK) {
				
				__sourceFile.Text = __ofd.FileName;
			}
			
		}
		
		private void __sourceDirBrowse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__sourceDir.Text = __fbd.SelectedPath;
			}
			
		}
		
		private void __exportBrowse_Click(object sender, EventArgs e) {
			
			if( Directory.Exists( __exportDirectory.Text ) ) __fbd.SelectedPath = __exportDirectory.Text;
			
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
		
		private void __close_Click(object sender, EventArgs e) {
			
			CancelOp();
			
			Close();
		}
		
	#region Validation
		
		private void __sourceFile_Validating(object sender, CancelEventArgs e) {
			
			if( !__sourceFileRad.Checked ) {
				__error.SetError( __sourceFile, "" );
				return;
			}
			
			String filePath = __sourceFile.Text;
			
			if( filePath.Length == 0 ) {
				
				__error.SetError( __sourceFile, "Specify a file to open" );
				e.Cancel = true;
				
			}
			
			if( !File.Exists( filePath ) ) {
				
				__error.SetError( __sourceFile, "Specified file does not exist" );
				e.Cancel = true;
				
			} else {
				
				__error.SetError( __sourceFile, "" );
			}
			
		}
		
		private void __sourceDir_Validating(object sender, CancelEventArgs e) {
			
			if( !__sourceDirRad.Checked ) {
				__error.SetError( __sourceDir, "" );
				return;
			}
			
			String dirPath = __sourceDir.Text;
			
			if( dirPath.Length == 0 ) {
				
				__error.SetError( __sourceDir, "Specify a directory to search" );
				e.Cancel = true;
				
			}
			
			if( !Directory.Exists( dirPath ) ) {
				
				__error.SetError( __sourceDir, "Specified directory does not exist" );
				e.Cancel = true;
				
			} else {
				
				__error.SetError( __sourceDir, "" );
			}
			
		}
		
		private void __exportDirectory_Validating(object sender, CancelEventArgs e) {
			
			if( __exportDirectory.Text.Length == 0 ) {
				
				e.Cancel = true;
				
				__error.SetError( __exportDirectory, "Set a directory to export to" );
				
			} else {
				__error.SetError( __exportDirectory, "" );
			}
			
		}
		
	#endregion
		
		private void SetEnabled(Boolean busy) {
			
			__sourceGrp .Enabled = !busy;
			__optionsGrp.Enabled = !busy;
			
			__progGrp.Enabled = busy;
			
			__process.Text = busy ? "Cancel" : "Export";
		}
		
#endregion
		
		public String SourceFilePath {
			get { return __sourceFile.Text; }
			set { __sourceFile.Text = value; }
		}
		
		public String SourceDirectoryPath {
			get { return __sourceDir.Text; }
			set { __sourceDir.Text = value; }
		}
		
		private BatchProcess _process = new BatchProcess();
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			BatchOptions options = e.Argument as BatchOptions;
			
			_process.MajorProgressChanged += new EventHandler(process_MajorProgressChanged);
			_process.MinorProgressChanged += new EventHandler(process_MinorProgressChanged);
			_process.Process( options );
		}
		
		private void __bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			
			if( !IsHandleCreated ) return;
			
			__progSource .Value = 100;
			__progSourceLbl.Text = "100% - Complete";
			__progOverall.Value = 100;
			__progOverallLbl.Text = "100% - Complete";
			
			SetEnabled(false);
			
			if( _process != null ) {
				
				if( _process.Cancel ) {
					MessageBox.Show(this, "Export cancelled with " + _process.Log.CountNonNominal + " errors", "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				} else {
					MessageBox.Show(this, "Export completed with " + _process.Log.CountNonNominal + " errors", "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				}
				
			}
			
		}
		
		private void process_MajorProgressChanged(object sender, EventArgs e) {
			
			if( !IsHandleCreated ) return; // to avoid exception when this method is called after Cancel();Close();
			
			BeginInvoke( new MethodInvoker( delegate() {
				
				BatchProcess proc = sender as BatchProcess;
				
				String message = proc.MajorProgressMessage;
				Int32  percent = proc.MajorProgressPercentage;
				
				if( percent == -1 ) {
					
					__progOverall.Style = ProgressBarStyle.Marquee; // the setter checks for equality, so don't worry about performance
					__progOverallLbl.Text = message;
					
					return;
				}
				
				__progOverall.Style = ProgressBarStyle.Blocks;
				__progOverall.Value   = percent;
				__progOverallLbl.Text = String.Format("{0}% - {1} - {2}/{3} files", percent, message, proc.FilesDone, proc.FilesCount );
				
			}));
		}
		
		private void process_MinorProgressChanged(object sender, EventArgs e) {
			
			if( !IsHandleCreated ) return;
			
			BeginInvoke( new MethodInvoker( delegate() {
				
				String message = (sender as BatchProcess).MinorProgressMessage;
				Int32  percent = (sender as BatchProcess).MinorProgressPercentage;
				
				__progSource.Value   = percent;
				__progSourceLbl.Text = String.Format("{0}% - {1}", percent, message);
				
			}));
		}
		
		private void DoBatchProcess() {
			
			if( !ValidateChildren() ) return;
			
			SetEnabled(true);
			
			if( _process != null && _process.IsBusy ) {
				CancelOp();
				return;
			}
			
			BatchOptions options        = new BatchOptions();
			
			if( __sourceFileRad.Checked ) {
				
				options.SourceMode      = BatchSourceMode.SingleFile;
				
				options.SourceFile      = new FileInfo( __sourceFile.Text );
				
			} else {
				
				options.SourceMode      = BatchSourceMode.Directory;
				
				options.SourceDirectory = new DirectoryInfo( __sourceDir.Text );
				options.SourceFilter    = __sourceFilter.Text;
				options.SourceRecurse   = __sourceRecurse.Checked;
				
			}
			
			options.ExportDirectory     = new DirectoryInfo( __exportDirectory.Text );	
			options.ExportCommonRes     = __exportCommonplace.Checked;
			options.ExportNonVisualSize = (Int32)__exportNonvisualSizeNum.Value * 1024;
			options.ExportNonVisual     = __exportNonvisual.Checked;
			options.ExportIcons         = __exportIconSubimages.Checked;
			
			__bw.RunWorkerAsync( options );
		}
		
		private void CancelOp() {
			
			if( _process != null ) {
				
				if( _process.IsBusy ) _process.Cancel = true;
			}
			
		}
		
	}
}
