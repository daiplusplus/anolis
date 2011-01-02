using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Tools.PEInfo {
	
	public partial class PEView : UserControl {
		
		private PEFile _pe;
		
		public PEView() {
			InitializeComponent();
		}
		
		public PEFile PEFile {
			get { return _pe; }
			set {
				_pe = value;
				if( value == null ) return;
				
				__sections.ImageLength = _pe.ImageLength;
				
				__sections.Sections.Clear();
				
				foreach(SectionTableEntry section in _pe.SectionTable) {
					
					Section s = new Section() {
						Name   = section.NameString,
						Start  = section.PointerToRawData,
						Length = section.SizeOfRawData,
						Color  = PESectionColors.GetColor( section.NameString )
					};
					
					__sections.Sections.Add( s );
					
				}
				
			}
		}
		
	}
}
