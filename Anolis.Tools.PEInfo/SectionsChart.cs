using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Anolis.Tools.PEInfo {
	
	public partial class SectionsChart : UserControl {
		
		private UInt64 _imageLength;
		
		public SectionsChart() {
			InitializeComponent();
			
			ObservableCollection<Section> sections = new ObservableCollection<Section>();
			sections.CollectionChanged += new NotifyCollectionChangedEventHandler(sections_CollectionChanged);
			
			Sections = sections;
			
			this.DoubleBuffered = true;
		}
		
		private void sections_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			Refresh();
		}
		
		public UInt64 ImageLength {
			get { return _imageLength; }
			set { _imageLength = value; Invalidate(); }
		}
		
		public Collection<Section> Sections {
			get; private set;
		}
		
		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			
			Invalidate();
		}
		
		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			
			// Paint vertically, methinks; gives more room for information and is more readable imo
			Graphics g = e.Graphics;
			
			g.DrawRectangle( Pens.Black, 0, 0, Width - 1, Height - 1 );
			
			double pixelsPerByte = (double)Height / (double)ImageLength;
			
			const int chartWidth = 150;
			
			UInt64 lastSectionStop = 0;
			
			int i=0;
			foreach(Section s in Sections) {
				
				Brush b = new SolidBrush( s.Color );
				
				///////////////////////////////////////////
				// Chart
				
				if( s.Length > 0 ) {
					
					int y = (int)( s.Start * pixelsPerByte );
					int h = (int)( s.Length * pixelsPerByte );
					
					Rectangle rect = new Rectangle(0, y, chartWidth, h);
					
					g.FillRectangle( b, rect );
					
					UInt64 lenKiB = s.Length / 1024; if( lenKiB == 0 ) lenKiB = 1;
					String text = String.Format("{0} - {1}KB", s.Name, lenKiB );
					
					g.DrawString( text, SystemFonts.IconTitleFont, Brushes.White, 3, y + 2 );
					
				}
				
				///////////////////////////////////////////
				// Legend
				
				int sectionNameX   = chartWidth + 30;
				int sectionStartX  = sectionNameX + 100 + ( 100 - (int)g.MeasureString(        s.Start.ToString() + " by", SystemFonts.IconTitleFont ).Width ); // right-justify
				int sectionLengthX = sectionNameX + 200 + ( 100 - (int)g.MeasureString(       s.Length.ToString() + " by", SystemFonts.IconTitleFont ).Width ); // right-justify
				int gapX           = sectionNameX + 300 + (  50 - (int)g.MeasureString( s.Start - lastSectionStop + " by", SystemFonts.IconTitleFont ).Width ); // right-justify
				
				g.FillRectangle( b, new Rectangle( chartWidth + 10, i * 20 + 10, 10, 10 ) );
				g.DrawString(       s.Name                     , SystemFonts.IconTitleFont, SystemBrushes.ControlText, sectionNameX  , i * 20 + 5 );
				g.DrawString(       s.Start.ToString()  + " by", SystemFonts.IconTitleFont, SystemBrushes.ControlText, sectionStartX , i * 20 + 5 );
				g.DrawString(       s.Length.ToString() + " by", SystemFonts.IconTitleFont, SystemBrushes.ControlText, sectionLengthX, i * 20 + 5 );
				g.DrawString( s.Start - lastSectionStop + " by", SystemFonts.IconTitleFont, SystemBrushes.ControlText, gapX          , i * 20 + 5 );
				
				i++;
				
				b.Dispose();
				
				lastSectionStop = s.Start + s.Length;
			}
			
		}
	}
	
	public class Section {
		
		public String Name   { get; set; }
		public UInt64 Start  { get; set; }
		public UInt64 Length { get; set; }
		public Color  Color  { get; set; }
		
	}
	
	public static class PESectionColors {
		
		public static readonly Color Text  = Color.Black;
		public static readonly Color IText = Color.Maroon;
		public static readonly Color Data  = Color.Green;
		public static readonly Color Bss   = Color.Olive;
		public static readonly Color IData = Color.Navy;
		public static readonly Color EData = Color.Purple;
		public static readonly Color Tls   = Color.Teal;
		public static readonly Color RData = Color.Gray;
		public static readonly Color Reloc = Color.Black;
		public static readonly Color Rsrc  = Color.Maroon;
		
		public static Color GetColor(String name) {
			
			name = name.ToUpperInvariant();
			switch(name) {
				case ".TEXT" : return Text;
				case ".ITEXT": return IText;
				case ".DATA" : return Data;
				case ".BSS"  : return Bss;
				case ".IDATA": return IData;
				case ".EDATA": return EData;
				case ".TLS"  : return Tls;
				case ".RDATA": return RData;
				case ".RELOC": return Reloc;
				case ".RSRC" : return Rsrc;
				default:
					return Color.Black;
			}
			
		}
	}
	
}
