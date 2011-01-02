using System;
using System.IO;
using System.Windows.Forms;

using ThumbDBLib;
using System.Collections.Generic;

namespace Anolis.Tools.TweakUI.ThumbsDB {
	
	public partial class ThumbnailForm : Form {
		
		private ThumbDB _db;
		
		public ThumbnailForm() {
			
			InitializeComponent();

			this.Load += new EventHandler(MainForm_Load);
			
			this.__thumbPathBrowse.Click += new EventHandler(__thumbPathBrowse_Click);
			this.__thumbPathLoad  .Click += new EventHandler(__thumbPathLoad_Click);
			this.__export         .Click += new EventHandler(__export_Click);
			this.__credit         .Click += new EventHandler(__credit_Click);
		}
		
		public String InitialThumbsDb { get; set; }
		
		private void MainForm_Load(object sender, EventArgs e) {
			
			if( File.Exists( InitialThumbsDb ) ) {
				
				__thumbPath.Text = InitialThumbsDb;
				
				LoadDb();
			}
			
		}
		
		
		
		private void __credit_Click(object sender, EventArgs e) {
			
			System.Diagnostics.Process.Start("http://www.petedavis.net/MySite/DynPageView.aspx?pageid=31");
		}
		
		private void __thumbPathBrowse_Click(object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__thumbPath.Text = __ofd.FileName;
				
				if( File.Exists( __thumbPath.Text ) ) LoadDb();
				
			}
			
		}
		
		private void __thumbPathLoad_Click(object sender, EventArgs e) {
			
			LoadDb();
		}
		
		private void __export_Click(object sender, EventArgs e) {
			
			foreach(ThumbnailItem item in _selectedItems) {
				
				__sfd.Title = "Save " + item.Caption + " as...";
				
				if( __sfd.ShowDialog(this) == DialogResult.OK ) {
					
					item.Image.Save( __sfd.FileName );
				}
				
			}
			
		}
		
		
		private void LoadDb() {
			
			String fileName = __thumbPath.Text;
			
			if( !File.Exists( fileName ) ) {
				
				MessageBox.Show(this, "The specified file does not exist", "Thumbs.db Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
			}
			
			_db = new ThumbDB( fileName ); // NOTE: ThumbDB doesn't do much error checking or validation, so any exception could be thrown by the Framework, not ThumbDB itself
			
			String[] thumbFileNames = _db.GetThumbFileNames();
			
			__thumbs.Controls.Clear();
			
			foreach(String thumbFileName in thumbFileNames) {
				
				ThumbnailItem item = new ThumbnailItem();
				item.Caption = thumbFileName;
				item.Image   = _db.GetThumbnailImage( thumbFileName );
				item.Click += new EventHandler(item_Click);
				
				__thumbs.Controls.Add( item );
				
			}
			
		}
		
		private List<ThumbnailItem> _selectedItems = new List<ThumbnailItem>();
		
		private void item_Click(object sender, EventArgs e) {
			
			ThumbnailItem item = sender as ThumbnailItem;
			item.IsSelected = !item.IsSelected;
			
			if( item.IsSelected ) _selectedItems.Add( item );
			else                  _selectedItems.Remove( item );
			
		}
		
	}
}
