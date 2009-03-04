using System;
using System.Xml;

namespace Anolis.Core.Packages {
	
	public abstract class PackageItem {
		
		internal PackageItem(XmlElement itemElement) {
			
			Id          = itemElement.GetAttribute("id");
			Name        = itemElement.GetAttribute("name");
			Description = itemElement.GetAttribute("desc");
			
			String descImg = itemElement.GetAttribute("descImg");
			if(descImg.Length > 0) {
				DescriptionImage = descImg;
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
		public String  DescriptionImage { get; protected set; }
		public Boolean Enabled          { get; set; }
		
		public override String ToString() {
			
			return Name;
			
		}
		
	}
}
