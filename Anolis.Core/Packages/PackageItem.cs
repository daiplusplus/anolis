using System;
using System.Xml;
using System.Drawing;

namespace Anolis.Core.Packages {
	
	public abstract class PackageItem {
		
		internal PackageItem(Package package, XmlElement itemElement) {
			
			Package     = package;
			
			Id          = itemElement.GetAttribute("id");
			Name        = itemElement.GetAttribute("name");
			Description = itemElement.GetAttribute("desc");
			
			String descImg = itemElement.GetAttribute("descImg");
			if(descImg.Length > 0) {
				
				String imgFilename = System.IO.Path.Combine( package.RootDirectory.FullName, descImg );
				
				Image img;
				if( package.PackageImages.TryGetValue( imgFilename, out img ) ) {
					
					DescriptionImage = img;
				} else {
					
					DescriptionImage = Image.FromFile( imgFilename  );
					
					try {
						img = Image.FromFile( imgFilename );
						
						package.PackageImages.Add( imgFilename, img );
					} catch(OutOfMemoryException) {
						package.PackageImages.Add( imgFilename, null );
					} catch(System.IO.FileNotFoundException) {
						package.PackageImages.Add( imgFilename, null );
					}
					
				}
				
			}
			
			String enabled = itemElement.GetAttribute("enabled");
			if(enabled.Length > 0) {
				Enabled = enabled != "false" && enabled != "0"; // with xs:boolean both "false" and "0" are valid values
			} else {
				Enabled = true;
			}
			
			
		}
		
		public String  Id               { get; protected set; }
		public String  Name             { get; protected set; }
		public String  Description      { get; protected set; }
		public Image   DescriptionImage { get; protected set; }
		public Boolean Enabled          { get; set; }
		
		public Package Package          { get; internal set; }
		
		public override String ToString() {
			
			return Name;
			
		}
		
	}
}
