using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SharpZipLib.Tar;

using W3b.TarLzma;
using System.IO;

namespace Anolis.Packager {
	
	public partial class TarLzmaForm : Form {
		
		public TarLzmaForm() {
			
			InitializeComponent();
			
			this.__compress          .Click += new EventHandler(__compress_Click);
			this.__compressAddDir    .Click += new EventHandler(__compressAddDir_Click);
			this.__compressAddFile   .Click += new EventHandler(__compressAddFile_Click);
			this.__compressRemove    .Click += new EventHandler(__compressRemove_Click);
			this.__compressRootBrowse.Click += new EventHandler(__compressRootBrowse_Click);
			
			this.__decompresBrowse   .Click += new EventHandler(__decompresBrowse_Click);
			this.__decompress        .Click += new EventHandler(__decompress_Click);
		}
		
		private void ProgressEvent(object sender, ProgressEventArgs e) {
			
			this.Invoke( new MethodInvoker( delegate() {
				
				__status.Text    = e.Message;
				__progress.Value = e.Percentage;
				
			} ) );
		}
		
		
#region Compress
		
		private void __compress_Click(object sender, EventArgs e) {
			
			if( __sfd.ShowDialog(this) != DialogResult.OK ) return;
			
			String destFilename = __sfd.FileName;
			
			TarLzmaEncoder enc = new TarLzmaEncoder( __compressRoot.Text, false );
			enc.ProgressEvent += new EventHandler<ProgressEventArgs>(ProgressEvent);
			
			foreach(FileSystemInfo fsi in __items.Items) {
				
				DirectoryInfo di = fsi as DirectoryInfo;
				if(di != null) enc.AddDirectory( di );
				
				FileInfo fi = fsi as FileInfo;
				if(fi != null) enc.AddFile( fi );
				
				ProgressEvent(this, new ProgressEventArgs(0, fsi.FullName) );
			}
			
			enc.FinishAdding();
			
			if( __sfd.FilterIndex != 2 ) {
				
				__bw.DoWork += new System.ComponentModel.DoWorkEventHandler(__bw_DoWork_CompressTarLzma);
				__bw.RunWorkerAsync( new Object[] { enc, destFilename } );
				
			} else {
				
				__bw.DoWork += new System.ComponentModel.DoWorkEventHandler(__bw_DoWork_CompressTar);
				__bw.RunWorkerAsync( new Object[] { enc, destFilename } );
				
			}
			
		}
		
		private void __bw_DoWork_CompressTarLzma(object sender, System.ComponentModel.DoWorkEventArgs e) {
			
			Object[] args = e.Argument as Object[];
			
			TarLzmaEncoder enc = args[0] as TarLzmaEncoder;
			String        path = args[1] as String;
			
			enc.Compress( path );
			
			enc.Dispose();
			
			__bw.DoWork -= new System.ComponentModel.DoWorkEventHandler(__bw_DoWork_CompressTarLzma);
		}
		
		private void __bw_DoWork_CompressTar(object sender, System.ComponentModel.DoWorkEventArgs e) {
			
			Object[] args = e.Argument as Object[];
			
			TarLzmaEncoder enc = args[0] as TarLzmaEncoder;
			String        path = args[1] as String;
			
			enc.SaveTarball(path);
			
			enc.Dispose();
			
			__bw.DoWork -= new System.ComponentModel.DoWorkEventHandler(__bw_DoWork_CompressTar);
		}
		
		private void __compressRootBrowse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) __compressRoot.Text = __fbd.SelectedPath;
			
		}
		
		private void __compressRemove_Click(object sender, EventArgs e) {
			
			List<Object> itemsToRemove = new List<object>();
			
			foreach(Object item in __items.SelectedItems) itemsToRemove.Add( item );
			
			foreach(Object item in itemsToRemove)        __items.Items.Remove( item );
			
		}
		
		private void __compressAddFile_Click(object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) != DialogResult.OK ) return;
			
			FileInfo f = new FileInfo( __ofd.FileName );
			if( !f.Exists ) return;
			
			__items.Items.Add( f );
			
		}
		
		private void __compressAddDir_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) != DialogResult.OK ) return;
			
			DirectoryInfo d = new DirectoryInfo( __fbd.SelectedPath );
			if( !d.Exists ) return;
			
			__items.Items.Add( d );
		}
		
		
#endregion
		
		
#region Decompress
		
		private void __decompress_Click(object sender, EventArgs e) {
			
			if( !File.Exists( __decompressFilename.Text ) ) return;
			
			if( __fbd.ShowDialog(this) != DialogResult.OK ) return;
			
			TarLzmaDecoder dec = new TarLzmaDecoder( __decompressFilename.Text );
			dec.ProgressEvent += new EventHandler<ProgressEventArgs>(ProgressEvent);
			
			__bw.DoWork += new System.ComponentModel.DoWorkEventHandler(__bw_DoWork_Decompress);
			__bw.RunWorkerAsync( new Object[] { dec, __fbd.SelectedPath } );
		}
		
		private void __bw_DoWork_Decompress(object sender, System.ComponentModel.DoWorkEventArgs e) {
			
			Object[] args = e.Argument as Object[];
			
			TarLzmaDecoder dec = args[0] as TarLzmaDecoder;
			String        path = args[1] as String;
			
			dec.Extract( path );
			
			__bw.DoWork -= new System.ComponentModel.DoWorkEventHandler(__bw_DoWork_Decompress);
		}
		
		private void __decompresBrowse_Click(object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) != DialogResult.OK ) return;
			
			__decompressFilename.Text = __ofd.FileName;
			
		}
		
#endregion
		
	}
}
