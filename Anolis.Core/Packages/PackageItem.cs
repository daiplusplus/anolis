using System;
using System.Collections.Generic;
using System.Xml;
using System.Drawing;

using Anolis.Core.Utility;

namespace Anolis.Core.Packages {
	
	public abstract class PackageBase {
		
		public Package    Package   { get; internal set; }
		
		public String     Id        { get; set; }
		public String     Name      { get; set; }
		
		public Expression Condition { get; set; }
		
		protected PackageBase(Package package, XmlElement element) {
			
			Package = package;
			
			Id      = element.GetAttribute("id");
			Name    = element.GetAttribute("name");
			
			String conditionExpr = element.GetAttribute("condition");
			if(conditionExpr.Length > 0)
				Condition = new Expression( conditionExpr );
			
		}
		
		protected PackageBase(Package package) {
			Package = package;
		}
		
		protected EvaluationResult Evaluate(Dictionary<String,Double> symbols) {
			
			if( Condition == null ) return EvaluationResult.True;
			
			try {
				Double result = Condition.Evaluate( symbols );
				
				if(result == 1) return EvaluationResult.True;
				return EvaluationResult.False;
				
			} catch(ExpressionException ex) {
				
				if( Package != null ) Package.Log.Add( LogSeverity.Warning, "Expression evaluation exception: " + ex.Message );
				
				return EvaluationResult.Error;
			}
		}
		
	}
	
	public enum EvaluationResult {
		True,
		False,
		Error
	}
	
	public abstract class PackageItem : PackageBase {
		
		protected PackageItem(Package package, Group parent, XmlElement itemElement) : base(package, itemElement) {
			
			ParentGroup          = parent;
			
			if( parent != null )
				if( parent.Package != package ) throw new ArgumentException("parent must be a child of the root package");
			
			
			Description          = itemElement.GetAttribute("desc");
			DescriptionImagePath = itemElement.GetAttribute("descImg");
			
			String enabled = itemElement.GetAttribute("enabled");
			if(enabled.Length > 0) {
				Enabled = enabled != "false" && enabled != "0"; // with xs:boolean both "false" and "0" are valid values
			} else {
				Enabled = true;
			}
			
		}
		
		protected PackageItem(Package package, Group parent) : base(package) {
			
			ParentGroup = parent;
		}
		
		public    String  Description          { get; set; }
		protected String  DescriptionImagePath { get; set; }
		
		/// <summary>Whether the item is enabled or disabled. If Disabled it will not be executed, but even if Enabled it may not be executed, see IsEnabled.</summary>
		public    Boolean Enabled              { get; set; }
		
		public    Group   ParentGroup          { get; internal set; }
		
		public override String ToString() {
			
			return Name;
		}
		
		private Image _descImage;
		
		public Image DescriptionImage {
			get {
				
				if( _descImage == null && DescriptionImagePath.Length > 0 ) {
					
					String imgFilename = System.IO.Path.Combine( Package.RootDirectory.FullName, DescriptionImagePath );
					
					Image img;
					if( Package.PackageImages.TryGetValue( imgFilename, out img ) ) {
						
						_descImage = img;
						
					} else {
						
						_descImage = Image.FromFile( imgFilename  );
						
						try {
							img = Image.FromFile( imgFilename );
							
							Package.PackageImages.Add( imgFilename, img );
						} catch(OutOfMemoryException) {
							Package.PackageImages.Add( imgFilename, null );
						} catch(System.IO.FileNotFoundException) {
							Package.PackageImages.Add( imgFilename, null );
						}
						
					}
					
				}
				
				return _descImage;
				
			}
		}
		
		public abstract void Write(XmlElement parent);
		
		/// <summary>Creates a PackageItem XML element, prepopulates it with the common attributes, and appends it to the parent element's children.</summary>
		protected XmlElement CreateElement(XmlElement parent, String name, params String[] attributes) {
			
			XmlElement element = CreateElement(parent, name);
			
			if( attributes.Length % 2 != 0 ) throw new ArgumentException("attributes must be in pairs");
			
			for(int i=0;i<attributes.Length;i+=2) {
				
				String attName = attributes[i];
				String attValu = attributes[i+1];
				
				AddAttribute(element, attName, attValu);
			}
			
			AddAttribute(element, "id"     , this.Id );
			AddAttribute(element, "name"   , this.Name );
			AddAttribute(element, "desc"   , this.Description );
			AddAttribute(element, "descImg", this.DescriptionImagePath );
			
			
			
			return element;
			
		}
		
		protected static XmlElement CreateElement(XmlElement parent, String name) {
			
			XmlElement element = parent.OwnerDocument.CreateElement( name );
			parent.AppendChild( element );
			
			return element;
		}
		
		protected internal static void AddAttribute(XmlElement element, String name, String value) {
			
			if( String.IsNullOrEmpty( value ) ) return;
			
			XmlAttribute att = element.OwnerDocument.CreateAttribute( name );
			att.Value = value;
			
			element.Attributes.Append( att );
			
		}
		
	}
}
