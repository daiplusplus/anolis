using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Utility;
using System.Xml;

namespace Anolis.Packager {
	
	public partial class OptimizerForm : Form {
		
		private PackageOptimizer _opt;
		
		private ProgressForm _prog = new ProgressForm();
		
		public OptimizerForm() {
			
			InitializeComponent();
			
			this.__loadBrowse.Click += new EventHandler(__loadBrowse_Click);
			this.__destBrowse.Click += new EventHandler(__destBrowse_Click);
			this.__load  .Click += new EventHandler(__load_Click);
			this.__dest  .Click += new EventHandler(__dest_Click);
			
			this.__compImages.SelectedIndexChanged += new EventHandler(__compImages_SelectedIndexChanged);
			this.__compLayers.SelectedIndexChanged += new EventHandler(__compLayers_SelectedIndexChanged);
			
			this.__bwLoad.DoWork             += new DoWorkEventHandler            (__bwLoad_DoWork);
			this.__bwLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(__bwLoad_RunWorkerCompleted);
			this.__bwLoad.ProgressChanged    += new ProgressChangedEventHandler   (__bwLoad_ProgressChanged);
			
			this.__bwDest.DoWork             += new DoWorkEventHandler            (__bwDest_DoWork);
			this.__bwDest.RunWorkerCompleted += new RunWorkerCompletedEventHandler(__bwDest_RunWorkerCompleted);
			this.__bwDest.ProgressChanged    += new ProgressChangedEventHandler   (__bwDest_ProgressChanged);
		}
		
#region Load
		
	#region BW
		
		private void __bwLoad_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			
			_prog.Status = e.UserState as String;
			_prog.SetProgress( e.ProgressPercentage, e.ProgressPercentage, 100 );
			
		}
		
		private void __bwLoad_DoWork(Object sender, DoWorkEventArgs e) {
			
			Triple<String[],String[],String[]> ret = LoadOptimizer( (String)e.Argument );
			
			e.Result = ret;
		}
		
		private void __bwLoad_RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e) {
			
			Triple<String[],String[],String[]> ret = e.Result as Triple<String[],String[],String[]>;
			String[] messages     = ret.X;
			String[] missing      = ret.Y;
			String[] unreferenced = ret.Z;
			
			/////////////////////
			// Messages
			
			__messages.Items.Clear();
			__messages.Items.AddRange( messages );
			
			/////////////////////
			// Missing and Unreferenced Files
			
			__missingFiles.Items.Clear();
			__missingFiles.Items.AddRange( missing );
			
			__unreferencedFiles.Items.Clear();
			__unreferencedFiles.Items.AddRange( unreferenced );
			
			/////////////////////
			// Composited Images
			
			List<CompositedImage> images = _opt.CompositedImages;
			__compImages.Items.AddRange( images.ToArray() );
			
			/////////////////////
			// Duplicate Files
			
			DuplicateFinder finder = _opt.GetDuplicateFilesFinder();
			String rootDirectory = Path.GetDirectoryName( __fileName.Text );
			
			__duplicateFiles.BeginUpdate();
			foreach(DuplicateFile set in finder.DuplicateFiles) {
				
				ListViewGroup group = new ListViewGroup( set.Hash );
				__duplicateFiles.Groups.Add( group );
				
				foreach(FileInfo file in set.Matches) {
					
					String relativeName = file.FullName.Substring( rootDirectory.Length );
					ListViewItem item = __duplicateFiles.Items.Add( relativeName );
					item.Group = group;
				}
				
			}
			__duplicateFiles.EndUpdate();
			
			_prog.Visible = false;
			this.BringToForeground();
			
			
		}
		
	#endregion
		
		private void __loadBrowse_Click(object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) == DialogResult.OK ) {
				
				__fileName.Text = __ofd.FileName;
			}
		}
		
		private void __load_Click(object sender, EventArgs e) {
			
			__bwLoad.RunWorkerAsync( __fileName.Text );
			
			_prog.ShowDialog(this);
		}
		
		private Triple<String[],String[],String[]> LoadOptimizer(String fileName) {
			
			__bwLoad.ReportProgress(-1, "Loading");
			
			_opt = new PackageOptimizer( __fileName.Text );
			List<String> messages = _opt.LoadAndValidate();
			
			String[] missing, unreferenced;
			
			__bwLoad.ReportProgress(-1, "Loading Missing and Unreferenced Files");
			
			/////////////////////////
			// Missing and Unreferenced Files
			
			_opt.GetFiles(out missing, out unreferenced);
			
			/////////////////////////
			// Duplicate Files
			
			__bwLoad.ReportProgress(-1, "Loading Duplicate Files");
			
			DuplicateFinder finder = _opt.GetDuplicateFilesFinder();
			finder.StatusUpdated += new EventHandler(finder_StatusUpdated);
			
			finder.Search();
			
			return new Triple<String[],String[],String[]>( messages.ToArray(), missing, unreferenced );
		}
		
		private void finder_StatusUpdated(object sender, EventArgs e) {
			
			if( !__bwLoad.IsBusy ) return;
			
			DuplicateFinder finder = sender as DuplicateFinder;
			
			__bwLoad.ReportProgress( finder.StatusPercentage, finder.StatusMessage );
		}
		
