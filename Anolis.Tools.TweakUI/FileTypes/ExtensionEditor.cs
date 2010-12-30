using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Anolis.Packages.Utility;

namespace Anolis.Tools.TweakUI.FileTypes {
	
	public partial class ExtensionEditor : UserControl {
		
		private FileExtension _extension;
		
		public ExtensionEditor() {
			InitializeComponent();
			
			this.__persistentHandler.SelectedIndex = 0;
			this.__save.Click += new EventHandler(__save_Click);
			this.__reload.Click += new EventHandler(__reload_Click);
		}
		
#region UI Events
		
		private void __save_Click(object sender, EventArgs e) {
			
			if( _extension == null ) return;
			
			_extension.IsDirty = true;
			_extension.PersistentHandler = __persistentHandler.Tag as String;
			_extension.PerceivedType     = __perceived.Text;
			_extension.FileType          = __type.Tag as FileType;
			_extension.ContentType       = __contentType.Text;
		}
		
		private void __reload_Click(object sender, EventArgs e) {
			
			ShowExtension( _extension );
		}
		
#endregion
		
		protected override void OnPaintBackground(PaintEventArgs e) {
			base.OnPaintBackground(e);
			
			e.Graphics.DrawLine( SystemPens.ControlDark, 0, 0, Width - 1, 0 );
		}
		
		public void ShowExtension(FileExtension ext) {
			
			_extension = ext;
			
			if( Enabled = ext != null ) {
				
				this.__extension  .Text                = ext.Extension;
				this.__persistentHandler.SelectedIndex = GetPersistentIndex( ext.PersistentHandler );
				this.__perceived  .Text                = ext.PerceivedType;
				this.__type       .Text                = ext.FileType != null ? ext.FileType.ProgId : "";
				this.__contentType.Text                = ext.ContentType;
				
			} else {
				
				// Reset the form
				this.__extension  .Text                = String.Empty;
				this.__persistentHandler.SelectedIndex = -1;
				this.__perceived  .Text                = String.Empty;
				this.__type       .Text                = String.Empty;
				this.__contentType.Text                = String.Empty;
				
			}
		}
		
		public FileExtension FileExtension {
			get { return _extension; }
		}
		
		private Int32 GetPersistentIndex(String persistentGuid) {
			
			// 0 = None
			// 1 = Text
			// 2 = Null
			// 3 = Html
			// 4 = Other...
			
			__persistentHandler.Tag = persistentGuid;
			
			if( String.IsNullOrEmpty( persistentGuid ) ) {
				
				return 0;
			}
			
			switch( persistentGuid ) {
				case FilePersistentHandlers.Text:
					return 1;
				case FilePersistentHandlers.Null:
					return 2;
				case FilePersistentHandlers.Html:
					return 3;
			}
			
			return 4;
		}
		
	}
}
