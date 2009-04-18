using System;
using System.ComponentModel;
using System.Drawing;

using Anolis.Core.Data;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class RawViewer : TypeViewer {
		
		public RawViewer() {
			
			InitializeComponent();
			
			this.__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
			this.Resize += new EventHandler(RawViewer_Resize);
			this.Load += new EventHandler(RawViewer_Load);
		}
		
		private void RawViewer_Load(object sender, EventArgs e) {
			
			RawViewer_Resize(null, EventArgs.Empty);
		}
		
		private void RawViewer_Resize(object sender, EventArgs e) {
			
			Int32 x, y;
			
			x = (Width  - __loading.Width)/2;
			y = (Height - __loading.Height)/2;
			
			__loading.Location = new Point(x, y);
		}
		
		private Boolean _loading;
		
		private void __bw_DoWork(object sender, DoWorkEventArgs e) {
			
			_loading = true;
			
			Byte[] data = e.Argument as Byte[];
			
			Be.Windows.Forms.DynamicByteProvider bytesProv = new Be.Windows.Forms.DynamicByteProvider( data );
			
			__hex.ByteProvider = bytesProv;
			
			__loading.Visible = false;
			__hex.Visible     = true;;
			
			_loading = false;
		}
		
		public override void RenderResource(ResourceData resource) {
			
			Byte[] data = resource.RawData;
			
			if(data.Length > 1024 * 512) { // if greater than 512KB, thread it off
				
				__hex.Visible     = false;
				__loading.Visible = true;
				
				if(_loading) {
					// sleep 10ms, if it's still loading create a new bw
					System.Threading.Thread.Sleep(10);
					if(_loading) {
						__bw = new BackgroundWorker();
						__bw.DoWork += new DoWorkEventHandler(__bw_DoWork);
					}
				}
				
				__bw.RunWorkerAsync( data );
				
			} else {
				
				Be.Windows.Forms.DynamicByteProvider bytesProv = new Be.Windows.Forms.DynamicByteProvider( data );
				
				__hex.ByteProvider = bytesProv;
			}
			
			
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			return TypeViewerCompatibility.Works;
		}
		
		public override string ViewerName {
			get { return "Raw Binary Viewer"; }
		}
	}
}