#endregion
		
		
#region Export
		
	#region BW
		
		private void __bwDest_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			
			_prog.Status = e.UserState as String;
			_prog.SetProgress( e.ProgressPercentage, e.ProgressPercentage, 100 );
			
		}
		
		private void __bwDest_DoWork(Object sender, DoWorkEventArgs e) {
			
			DirectoryInfo origRoot = new DirectoryInfo( Path.GetDirectoryName( __fileName.Text ) );
			DirectoryInfo destRoot = new DirectoryInfo( __destPath.Text );
			
			__bwDest.ReportProgress(0, "Copying Files");
			
			origRoot.CopyTo( destRoot.FullName );
			
			///////////////////////////
			// Load the copied package
			
			String packageXmlFileName = Path.Combine( __destPath.Text, Path.GetFileName( __fileName.Text ) );
			
			__bwDest.ReportProgress(30, "Identifying Duplicate Files");
			
			PackageOptimizer opt = new PackageOptimizer( packageXmlFileName );
			List<String> messages = opt.LoadAndValidate();
			if( messages.Count > 0 ) {
				e.Result = messages;
				return;
			}
			
			DuplicateFinder finder = opt.GetDuplicateFilesFinder();
			finder.StatusUpdated += new EventHandler(finderDest_StatusUpdated);
			
			finder.Search();
			
			DirectoryInfo moveDupesTo = destRoot.GetDirectory("_duplicates");
			if( !moveDupesTo.Exists ) moveDupesTo.Create();
			
			__bwDest.ReportProgress(60, "Process and Saving Package.xml");
			
			finder.ProcessPackage( opt.Document );
			
			/////////////////////////
			// Save the updated package xml
			
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.NewLineChars = "\r\n";
			settings.IndentChars = "\t";
			
			XmlWriter wtr = XmlWriter.Create( packageXmlFileName, settings );
			opt.Document.Save( wtr );
			
			__bwDest.ReportProgress(60, "Moving Duplicate Files");
			
			finder.SeparateOutDuplicateFiles( moveDupesTo );
			
			__bwDest.ReportProgress(100, "Complete");
			
		}
		
		private void __bwDest_RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e) {
			
			_prog.Visible = false;
			
			List<String> messages = e.Result as List<String>;
			if( messages != null ) {
				
				MessageBox.Show(this, "Could not process package: there were validation errors", "Anolis Packager", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
			}
			
		}
		
		private void finderDest_StatusUpdated(object sender, EventArgs e) {
			
			if( !__bwDest.IsBusy ) return;
			
			DuplicateFinder finder = sender as DuplicateFinder;
			
			__bwDest.ReportProgress( finder.StatusPercentage, finder.StatusMessage );
		}
		
	#endregion
		
		private void __destBrowse_Click(object sender, EventArgs e) {
			
			if( __fbd.ShowDialog(this) == DialogResult.OK ) {
				
				__destPath.Text = __fbd.SelectedPath;
			}
		}
		
		private void __dest_Click(object sender, EventArgs e) {
			
			__bwDest.RunWorkerAsync( __destPath.Text );
			
			_prog.ShowDialog(this);
		}
		
#endregion
		
#region Composited Images
		
		private CompositedImage _currentImage;
		
		private void __compImages_SelectedIndexChanged(object sender, EventArgs e) {
			
			if( __compImages.SelectedIndex == -1 ) {
				
				LoadImage( null );
			} else {
				
				CompositedImage image = (CompositedImage)__compImages.Items[ __compImages.SelectedIndex ];
				
				LoadImage( image );
			}
			
		}
		
		private void LoadImage(CompositedImage image) {
			
			__compLayers.Items.Clear();
			
			_currentImage = image;
			
			if( image == null ) {
				
				LoadLayer(null);
				
			} else {
				
				__compLayers.Items.Add("Final Image");
				
				foreach(Layer layer in image.Layers) {
					
					__compLayers.Items.Add( layer );
				}
				
				__compLayers.SelectedIndex = 0;
				
			}
			
		}
		
		private void __compLayers_SelectedIndexChanged(object sender, EventArgs e) {
			
			if( __compLayers.SelectedIndex == -1 ) {
				
				LoadImage( null );
				
			} else {
				
				Object obj = __compLayers.Items[ __compLayers.SelectedIndex ];
				if( obj is String ) {
					
					LoadFinalImage( _currentImage );
					
				} else {
					
					Layer layer = (Layer)obj;
				
					LoadLayer( layer );
				}
				
				
			}
			
		}
		
		private void LoadFinalImage(CompositedImage image) {
			
			__compPreview.Image = image.ToBitmap();
			
		}
		
		private void LoadLayer(Layer layer) {
			
			if( layer == null ) {
				
				__compPreview.Image = null;
				
			} else {
				
				__compPreview.Image = layer.Image;
				
			}
			
		}
		
#endregion
		
	}
}
