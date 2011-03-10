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
			
			AddEncodingMenuItems();
			
			__toolsEnc_Click( __toolsEncUtf8, EventArgs.Empty ); // set UTF-8 as the default encoding
		}
		
		private void AddEncodingMenuItems() {
			
			__toolsEncAscii.Click += new EventHandler(__toolsEnc_Click); __toolsEncAscii.Tag = new ASCIIEncoding();
			__toolsEncUtf7 .Click += new EventHandler(__toolsEnc_Click); __toolsEncUtf7 .Tag = new UTF7Encoding();
			__toolsEncUtf8 .Click += new EventHandler(__toolsEnc_Click); __toolsEncUtf8 .Tag = new UTF8Encoding();
			__toolsEncUtf16.Click += new EventHandler(__toolsEnc_Click); __toolsEncUtf16.Tag = new Object();
			__toolsEncUtf32.Click += new EventHandler(__toolsEnc_Click); __toolsEncUtf32.Tag = new Object();
			
			__toolsEncodingBE .Click += new EventHandler(__toolsEncodingOptions_Click);
			__toolsEncodingLE .Click += new EventHandler(__toolsEncodingOptions_Click);
			__toolsEncodingBom.Click += new EventHandler(__toolsEncodingOptions_Click);
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
				__text.Text = TextEncoding.GetString( data );
			
			} catch(EncoderFallbackException fex) {
				
				MessageBox.Show(this, "Could not render the text stream using the current encoder: " + fex.Message, "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
			}
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			if( data is SgmlResourceData ) return TypeViewerCompatibility.Ideal;
			return TypeViewerCompatibility.Works;
		}
		
		public override string ViewerName {
			get { return "Raw Text (with Encoding) Viewer"; }
		}
		
		private Encoding TextEncoding {
			set {
				__toolsEncoding.Tag = value;
				if( _data != null ) RenderResource( _data );
			}
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
		
		private void __toolsEncodingOptions_Click(object sender, EventArgs e) {
			
			ToolStripMenuItem mi = sender as ToolStripMenuItem;
			if( mi == null ) return;
			
			__toolsEncodingBE.Checked = mi == __toolsEncodingBE;
			__toolsEncodingLE.Checked = mi == __toolsEncodingLE;
			
			if( this.TextEncoding is UTF32Encoding ) {
				
				this.TextEncoding = new UTF32Encoding( __toolsEncodingBE.Checked, __toolsEncodingBom.Checked );
				
			} else if( this.TextEncoding is UnicodeEncoding ) {
				
				this.TextEncoding = new UnicodeEncoding( __toolsEncodingBE.Checked, __toolsEncodingBom.Checked );
			}
		}
		
		private void __toolsEnc_Click(object sender, EventArgs e) {
			
			ToolStripMenuItem se = sender as ToolStripMenuItem;
			
			// uncheck all the others except this one
			foreach(ToolStripItem item in __toolsEncoding.DropDownItems) {
				if( item.Tag == null ) continue;
				ToolStripMenuItem mi = item as ToolStripMenuItem;
				if( mi == null ) continue;
				
				mi.Checked = item == se;
			}
			
			__toolsEncodingBE .Enabled = !(se.Tag is Encoding);
			__toolsEncodingLE .Enabled = !(se.Tag is Encoding);
			__toolsEncodingBom.Enabled = !(se.Tag is Encoding);
			
			// and set the encoding, of course
			
			if( se.Tag is Encoding ) {
				
				this.TextEncoding = se.Tag as Encoding;
				
			} else {
				
				if( se == __toolsEncUtf16 ) {
					
					this.TextEncoding = new UnicodeEncoding( __toolsEncodingBE.Checked, __toolsEncodingBom.Checked );
					
				} else if( se == __toolsEncUtf32 ) {
					
					this.TextEncoding = new UTF32Encoding( __toolsEncodingBE.Checked, __toolsEncodingBom.Checked );
				}
				
			}
			
			__toolsEncoding.Text = se.Text;
			
		}
		
#endregion
		
	}
}
