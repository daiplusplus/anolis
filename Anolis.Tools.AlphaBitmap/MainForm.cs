using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Tools.AlphaBitmap {
	
	public partial class MainForm : Form {
		
		public MainForm() {
			InitializeComponent();
			
			this.__alphaBrowse.Click += new EventHandler(__alphaBrowse_Click);
			this.__colorBrowse.Click += new EventHandler(__colorBrowse_Click);
			
			this.__compose.Click += new EventHandler(__compose_Click);
			
			this.__save.Click += new EventHandler(__save_Click);
		}
		
		private void __save_Click(object sender, EventArgs e) {
			
			Image final = __final.Image;
			if( final == null ) return;
			
			if( __sfd.ShowDialog(this) == DialogResult.OK ) {
				
				if( __sfd.FilterIndex == 1 ) {
					
					final.Save( __sfd.FileName, ImageFormat.Bmp );
					
				} else if( __sfd.FilterIndex == 2 ) {
					
					final.Save( __sfd.FileName, ImageFormat.Png );
					
				}
				
			}
			
		}
		
		private void __colorBrowse_Click(object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__colorSource.Text = __ofd.FileName;
				
				Image img = Image.FromFile( __ofd.FileName );
				__color.Image = img;
				
			}
			
		}
		
		private void __alphaBrowse_Click(object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__alphaSource.Text = __ofd.FileName;
				
				Image img = Image.FromFile( __ofd.FileName );
				__alpha.Image = img;
				
			}
			
		}
		
		private void __compose_Click(object sender, EventArgs e) {
			
			Bitmap color = __color.Image as Bitmap;
			Bitmap alpha = __alpha.Image as Bitmap;
			
			Bitmap final = new Bitmap( color.Width, color.Height, PixelFormat.Format32bppArgb );
			
			// the inefficient way for purity right now
			
			for(int x=0;x<color.Width;x++ ) {
				
				for(int y=0;y<color.Height;y++) {
					
					Color c = color.GetPixel( x, y );
					Color a = alpha.GetPixel( x, y );
					
					Int32 ac;
					switch(alpha.PixelFormat) {
						case PixelFormat.Format32bppArgb:
							
							ac = a.A;
							break;
							
						case PixelFormat.Format24bppRgb:
						default:
							
							ac = a.R;
							break;
					}
					
					Color f = Color.FromArgb( ac, c.R, c.G, c.B );
					
					final.SetPixel( x, y, f );
					
				}
			}
			
			__final.Image = final;
			
		}
		
	}
}
