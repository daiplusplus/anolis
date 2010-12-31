using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

#if QUANTIZE
using Anolis.Core.Utility.Quantization;
#endif

using N    = System.Globalization.NumberStyles;
using Cult = System.Globalization.CultureInfo;
using System.Runtime.InteropServices;

namespace Anolis.Core.Utility {
	
	public class CompositedImage {
		
		private Collection<CompositedLayer> _layers;
		
		public CompositedImage() {
			_layers = new Collection<CompositedLayer>();
		}
		
		/// <param name="compositionExpression">Must be in the form "comp:width,height[,bpp];Path\To\Image1.bmp,x,y[,width,height[,opacity]];Path\To\Image2.bmp,x,y[,opacity[,width,height]]"</param>
		public CompositedImage(String compositionExpression, DirectoryInfo root) : this() {
			
			if( compositionExpression == null ) throw new ArgumentNullException("compositionExpression");
			if( root                  == null ) throw new ArgumentNullException("root");
			
			if( !root.Exists ) throw new DirectoryNotFoundException("The root directory must exit: " + root.FullName);
			
#if QUANTIZE
			PixelFormat = PixelFormat.Format24bppRgb;
#endif
			
			ProcessExpression(compositionExpression, root);
		}
		
		private void ProcessExpression(String expr, DirectoryInfo root) {
			
			if( expr.StartsWith("comp:", StringComparison.OrdinalIgnoreCase) ) expr = expr.Substring(5);
			
			String[] layers = expr.Split(';');
			
			if( layers.Length < 2) throw new ArgumentException("compositionExpression was not formatted correctly or contains no images", "expr");
			
			///////////////////////////////////
			// Options
			String options = layers[0];
			String[] opts = options.Split(',');
			if( opts.Length < 2 ) throw new  ArgumentException("compositionExpression was not formatted correctly: too few options", "expr");
			
			String dxStr = opts[0];
			String dyStr = opts[1];
			
			Int32 dx, dy;
			
			if( !Int32.TryParse( dxStr, N.Integer, Cult.InvariantCulture, out dx ) ) throw new ArgumentException("Image X size must be an integer", "expr");
			if( !Int32.TryParse( dyStr, N.Integer, Cult.InvariantCulture, out dy ) ) throw new ArgumentException("Image Y size must be an integer", "expr");
			
			Dimensions = new Size( dx, dy );
			
			if( opts.Length >= 3 ) {
				
				String bpp = opts[2];
				
				if( !bpp.StartsWith("Format", StringComparison.OrdinalIgnoreCase) ) bpp = "Format" + bpp;
				
#if QUANTIZE
				try {
					PixelFormat = (PixelFormat)Enum.Parse( typeof(PixelFormat), bpp, true );
				} catch(ArgumentException) {
				}
#endif
				
			}
			
			///////////////////////////////////
			// Layers
			for(int i=1;i<layers.Length;i++) {
				
				String layerExpr = layers[i];
				
				CompositedLayer l = new CompositedLayer( layerExpr, root );
				
				_layers.Add( l );
			}
			
		}
		
		public Bitmap ToBitmap() {
			
			if( Dimensions.IsEmpty ) throw new InvalidOperationException("The specified image dimensions is invalid");
			
			/////////////////////////
			// Make the composited bitmap
			
#if QUANTIZE
			Bitmap bmp = new Bitmap( Dimensions.Width, Dimensions.Height );
#else
			Bitmap bmp = new Bitmap( Dimensions.Width, Dimensions.Height, PixelFormat.Format24bppRgb );
#endif
			using(Graphics g = Graphics.FromImage(bmp)) {
				
				for(int i=0;i<_layers.Count;i++) {
					
					CompositedLayer l = _layers[i];
					Rectangle rect;
					
					if( i == 0 ) {
						// use the color of the bottom-left pixel of the background layer as the background color
						Bitmap lbmp = l.Image as Bitmap;
						if( lbmp != null && lbmp.Height < bmp.Height ) {
							Color blp = lbmp.GetPixel( 0, lbmp.Height - 1 );
							g.FillRectangle( new SolidBrush(blp), 0, 0, lbmp.Width, lbmp.Height );
						}
						
					}
					
					if( l.Dimensions.IsEmpty ) {
						
						rect = new Rectangle( l.Location, l.Image.Size );
					} else {
						
						rect = new Rectangle( l.Location, l.Dimensions );
					}
					
					g.DrawImage( l.Image, rect );
					
				}
				
			}
			
#if QUANTIZE
			if( bmp.PixelFormat != PixelFormat ) {
				
				Bitmap ret;
				
				if( PixelFormat.IsIndexed() ) {
					// then it needs quantizing
					
					OctreeQuantizer q = CreateQuantizer(PixelFormat);
					if(q == null ) throw new AnolisException("Invalid PixelFormat");
					
					ret = q.Quantize( bmp );
					
				} else {
					
					ret = new Bitmap( bmp.Width, bmp.Height, PixelFormat );
					using(Graphics g = Graphics.FromImage(ret)) {
						
						Rectangle r = new Rectangle(0, 0, bmp.Width, bmp.Height );
						
						g.DrawImage( bmp, r );
					}
					
					
				}
				
				bmp.Dispose();
				
				return ret;
				
			}
#endif
			
			return bmp;
			
		}
		
#if QUANTIZE
		
