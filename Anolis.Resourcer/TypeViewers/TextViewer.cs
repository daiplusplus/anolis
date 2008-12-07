﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Win32;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class TextViewer : TypeViewer {
		
		public TextViewer() {
			InitializeComponent();
			
			__toolsEncoding.Tag = Encoding.UTF8;
			__toolsEncoding.DropDownItemClicked += new ToolStripItemClickedEventHandler(__toolsEncoding_DropDownItemClicked);
		}
		
		private void __toolsEncoding_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
			// change current encoding
			ToolStripItem item = e.ClickedItem;
			if(item.Tag != null) __toolsEncoding.Tag = item.Tag;
			__toolsEncoding.Text = item.Text;
		}
		
		public override void RenderResource(Win32ResourceLanguage resource) {
			
			Byte[] data = resource.GetData();
			
			__text.Text = Encoding.GetString( data );
			
		}
		
		public override bool CanHandleResourceType(Win32ResourceType type) {
			return true; // this can handle anything since it shows the text of raw bytes
		}
		
		public override string ViewerName {
			get { return "Raw Text (with Encoding) Viewer"; }
		}
		
		private Encoding Encoding {
			get { return __toolsEncoding.Tag as Encoding; }
		}
		
		private void LoadEncoding() {
			
			__toolsEncoding.DropDownItems.Clear();
			__toolsEncoding.DropDownItems.Add( new ToolStripMenuItem("ASCII")  { Tag = new ASCIIEncoding() } );
			__toolsEncoding.DropDownItems.Add( new ToolStripMenuItem("UTF-7")  { Tag = new UTF7Encoding(true) } );
			__toolsEncoding.DropDownItems.Add( new ToolStripMenuItem("UTF-8")  { Tag = new UTF8Encoding( __toolsBom.Checked ) } );
			__toolsEncoding.DropDownItems.Add( new ToolStripMenuItem("UTF-16") { Tag = new UnicodeEncoding(__toolsEndian.Checked, __toolsBom.Checked) } );
			__toolsEncoding.DropDownItems.Add( new ToolStripMenuItem("UTF-32") { Tag = new UTF32Encoding(__toolsEndian.Checked, __toolsBom.Checked) } );
			
		}
		
#region UI Events
		
		private void __toolsFont_Click(object sender, EventArgs e) {
			
			DialogResult result = __fdlg.ShowDialog(this);
			if(result == DialogResult.OK) {
				__text.Font = __fdlg.Font;
			}
		}
		
#endregion
		
	}
}