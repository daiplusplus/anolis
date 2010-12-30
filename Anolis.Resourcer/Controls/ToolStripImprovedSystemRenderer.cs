using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using System.Reflection;
using System.IO;
using Microsoft.Win32;

namespace Anolis.Resourcer.Controls {
	
	public class ToolStripImprovedSystemRenderer : ToolStripSystemRenderer {
		
		// Shame a lot of the methods are private rather than protected.
		// Here's to Lutz Roeder's Reflector
		
		private Boolean _aero;
		
		public ToolStripImprovedSystemRenderer() {
			
			_aero = IsUsingAero();
			
			SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(SystemEvents_UserPreferenceChanged);
		}
		
		private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e) {
			
			_aero = IsUsingAero();
		}
		
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
			
			ToolStrip toolStrip = e.ToolStrip;
			if( toolStrip is BindingNavigator || toolStrip is MenuStrip || toolStrip is StatusStrip || toolStrip is ToolStripDropDown ) {
				base.OnRenderToolStripBorder(e);
				return;
			}
			
			if( _aero ) {
				
				Pen topBorder = new Pen( Color.FromArgb( 250, 250, 250 ) );
				Pen botBorder = new Pen( Color.FromArgb( 169, 185, 202 ) );
				
				Point tl = new Point( 0                         ,                           0 );
				Point tr = new Point( e.AffectedBounds.Width - 1,                           0 );
				Point bl = new Point( 0                         , e.AffectedBounds.Height - 1 );
				Point br = new Point( e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1 );
				
				e.Graphics.DrawLine( topBorder, tl, tr );
				
				e.Graphics.DrawLine( botBorder, bl, br );
				
				topBorder.Dispose();
				botBorder.Dispose();
				
			} else {
				
				base.OnRenderToolStripBorder(e);
			}
				
		}
		
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) {
			
			ToolStrip toolStrip      = e.ToolStrip;
			Graphics  g              = e.Graphics;
			Rectangle affectedBounds = e.AffectedBounds;
			
			if (ShouldPaintBackground(toolStrip)) {
				
				if (toolStrip is StatusStrip) {
					
					RenderStatusStripBackground(e);
					
				} else if (DisplayInformation_HighContrast) {
					
					FillBackground(g, affectedBounds, SystemColors.ButtonFace);
					
				} else if (DisplayInformation_LowResolution) {
					
					FillBackground(g, affectedBounds, (toolStrip is ToolStripDropDown) ? SystemColors.ControlLight : e.BackColor);
					
				} else if (toolStrip.IsDropDown) {
					
					FillBackground(g, affectedBounds, !ToolStripManager.VisualStylesEnabled ? e.BackColor : SystemColors.Menu);
					
				} else if (toolStrip is MenuStrip) {
					
					FillBackground(g, affectedBounds, !ToolStripManager.VisualStylesEnabled ? e.BackColor : SystemColors.MenuBar);
					
				} else if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.Band.Normal)) {
					
//					VisualStyleRenderer visualStyleRenderer = VisualStyleRenderer;
//					//visualStyleRenderer.SetParameters(VisualStyleElement_ToolBar_Bar_Normal);
//					visualStyleRenderer.SetParameters(VisualStyleElement.Rebar.Band.Normal); // this is not implemented in WinForms, for shame. Refer to RebarRenderer
//					visualStyleRenderer.DrawBackground(g, affectedBounds);
					
					if( _aero ) {
						
						// the Aero toolbar background image is ugly, ugly, ugly
						// so here's a Camifox-inspired background gradient
						
						// unfortunately the gradient makes use of midpoints, which GDI doesn't support
						// so use a PNG
						
						Bitmap aeroBg = Resources.Toolbar_Aero;
						
						Rectangle affectedBoundsSameHeight = affectedBounds;
						affectedBoundsSameHeight.Height = aeroBg.Height;
						
						g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
//						g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
						g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
						g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
						g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
						
						g.DrawImage( aeroBg, affectedBoundsSameHeight );
						
					} else {
						
						RebarRenderer.DrawBackground(g, affectedBounds);
					}
					
				} else {
					FillBackground(g, affectedBounds, !ToolStripManager.VisualStylesEnabled ? e.BackColor : SystemColors.MenuBar);
				}
			}
		}
		
		private VisualStyleRenderer _rebarRenderer;
		
		protected VisualStyleRenderer RebarRenderer {
			get {
				if(_rebarRenderer == null) {
					_rebarRenderer = new VisualStyleRenderer("Rebar", 0, 0); // Microsoft thoughtlessly ommitted this from VisualStyleElement
				}
				return _rebarRenderer;
			}
		}
		
		protected static void FillBackground(Graphics g, Rectangle bounds, Color backColor) {
			
			if (backColor.IsSystemColor) {
				
				g.FillRectangle(SystemBrushes.FromSystemColor(backColor), bounds);
				
			} else {
				
				using (Brush brush = new SolidBrush(backColor)) {
					g.FillRectangle(brush, bounds);
				}
			}
		}
		
		protected static void RenderStatusStripBackground(ToolStripRenderEventArgs e) {
			
			if (Application.RenderWithVisualStyles) {
				
				VisualStyleRenderer visualStyleRenderer = VisualStyleRenderer;
				visualStyleRenderer.SetParameters(VisualStyleElement.Status.Bar.Normal);
				visualStyleRenderer.DrawBackground(e.Graphics, new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1));
				
			} else if (!SystemInformation_InLockedTerminalSession()) {
				
				e.Graphics.Clear(e.BackColor);
			}
		}
		
		public static Boolean IsUsingAero() {
			
			Type vsiType = typeof(VisualStyleInformation);
			PropertyInfo pi;
			
			try {
				pi = vsiType.GetProperty("ThemeFilename", BindingFlags.Static | BindingFlags.NonPublic, null, typeof(String), new Type[0], null);
			} catch(AmbiguousMatchException) {
				return false;
			}
			
			if( pi == null ) return false;
			
			String fileName = (String)pi.GetValue(null, null);
			
			if( String.IsNullOrEmpty( fileName ) ) return false;
			
			fileName = Path.GetFileName( fileName );
			
			return String.Equals( "aero.msstyles", fileName, StringComparison.OrdinalIgnoreCase );
		}
		