		public PixelFormat PixelFormat { get; set; }
		
		private static OctreeQuantizer CreateQuantizer(PixelFormat format) {
			
			switch(format) {
				case PixelFormat.Format1bppIndexed:
					return new OctreeQuantizer(   1, 8);
				case PixelFormat.Format4bppIndexed:
					return new OctreeQuantizer(  15, 8 );
				case PixelFormat.Format8bppIndexed:
					return new OctreeQuantizer( 255, 8 );
				default:
					return null;
			}
			
		}
#endif
		
		public void Save(Stream stream, ImageFormat format) {
			
			if( format == null ) throw new ArgumentNullException("format");
			
			// it seems some problems are caused by the images lacking byte padding at the end to align to a DWORD boundary
			// so let's padd it out if necessary
			
//			Int64 startOffset = stream.Position;
			
			using(Bitmap bitmap = ToBitmap()) {
				
				bitmap.Save( stream, format );
			}
			
//			Int64 bitmapLength = stream.Position - startOffset;
//			
//			Int64 addBytes = bitmapLength % 4;
//			if( addBytes != 0 ) {
//				
//				for(int i=0;i<addBytes;i++)
//					stream.WriteByte( 0 );
//			}
//			
		}
		
		public void Save(String fileName, ImageFormat format) {
			
			if( fileName == null)  throw new ArgumentNullException("fileName");
			if( format   == null ) throw new ArgumentNullException("format");
			
			using(FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
				
				Save(fs, format);
			}
			
		}
		
		public Size Dimensions { get; set; }
		
		public Collection<CompositedLayer> Layers {
			get { return _layers; }
		}
		
		public override String ToString() {
			
			StringBuilder sb = new StringBuilder();
			sb.Append("comp:");
			
			// Options
			sb.Append( this.Dimensions.Width );
			sb.Append(",");
			sb.Append( this.Dimensions.Height );
			
			// Layers
			foreach(CompositedLayer layer in Layers) {
				sb.Append(";");
				sb.Append( layer.ToString() );
			}
			
			return sb.ToString();
		}
		
	}
	
	public class CompositedLayer {
		
