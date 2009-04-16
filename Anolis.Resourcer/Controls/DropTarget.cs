using System;
using System.IO;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Resourcer.Controls {
	
	public partial class DropTarget : UserControl {
		
		public DropTarget() {
			
			InitializeComponent();
			
			this.__sourceOpen.Tag  = "SourceLoad";
			this.__dataAdd.Tag     = "DataAdd";
			this.__dataReplace.Tag = "DataReplace";
			
			this.__sourceOpen.DragEnter += new DragEventHandler(file_DragEnter);
			this.__sourceOpen.DragDrop += new DragEventHandler(file_DragDrop);
			this.__sourceOpen.DragLeave += new EventHandler(file_DragLeave);
			
			this.__dataAdd.DragEnter += new DragEventHandler(file_DragEnter);
			this.__dataAdd.DragDrop += new DragEventHandler(file_DragDrop);
			this.__sourceOpen.DragLeave += new EventHandler(file_DragLeave);
			
			this.__dataReplace.DragEnter += new DragEventHandler(file_DragEnter);
			this.__dataReplace.DragDrop += new DragEventHandler(file_DragDrop);
			this.__sourceOpen.DragLeave += new EventHandler(file_DragLeave);
		}
		
		protected override void OnDragLeave(EventArgs e) {
			
			// wait 10ms to see if it's on any of the buttons
			
			Timer timer = new Timer();
			timer.Interval = 10;
			timer.Tick += new EventHandler(delegate(Object sender, EventArgs e2) {
				
				if( !_buttonsHaveDrag ) {
					if( DragLeave2 != null ) DragLeave2(this, e);
				}
				timer.Stop();
				
			});
			timer.Start();
			
		}
		
		// TODO: Replace Button instances with "push buttons" that can be set into "pushed" appearance on drag-enter
		
		public Boolean DropDataAddEnabled {
			get { return __dataAdd.Enabled; }
			set { __dataAdd.Enabled = value; }
		}
		public Boolean DropDataReplaceEnabled {
			get { return __dataReplace.Enabled; }
			set { __dataReplace.Enabled = value; }
		}
		
		private Boolean ProcessDrop(DragEventArgs e) {
			
			if( !e.Data.GetDataPresent(DataFormats.FileDrop) ) return false;
			
			String[] filenames = e.Data.GetData(DataFormats.FileDrop) as String[];
			if(filenames == null)    return false;
			if(filenames.Length < 1) return false;
			
			if( !File.Exists(filenames[0]) ) return false;
			
			DropFile = filenames[0];
			
			this.Visible = false;
			
			return true;
		}
		
		private void file_DragDrop(object sender, DragEventArgs e) {
			
			if( ProcessDrop(e) ) {
				
				EventHandler call = null;
				switch( (sender as Button).Tag as String ) {
					case "SourceLoad":
						call = this.DropSourceLoad;
						break;
					case "DataAdd":
						call = this.DropDataAdd;
						break;
					case "DataReplace":
						call = this.DropDataReplace;
						break;
				}
				
				if( call != null ) call(sender, e);
				
			} else {
				DropFile = null;
			}
			
		}
		
		private Boolean _buttonsHaveDrag;
		
		private void file_DragEnter(object sender, DragEventArgs e) {
			
			_buttonsHaveDrag = true;
			
			if( e.Data.GetDataPresent(DataFormats.FileDrop) ) {
				e.Effect = DragDropEffects.Link;
			}
			
		}
		
		private void file_DragLeave(object sender, EventArgs e) {
			
			_buttonsHaveDrag = false;
		}
		
		public String DropFile {
			get; private set;
		}
		
		public event EventHandler DropSourceLoad;
		public event EventHandler DropDataAdd;
		public event EventHandler DropDataReplace;
		
		public event EventHandler DragLeave2;
		
	}
}
