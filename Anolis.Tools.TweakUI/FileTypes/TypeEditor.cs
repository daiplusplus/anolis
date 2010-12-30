using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using Anolis.Packages.Utility;

namespace Anolis.Tools.TweakUI.FileTypes {
	
	public partial class TypeEditor : UserControl {
		
		public TypeEditor() {
			InitializeComponent();
		}
		
		public void ShowFileType(FileType type) {
			
			this.__progId      .Text = type.ProgId;
			this.__friendlyName.Text = type.FriendlyName;
			this.__iconPath    .Text = type.DefaultIcon;
			
			Image old = this.__icon.Image;
			if( old != null ) old.Dispose();
			
			this.__icon.Image = GetIconImage( type.DefaultIcon );
			
			__verbs.Items.Clear();
			foreach(String verb in type.ShellVerbs.Keys) {
				
				String line = verb + @" \ " + type.ShellVerbs[verb];
				
				__verbs.Items.Add( line );
			}
			
		}
		
		private static Bitmap GetIconImage(String defaultIconExpr) {
			
			if( String.IsNullOrEmpty( defaultIconExpr ) ) return null;
			
			String path = defaultIconExpr;
			
			String idxStr = "";
			
			Int32 commaIdx = defaultIconExpr.LastIndexOf(',');
			if( commaIdx > 0 ) {
				idxStr = path.Substring( commaIdx + 1 );
				path   = path.Substring( 0, commaIdx );
			}
			
			Icon ico;
			
			if( idxStr.Length > 0 ) {
				
				Int32 idx = -1;
				if( !Int32.TryParse( idxStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out idx ) ) {
					return null;
				}
				
				ico = NativeMethods.ExtractIcon( path, idx );
				
				if( ico == null ) {
					return null;
				}
				
			} else if( IsValidIconPath( path ) ) {
				
				try {
					
					ico = new Icon( path );
					
				} catch(IOException) {
					return null;
				}
				
			} else {
				
				return null;
			}
			
			if( ico.Width == 0 || ico.Height == 0 ) {
				DestroyIcon( ico );
				return null;
			}
			
			Bitmap ret = ico.ToBitmap();
			
			DestroyIcon( ico );
			
			return ret;
			
		}
		
		private static void DestroyIcon(Icon icon) {
			
			// this is needed because Icon.Dispose doesn't Destroy its handle if it was made .FromHandle
			
			IntPtr handle = icon.Handle;
			icon.Dispose();
			NativeMethods.DestroyIcon( handle );
			
		}
		
		private static Boolean IsValidIconPath(String path) {
			
			Char[] invalid = Path.GetInvalidPathChars();
			if( path.IndexOfAny( invalid ) >= 0 ) {
				return false;
			}
			
			return Path.IsPathRooted( path ) && File.Exists( path );
		}
		
	}
}
