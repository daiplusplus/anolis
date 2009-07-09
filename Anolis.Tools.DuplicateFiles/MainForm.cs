using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace Anolis.Tools.DuplicateFiles {
	
	public partial class MainForm : Form {
		
		private DuplicateFile[] _result;
		
		public MainForm() {
			InitializeComponent();
			
			__rootBrowse.Click += new EventHandler(__rootBrowse_Click);
			
			__exclude.SelectedIndexChanged += new EventHandler(__exclude_SelectedIndexChanged);
			__excludeAdd   .Click += new EventHandler(__excludeAdd_Click);
			__excludeRemove.Click += new EventHandler(__excludeRemove_Click);
			
			__process.Click += new EventHandler(__process_Click);
			
			__bw.DoWork             += new System.ComponentModel.DoWorkEventHandler(__bw_DoWork);
			__bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(__bw_RunWorkerCompleted);
			__bw.ProgressChanged    += new System.ComponentModel.ProgressChangedEventHandler(__bw_ProgressChanged);
			
			this.Load += new EventHandler(MainForm_Load);
			this.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);
		}
		
		private void MainForm_Load(Object sender, EventArgs e) {
			
			Settings.Default.Upgrade();
			
			__root.Text = Settings.Default.Root;
			
			StringCollection sc = Settings.Default.Exclude;
			if( sc == null ) return;
			
			foreach(String dir in sc) {
				
				DirectoryInfo d = new DirectoryInfo( dir );
				__exclude.Items.Add( d );
			}
			
		}
		
		private void MainForm_FormClosed(Object sender, FormClosedEventArgs e) {
			
			Settings.Default.Root = __root.Text;
			
			if( Settings.Default.Exclude == null ) Settings.Default.Exclude = new StringCollection();
			
			foreach(DirectoryInfo dir in __exclude.Items) {
				
				Settings.Default.Exclude.Add( dir.FullName );
			}
			
			Settings.Default.Save();
		}
		
		private void __rootBrowse_Click(Object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__root.Text = __fbd.SelectedPath;
			}
			
		}
		
#region Excluded Directories
		
		private void __exclude_SelectedIndexChanged(Object sender, EventArgs e) {
			
			__excludeRemove.Enabled = __exclude.SelectedIndex > -1;
		}
		
		private void __excludeAdd_Click(Object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				DirectoryInfo dir = new DirectoryInfo( __fbd.SelectedPath );
				if( dir.Exists ) __exclude.Items.Add( dir );
			}
			
		}
		
		private	void __excludeRemove_Click(Object sender, EventArgs e) {
			
			if( __exclude.SelectedIndex > -1 ) {
				
				__exclude.Items.Remove( __exclude.SelectedItem );
			}
			
		}
		
#endregion
		
		private void __process_Click(Object sender, EventArgs e) {
			
			SetEnabled( false, true, false );
			
			__bw.RunWorkerAsync();
		}
		
		private void __bw_ProgressChanged(Object sender, System.ComponentModel.ProgressChangedEventArgs e) {
			
			String message = e.UserState as String;
			
			if( e.ProgressPercentage == -1 ) {
				__progress.Style = ProgressBarStyle.Marquee;
			} else {
				__progress.Style = ProgressBarStyle.Continuous;
				__progress.Value       = e.ProgressPercentage;
			}
			
			__progressMessage.Text = e.ProgressPercentage.ToString() + "% - " + message;
		}
		
		private void SetEnabled(Boolean criteriaEnabled, Boolean resultsEnabled, Boolean actionsEnabled) {
			
			__rootLbl      .Enabled = criteriaEnabled;
			__root         .Enabled = criteriaEnabled;
			__rootBrowse   .Enabled = criteriaEnabled;
			
			__excludeLbl   .Enabled = criteriaEnabled;
			__exclude      .Enabled = criteriaEnabled;
			__excludeAdd   .Enabled = criteriaEnabled;
			__excludeRemove.Enabled = criteriaEnabled;
			
			__process      .Enabled = criteriaEnabled;
			
			__progressLbl  .Enabled = resultsEnabled;
			__progress     .Enabled = resultsEnabled;
		}
		
		private void __bw_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e) {
			
			DirectoryInfo root = new DirectoryInfo( __root.Text );
			
			DirectoryInfo[] excluded = new DirectoryInfo[ __exclude.Items.Count ];
			for(int i=0;i<__exclude.Items.Count;i++) {
				excluded[i] = (DirectoryInfo)__exclude.Items[i];
			}
			
			DuplicateFinder finder = new DuplicateFinder( root, excluded );
			finder.StatusUpdated += new EventHandler(finder_StatusUpdated);
			
			_result = finder.Process();
		}
		
		private void finder_StatusUpdated(Object sender, EventArgs e) {
			
			DuplicateFinder finder = sender as DuplicateFinder;
			
			__bw.ReportProgress( finder.PercentageCompleted, finder.StatusMessage );
		}
		
		private void __bw_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
			
			__results.BeginUpdate();
			
			Int64 wastedSpace = 0;
			
			foreach(DuplicateFile dupe in _result) {
				
				ListViewGroup grp = new ListViewGroup( dupe.Hash );
				__results.Groups.Add( grp );
				
				foreach(FileInfo file in dupe.Matches) {
					
					wastedSpace += file.Length;
					
					String relativePath = file.FullName.Substring( __root.Text.Length );
					
					ListViewItem item = new ListViewItem( relativePath );
					__results.Items.Add( item );
					
					item.Group = grp;
				}
				
				wastedSpace -= dupe.Matches[0].Length;
			}
			
			__results.EndUpdate();
			
			__totalWastedSpace.Text = "Total Wasted Space: " + (wastedSpace / 1048576).ToString() + "MB";
			
			SetEnabled( true, false, true );
		}
		
	}
}