		internal CompositedLayer(String layerExpression, DirectoryInfo root) {
			
			String[] components = layerExpression.Split(',');
			
			if( components.Length < 3 ) throw new ArgumentException("A layer expression must have at least 3 components", "layerExpression");
			
			// [0] = Path
			// [1] = X
			// [2] = Y
			// [3] = Width
			// [4] = Height
			// [5] = Opacity
			
			///////////////////////////
			// Image
			
			FileInfo file = root.GetFile( ImageRelativeFileName = components[0] );
			if( !file.Exists ) throw new FileNotFoundException("Layer Image file must exist", file.FullName );
			ImageFileName = file.FullName;
			
			///////////////////////////
			// Location
			String lxStr    = components[1];
			String lyStr    = components[2];
			
			Int32 lx, ly;
			
			if( !Int32.TryParse( lxStr, N.Integer, Cult.InvariantCulture, out lx ) ) throw new ArgumentException("Layer X coordinate must be an integer", "layerExpression");
			if( !Int32.TryParse( lyStr, N.Integer, Cult.InvariantCulture, out ly ) ) throw new ArgumentException("Layer Y coordinate must be an integer", "layerExpression");
			
			Location = new Point( lx, ly );
			
			///////////////////////////
			// Dimensions
			if( components.Length >= 5 ) {
				
				String dxStr = components[3];
				String dyStr = components[4];
				
				Int32 dx, dy;
				
				if( !Int32.TryParse( dxStr, N.Integer, Cult.InvariantCulture, out dx ) ) throw new ArgumentException("Layer X size must be an integer", "layerExpression");
				if( !Int32.TryParse( dyStr, N.Integer, Cult.InvariantCulture, out dy ) ) throw new ArgumentException("Layer Y size must be an integer", "layerExpression");
				
				Dimensions = new Size( dx, dy );
			} else {
				
				Dimensions = Size.Empty;
			}
			
			///////////////////////////
			// Opacity
			if( components.Length >= 6 ) {
				
				String oStr = components[5];
				Byte o;
				
				if( !Byte.TryParse( oStr, N.Integer, Cult.InvariantCulture, out o ) )  throw new ArgumentException("Layer opacity must be an integer", "layerExpression");
				
				Opacity = o;
			} else {
				
				Opacity = 255;
			}
			
		}
		
		private Image  _image;
		
		public Image Image {
			get {
				if( _image == null ) _image = GetImage( ImageFileName );
				return _image;
			}
			set {
				_image = value;
			}
		}
		
		/// <summary>Force-loads the image as 32bppARGB</summary>
		private static Image GetImage(String fileName) {
			
			Image file = Miscellaneous.ImageFromFile( fileName );
			
			if( file.PixelFormat != PixelFormat.Format32bppRgb ) return file; // we're only interested in 32bppRGB files, so 24bppRGB and 32bppARGB files can slip through okay
			
			Bitmap src = file as Bitmap;
			if( src == null ) return file;
			
			BitmapData srcData = src.LockBits( new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, src.PixelFormat );
			
			// don't use the Bitmap( width, height, stride, pixelformat, scan0 ) constructor because the BitmapData instance containing the scan0 cannot be freed until the constructed bitmap is first
			// (i.e. it isn't a copy constructor-of-sorts)
			
			Bitmap ret = new Bitmap( src.Width, src.Height, PixelFormat.Format32bppArgb );
			BitmapData retData = ret.LockBits( new Rectangle(0, 0, ret.Width, ret.Height), ImageLockMode.WriteOnly, ret.PixelFormat );
			
			// if there's a way to copy from one pointer to another without using unsafe code I'd like to hear it...
			Byte[] imageData = new Byte[ srcData.Stride * srcData.Height ];
			
			Marshal.Copy( srcData.Scan0, imageData, 0, imageData.Length );
			
			// and copy back to the new bitmap
			
			Marshal.Copy( imageData, 0, retData.Scan0, imageData.Length );
			
			src.UnlockBits( srcData );
			src.Dispose();
			
			ret.UnlockBits( retData );
			
			return ret;
		}
		
		public String ImageFileName { get; set; }
		public String ImageRelativeFileName { get; set; }
		
		public Point Location { get; set; }
		
		public Size  Dimensions { get; set; }
		
		public Byte  Opacity { get; set; }
		
		public static String MakeString(String path, Int32 coordX, Int32 coordY, Byte? opacity, Size dimensions) {
			
			StringBuilder sb = new StringBuilder();
			sb.Append( path );
			sb.Append(',');
			sb.Append( coordX );
			sb.Append(',');
			sb.Append( coordY );
			
			if( !dimensions.IsEmpty ) {
				sb.Append(',');
				sb.Append( dimensions.Width );
				sb.Append(',');
				sb.Append( dimensions.Height );
			}
			
			if( opacity != null && opacity < 255 ) {
				sb.Append(',');
				sb.Append( opacity.Value );
			}
			
			return sb.ToString();
			
		}
		
		public override String ToString() {
			
			return MakeString( this.ImageRelativeFileName, this.Location.X, this.Location.Y, this.Opacity, this.Dimensions );
			
		}
		
	}
	
}