#region Reflected Properties
		
		private MethodInfo _shouldPaintBackground;
		
		protected Boolean ShouldPaintBackground(Control control) {
			
			if(_shouldPaintBackground == null) {
				Type t = typeof(System.Windows.Forms.ToolStripRenderer);
				_shouldPaintBackground = t.GetMethod("ShouldPaintBackground", BindingFlags.NonPublic | BindingFlags.Instance);
			}
			
			return (Boolean)_shouldPaintBackground.Invoke(this, new Object[] { control } );
		}
		
		private static PropertyInfo _visualStyleRenderer;
		
		protected static VisualStyleRenderer VisualStyleRenderer {
			get {
				
				if(_visualStyleRenderer == null) {
					Type t = typeof(System.Windows.Forms.ToolStripSystemRenderer);
					_visualStyleRenderer = t.GetProperty("VisualStyleRenderer", BindingFlags.Static | BindingFlags.NonPublic);
				}
				
				return (VisualStyleRenderer)_visualStyleRenderer.GetValue(null, null);
			}
		}
		
		private static PropertyInfo _displayInformation_HighContrast;
		
		protected static Boolean DisplayInformation_HighContrast {
			get {
				
				if(_displayInformation_HighContrast == null) {
					Type t = Type.GetType("System.Windows.Forms.DisplayInformation, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					_displayInformation_HighContrast = t.GetProperty("HighContrast", BindingFlags.Static | BindingFlags.Public );
				}
				
				Object retval = _displayInformation_HighContrast.GetValue(null, null);
				return (Boolean)retval;
			}
		}
		
		private PropertyInfo _displayInformation_LowResolution;
		
		protected Boolean DisplayInformation_LowResolution {
			get {
				
				if(_displayInformation_LowResolution == null) {
					Type t = Type.GetType("System.Windows.Forms.DisplayInformation, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					_displayInformation_LowResolution = t.GetProperty("LowResolution", BindingFlags.Static | BindingFlags.Public );
				}
				
				Object retval = _displayInformation_LowResolution.GetValue(null, null);
				return (Boolean)retval;
			}
		}
		
		private static MethodInfo _systemInformation_InLockedTerminalSession;
		
		protected static Boolean SystemInformation_InLockedTerminalSession() {
			
			if(_systemInformation_InLockedTerminalSession == null) {
				Type t = typeof(System.Windows.Forms.SystemInformation);
				_systemInformation_InLockedTerminalSession = t.GetMethod("InLockedTerminalSession", BindingFlags.Static | BindingFlags.NonPublic);
			}
			
			Object retval = _systemInformation_InLockedTerminalSession.Invoke(null, null);
			return (Boolean)retval;
			
		}
		
		private static PropertyInfo _visualStyleElement_ToolBar_Bar_Normal;
		
		protected static VisualStyleElement VisualStyleElement_ToolBar_Bar_Normal {
			get {
				
				if(_visualStyleElement_ToolBar_Bar_Normal == null) {
					Type t = Type.GetType("System.Windows.Forms.VisualStyles.VisualStyleElement+ToolBar+Bar, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					_visualStyleElement_ToolBar_Bar_Normal = t.GetProperty("Normal", BindingFlags.Static | BindingFlags.Public );
				}
				
				Object retval = _visualStyleElement_ToolBar_Bar_Normal.GetValue(null, null);
				return (VisualStyleElement)retval;
			}
		}
		
#endregion
		
	}
}
