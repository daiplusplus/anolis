using System;
using System.Collections.Generic;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;

using Anolis.Core.Utility;
using Cult = System.Globalization.CultureInfo;
using Env  = Anolis.Core.Utility.Environment;
using N    = System.Globalization.NumberStyles;
using System.IO;

namespace Anolis.Packages {
	
	public enum EvaluationResult {
		True,
		False,
		FalseParent,
		Error
	}
	
	public abstract class PackageItem : PackageBase {
		
		protected PackageItem(Package package, Group parent, XmlElement itemElement) : base(package, itemElement) {
			
			ParentGroup          = parent;
			
			if( parent != null )
				if( parent.Package != package ) throw new ArgumentException("parent must be a child of the root package");
			
			
			Description          = itemElement.GetAttribute("desc");
			DescriptionImagePath = itemElement.GetAttribute("descImg");
			PreviewImagePath     = itemElement.GetAttribute("previewImg");
			Hidden               = itemElement.GetAttribute("hidden") == "true" || itemElement.GetAttribute("hidden") == "1";
			
			String enabled       = itemElement.GetAttribute("enabled"); // since Enabled is nullable, set it to a value only when explicitly defined
			if( enabled == "true"  || enabled == "1" ) Enabled = true;
			if( enabled == "false" || enabled == "0" ) Enabled = false;
			
		}
		
		protected PackageItem(Package package, Group parent) : base(package) {
			
			ParentGroup = parent;
		}
		
		public          String  Description          { get; set; }
		protected       String  DescriptionImagePath { get; set; }
		protected       String  PreviewImagePath     { get; set; }
		
		/// <summary>Whether the item is enabled or disabled. If Disabled it will not be executed, but even if Enabled it may not be executed, see IsEnabled.</summary>
		public          Boolean? Enabled       { get; set; }
		public          Boolean Hidden         { get; set; }
		public          Boolean IsEnabled      {
			get {
				if( Enabled     != null ) return Enabled.Value;
				if( ParentGroup == null ) return true;
				return ParentGroup.IsEnabled;
			}
		}
		
		public          Group   ParentGroup          { get; internal set; }
		
#region Evaluation
		
		public override EvaluationResult Evaluate() {
			
			if( ParentGroup != null ) {
				
				EvaluationResult result = ParentGroup.Evaluate();
				if( result == EvaluationResult.False || result == EvaluationResult.FalseParent ) return EvaluationResult.FalseParent;
				if( result != EvaluationResult.True ) return result;
			}
			
			if( Condition == null ) return EvaluationResult.True;
			
			return EvaluateCondition( GetSymbols() );
		}
		
#endregion
		
		public override String ToString() {
			
			return Name;
		}
		
		private Image _descImage;
		
		public Image DescriptionImage {
			get {
				
				if( _descImage == null && DescriptionImagePath.Length > 0 ) {
					
					_descImage = GetImage( DescriptionImagePath );
				}
				
				return _descImage;
				
			}
		}
		
		private Image _previewImage;
		
		public Image PreviewImage {
			get {
				
				if( _previewImage == null && PreviewImagePath.Length > 0 ) {
					
					_previewImage = GetImage( PreviewImagePath );
				}
				
				return _previewImage;
				
			}
		}
		
		protected Image GetImage(String relativeImagePath) {
			
			String imgFilename = System.IO.Path.Combine( Package.RootDirectory.FullName, relativeImagePath );
			
			if( !File.Exists( imgFilename ) ) return null;
			
			Image img;
			if( !Package.PackageImages.TryGetValue( imgFilename, out img ) ) {
				
				try {
					
					img = Miscellaneous.ImageFromFile( imgFilename );
					
					Package.PackageImages.Add( imgFilename, img );
				} catch(OutOfMemoryException) {
					Package.PackageImages.Add( imgFilename, null );
				}
				
			}
			
			return img;
		}
		
#region Write
		
		public abstract void Write(XmlElement parent);
		
		/// <summary>Creates a PackageItem XML element, prepopulates it with the common attributes, and appends it to the parent element's children.</summary>
		protected XmlElement CreateElement(XmlElement parent, String name, params String[] attributes) {
			
			XmlElement element = CreateElement(parent, name);
			
			if( attributes.Length % 2 != 0 ) throw new ArgumentException("attributes must be in pairs");
			
			for(int i=0;i<attributes.Length;i+=2) {
				
				String attName = attributes[i];
				String attValu = attributes[i+1];
				
				if( !String.IsNullOrEmpty( attValu ) )
					AddAttribute(element, attName, attValu);
			}
			
			AddAttribute(element, "id"     , this.Id );
			AddAttribute(element, "name"   , this.Name );
			AddAttribute(element, "desc"   , this.Description );
			AddAttribute(element, "descImg", this.DescriptionImagePath );
			
			if( Hidden )
				AddAttribute(element, "hidden", "true" );
			
			if( Enabled != null && !Enabled.Value )
				AddAttribute(element, "enabled", "false" );
			
			if( Condition != null )
				AddAttribute(element, "condition", Condition.ExpressionString );
			
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
		
#endregion
		
	}
}
