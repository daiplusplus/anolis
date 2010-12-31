using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using Anolis.Core.Utility;
using Anolis.Core;
using System.IO;

namespace Anolis.Tools.CompositedImages {
	
	// this class is used internally to assist in creating the XML package definition for xpize
	// feel free to use it if you can figure it out
	
	public class Program {
		
		const String _generatorMetaRoot = @"D:\Users\David\My Documents\Visual Studio Projects\Anolis\_resources\xpize\_source\Generator\";
		
		public static void Main(String[] args) {
			
			// Res2.csv contains all the information required:
			// PE filename
			// Foreground image: point and filesystem location
			// Dimensions of the completed image
			
			//RunOnceProgram.RunOnceMain(args);
			//OneOffGen.OneOffMain(args);
			//return;
			
			List<ImageItem> items = new List<ImageItem>();
			
			using(StreamReader rdr = new StreamReader( _generatorMetaRoot + "Res2.csv")) {
				
				String line;
				while( (line = rdr.ReadLine()) != null ) {
					
					String[] fields = line.Split(',');
					
					ImageItem item = new ImageItem();
					item.PEPath              = fields[0];
					item.ResName             = fields[1];
					item.SizeType            = GetSize( fields[2] );
					item.FinalDimensions     = new Size( Int32.Parse( fields[3] ), Int32.Parse( fields[4] ) );
					item.ForegroundImagePath = "Generator\\" + fields[5];
					item.ForegroundCoords    = new Point( Int32.Parse( fields[6] ), Int32.Parse( fields[7] ) );
					
					items.Add( item );
				}
			}
			//////////////////////////////////////////////////
			
			ColorGenerator[] gens = new ColorGenerator[] {
				new MceColorGenerator(),
				new MceBlackColorGenerator(),
				new ElementColorGenerator(),
				new BlueColorGenerator(),
				new EnergyColorGenerator()
			};
			
			foreach(ColorGenerator gen in gens) {
				
				using(StreamWriter wtr = new StreamWriter( _generatorMetaRoot + gen.GetType().Name + ".txt", false )) {
					
					String lastPE = null;
					
					foreach(ImageItem item in items) {
						
						if( item.PEPath != lastPE ) {
							wtr.WriteLine("\t\t\t</patch>");
							wtr.WriteLine("\t\t\t<patch path=\"" + item.PEPath + "\">" );
						}
						
						wtr.WriteLine("\t\t\t\t<res type=\"bitmap\" name=\"" + item.ResName + "\" src=\"" + gen.GetExpr( item ) + "\" />" );
						
						lastPE = item.PEPath;
					}
					
				}
				
			}
			
		}
		
		private static ColorGenerator CreateColorGenerator() {
			return new MceColorGenerator();
		}
		
		public static SizeType GetSize(String sizeType) {
			
			switch(sizeType) {
				case "Long" : return SizeType.Long;
				case "Small": return SizeType.Small;
				default:
					return SizeType.Side;
			}
		}
		
	}
	
	public abstract class ColorGenerator {
		
		protected virtual Size GetOriginalBGSize(SizeType type) {
			
			switch(type) {
				case SizeType.Long:
					return new Size( 126, 628 );
				case SizeType.Small:
					return new Size( 49, 49 );
				case SizeType.Side:
					return new Size( 164, 314 );
				default:
					throw new AnolisException("type");
			}
			
		}
		
		protected abstract String GetBGPath(SizeType type);
		
		public virtual String GetExpr(ImageItem item) {
			
			String opts = "comp:" + item.FinalDimensions.Width + ',' + item.FinalDimensions.Height;
			
			/////////////////////////////////////////////////////////
			// Background
			
			Size origBgSize = GetOriginalBGSize( item.SizeType );
			
			Single aspectRatio = (Single)origBgSize.Width / (Single)origBgSize.Height;
			Int32  newBgHeight  = (Int32)( (Single)item.FinalDimensions.Width / aspectRatio );
			
			String bgPath = GetBGPath( item.SizeType );
			if( bgPath != null ) {
				Size dimensions = new Size( item.FinalDimensions.Width, newBgHeight );
				opts += ";" + CompositedLayer.MakeString( bgPath, 0, 0, null, dimensions );
			}
			
			/////////////////////////////////////////////////////////
			// Foreground
			
			opts += ";" + CompositedLayer.MakeString( item.ForegroundImagePath, item.ForegroundCoords.X, item.ForegroundCoords.Y, null, Size.Empty );
			
			return opts;
		}
		
	}
	
	public class MceColorGenerator : ColorGenerator {
		
		const String pathToStripe  = @"MCE\GenStripe.png";
		
		protected override String GetBGPath(SizeType type) {
			
			switch(type) {
				case SizeType.Long:
					return @"MCE\GenLong.png";
				case SizeType.Side:
					return @"MCE\GenSide.png";
				case SizeType.Small:
					return @"MCE\GenSmall.png";
				default:
					throw new AnolisException("type");
			}
			
		}
		
