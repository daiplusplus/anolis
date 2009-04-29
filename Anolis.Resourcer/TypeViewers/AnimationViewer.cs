using System;
using System.IO;
using System.Windows.Forms;

using Anolis.Core.Data;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class AnimationViewer : TypeViewer {
		
		private String _filename;
		
		public AnimationViewer() {
			InitializeComponent();
			
			this.__tPlay.Click += new EventHandler(__tPlay_Click);
			this.__tStop.Click += new EventHandler(__tStop_Click);
			this.__tTransparent.CheckedChanged += new EventHandler(__tTransparent_CheckedChanged);
			
			this.__t.Enabled = false;
		}
		
		private void __tTransparent_CheckedChanged(object sender, EventArgs e) {
			
			__ani.TransparentVideo = __tTransparent.Checked;
		}
		
		private void __tStop_Click(object sender, EventArgs e) {
			
			__ani.Stop();
		}
		
		private void __tPlay_Click(object sender, EventArgs e) {
			
			__ani.Play();
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			if(data is RiffMediaResourceData) return TypeViewerCompatibility.Ideal;
			
			return TypeViewerCompatibility.None;
		}
		
		public override void RenderResource(ResourceData resource) {
			
			__ani.Close();
			
			RiffMediaResourceData res = resource as RiffMediaResourceData;
			if(res == null) {
				
				return;
			}
			
			if( File.Exists( _filename ) ) File.Delete( _filename );
			
			_filename = resource.Lang.ResourcePath.Replace('\\', '-') + ".bin";
			_filename = Path.Combine( Path.GetTempPath(), _filename );
			
			resource.Save( _filename );
			
			if( !__ani.IsHandleCreated) __ani.CreateControl();
			__ani.Open( _filename );
			
			if( !__ani.IsOpen ) {
				
				__ani.Close();
				
				__t.Enabled = false;
				
				MessageBox.Show(this, "Resourcer cannot play the specified RIFF media", "Anolis Resourcer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				return;
				
			}
			
			__t.Enabled = true;
			
			__ani.Play();
		}
		
		public override String ViewerName {
			get { return "Animation Viewer"; }
		}
		
	}
}
