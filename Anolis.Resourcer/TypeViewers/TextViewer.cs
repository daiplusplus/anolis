using System;
using System.Text;
using System.Windows.Forms;

using Anolis.Core.Data;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class TextViewer : TypeViewer {
		
		private ResourceData _data;
		
		public TextViewer() {
			InitializeComponent();
			
			this.__text.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, __text.Font.SizeInPoints);
			
			this.__toolsFont.Click += new EventHandler(__toolsFont_Click);
			this.__toolsWrap.CheckedChanged += new EventHandler(__toolsWrap_CheckedChanged);
			
			Encoding = Encoding.UTF8;
			
			this.__toolsEncAscii  .Tag = Encoding.ASCII;
			this.__toolsEncUtf7   .Tag = Encoding.UTF7;
			this.__toolsEncUtf8   .Tag = new UTF8Encoding(false);
			this.__toolsEncUtf8Bom.Tag = Encoding.UTF8; // new UTF8Encoding(true);
			this.__toolsEncUtf16LE.Tag = Encoding.Unicode;
			this.__toolsEncUtf16BE.Tag = Encoding.BigEndianUnicode;
			this.__toolsEncUtf32  .Tag = Encoding.UTF32;
			
			this.__toolsEncoding.DropDownItemClicked += new ToolStripItemClickedEventHandler(__toolsEncoding_DropDownItemClicked);
			
		}
		
		private void __toolsEncoding_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
			
			ToolStripMenuItem clickedItem = e.ClickedItem as ToolStripMenuItem;
			clickedItem.Checked = true;
			
			// uncheck all other items
			foreach(ToolStripMenuItem item in __toolsEncoding.DropDownItems) if( item != clickedItem ) item.Checked = false;
			
			Encoding = clickedItem.Tag as Encoding;
			
			__toolsEncoding.Text = clickedItem.Text;
			
			if( _data != null ) RenderResource( _data );
		}
		
		public override void RenderResource(ResourceData resourceData) {
			
			Byte[] data = (_data = resourceData).RawData;
			
			if( data.Length > 1024 * 1024 ) {
				
				DialogResult r = MessageBox.Show(this, "The resource to display is larger than one megabyte. Are you sure you want to continue loading it?", "Anolis Resourcer", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
				
				if(r == DialogResult.Cancel) {
					this.Visible = false;
					return;
				} else this.Visible = true;
				
			}
			
			try {
				__text.Text = Encoding.GetString( data );
			
			} catch(EncoderFallbackException fex) {
				
				MessageBox.Show(this, "Could not render the text stream using the current encoder: " + fex.Message, "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
			}
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			return TypeViewerCompatibility.Works;
		}
		
		public override string ViewerName {
			get { return "Raw Text (with Encoding) Viewer"; }
		}
		
		private Encoding Encoding {
			set { __toolsEncoding.Tag = value; }
			get { return __toolsEncoding.Tag as Encoding; }
		}
		
#region UI Events
		
		private void __toolsFont_Click(object sender, EventArgs e) {
			
			DialogResult result = __fdlg.ShowDialog(this);
			if(result == DialogResult.OK) {
				__text.Font = __fdlg.Font;
			}
		}
		
		private void __toolsWrap_CheckedChanged(object sender, EventArgs e) {
			
			__text.WordWrap = __toolsWrap.Checked;
		}
		
#endregion
		
	}
}