		public override String GetExpr(ImageItem item) {
			
			String opts = base.GetExpr(item);
			
			//////////////////////////////////
			// Background Stripe (goes at the very front because sometimes the foreground spreads on to it)
			if( item.SizeType != SizeType.Small ) {
				
				opts += ";" + CompositedLayer.MakeString( pathToStripe, item.FinalDimensions.Width - 4, 0, null, Size.Empty );
			}
			
			return opts;
		}
	}
	
	public class MceBlackColorGenerator : ColorGenerator {
		
		const String pathToStripe  = @"Black\GenStripe.png";
		
		// Black MCE doesn't use a background for the 49x49 images
		
		protected override String GetBGPath(SizeType type) {
			
			switch(type) {
				case SizeType.Long:
					return @"Black\GenLong.png";
				case SizeType.Side:
					return @"Black\GenSide.png";
				case SizeType.Small:
					return null;
				default:
					throw new AnolisException("type");
			}
			
		}
		
		public override String GetExpr(ImageItem item) {
			
			String opts = base.GetExpr(item);
			
			//////////////////////////////////
			// Background Stripe (goes at the very front because sometimes the foreground spreads on to it)
			if( item.SizeType != SizeType.Small ) {
				
				opts += ";" + CompositedLayer.MakeString( pathToStripe, item.FinalDimensions.Width - 4, 0, null, Size.Empty );
			}
			
			return opts;
		}
	}
	
	public class EnergyColorGenerator : ColorGenerator {
		
		protected override Size GetOriginalBGSize(SizeType type) {
			
			switch(type) {
				case SizeType.Long:
					return new Size( 164, 628 );
				case SizeType.Small:
					return new Size( 49, 49 );
				case SizeType.Side:
					return new Size( 164, 314 );
				default:
					throw new AnolisException("type");
			}
			
		}
		
		protected override String GetBGPath(SizeType type) {
			
			switch(type) {
				case SizeType.Long:
					return @"Energy\GenLong.png";
				case SizeType.Side:
					return @"Energy\GenSide.png";
				case SizeType.Small:
					return @"Energy\GenSmall.png";
				default:
					throw new AnolisException("type");
			}
			
		}
		
		public override String GetExpr(ImageItem item) {
			
			String opts = base.GetExpr(item);
			
			// Energy Blue had its own reflection style, but that's too much effort :)
			
			return opts;
		}
	}
	
	public class ElementColorGenerator : ColorGenerator {
		
		const String pathToStripe  = @"Element\GenStripe.png";
		
		protected override Size GetOriginalBGSize(SizeType type) {
			
			switch(type) {
				case SizeType.Long:
					return new Size( 164, 628 );
				case SizeType.Small:
					return new Size( 49, 49 );
				case SizeType.Side:
					return new Size( 164, 314 );
				default:
					throw new AnolisException("type");
			}
			
		}
		
		protected override String GetBGPath(SizeType type) {
			
			switch(type) {
				case SizeType.Long:
					return @"Element\GenLong.png";
				case SizeType.Side:
					return @"Element\GenSide.png";
				case SizeType.Small:
					return @"Element\GenSmall.png";
				default:
					throw new AnolisException("type");
			}
			
		}
		
		public override String GetExpr(ImageItem item) {
			
			String opts = base.GetExpr(item);
			
			//////////////////////////////////
			// Background Stripe (goes at the very front because sometimes the foreground spreads on to it)
			if( item.SizeType != SizeType.Small ) {
				
				opts += ";" + CompositedLayer.MakeString( pathToStripe, item.FinalDimensions.Width - 3, 0, null, Size.Empty );
			}
			
			return opts;
		}
	}
	
	public class BlueColorGenerator : ColorGenerator {
		
		protected override Size GetOriginalBGSize(SizeType type) {
			
			switch(type) {
				case SizeType.Long:
					return new Size( 164, 628 );
				case SizeType.Small:
					return new Size( 49, 49 );
				case SizeType.Side:
					return new Size( 164, 314 );
				default:
					throw new AnolisException("type");
			}
			
		}
		
		protected override String GetBGPath(SizeType type) {
			
			switch(type) {
				case SizeType.Long:
					return @"Blue\GenLong.png";
				case SizeType.Side:
					return @"Blue\GenSide.png";
				case SizeType.Small:
					return @"Blue\GenSmall.png";
				default:
					throw new AnolisException("type");
			}
			
		}
		
		public override String GetExpr(ImageItem item) {
			
			String opts = base.GetExpr(item);
			
			
			
			return opts;
		}
	}
	
	public enum SizeType {
		Small,
		Side,
		Long
	}
	
	public class ImageItem {
		
		public String      PEPath;
		public String      ResName;
		
		public String      ForegroundImagePath;
		public Point       ForegroundCoords;
		
		public SizeType    SizeType;
		public Size        FinalDimensions;
		
		public String      Expr;
	}
	
}
