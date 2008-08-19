using System;
using System.Drawing;
using System.Xml;
using System.Text;
using System.IO;

namespace Anolis.Core {
	
	public class PackageSet {
		
	}
	
	public class Package {
		
		// Metadata
		private Bitmap _icon;
		private Bitmap _image;
		private String _text;
		
		public Bitmap Icon { get { return _icon; } }
		public Bitmap Image { get { return _image; } }
		public String Text { get { return _text; } }
		
		public Package(Stream stream) {
			
			
			
		}
		
	}
	
	public class File {
		
	}
	
	public class Extra {
		
	}
	
}
