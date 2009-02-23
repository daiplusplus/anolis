using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.IO;

using ProgressEventArgs = Anolis.Core.Packages.PackageProgressEventArgs;
using XTrip             = Anolis.Core.Utility.Triple<string,System.Xml.Schema.XmlSeverityType, System.Exception>;

namespace Anolis.Core.Packages {
	
	public abstract class PackageItem {
		
		protected PackageItem(XmlElement itemElement) {
			
			Id          = itemElement.Attributes["id"].Value;
			Name        = itemElement.Attributes["name"].Value;
			Description = itemElement.Attributes["desc"].Value;
			
		}
		
		public Boolean Enabled     { get; set; }
		public String  Id          { get; protected set; }
		public String  Name        { get; protected set; }
		public String  Description { get; protected set; }
		
	}
	
	/// <summary>Represents a collection of resource sets</summary>
	public class Package : PackageItem {
		
		internal Package(XmlElement packageElement) : base(packageElement) {
			
			Version     = Single.Parse( packageElement.Attributes["version"].Value );
			Attribution = packageElement.Attributes["attribution"].Value;
			Website     = new Uri( packageElement.Attributes["website"].Value );
			UpdateUri   = new Uri( packageElement.Attributes["updateUri"].Value );
			
			foreach(XmlElement setElement in packageElement.ChildNodes) {
				
				Set s = new Set( setElement );
				Children.Add( s );
				
			}
			
		}
		
		public Single Version     { get; private set; }
		public String Attribution { get; private set; }
		public Uri    Website     { get; private set; }
		public Uri    UpdateUri   { get; private set; }
		
		public SetCollection Children { get; private set; }
		
		//////////////////////////////
		
		public static Package FromFile(String packageXmlFilename) {
			
			XmlDocument doc = new XmlDocument();
			doc.Load( packageXmlFilename );
			
			System.Collections.Generic.List<XTrip> validationMessages = new System.Collections.Generic.List<XTrip>();
			
			doc.Validate( new ValidationEventHandler( delegate(Object sender, ValidationEventArgs ve) {
				
				validationMessages.Add( new XTrip( ve.Message, ve.Severity, ve.Exception ) );
				
			}) );
			
			// walk the document
			
			XmlElement packageElement = doc.DocumentElement;
			
			return new Package( packageElement );
			
			
		}
		
		//////////////////////////////
		
		/// <summary>Executes the operations contained within this package</summary>
		public void Execute() {
			
			foreach(Set set in Children) {
				
				set.Execute();
			}
			
		}
		
		
		
		protected void OnProgressEvent(ProgressEventArgs e) {
			
			if( ProgressEvent != null ) ProgressEvent(this, e);
		}
		
		public event EventHandler<ProgressEventArgs> ProgressEvent;
		
	}
	
	/// <summary>An arbitrary grouping of elements</summary>
	public class Set : PackageItem {
		
		public Set(XmlElement setElement) : base(setElement) {
			
			Mutex = new SetCollection();
			// set Mutex members after all the siblings have been read in
			
			foreach(XmlElement e in setElement.ChildNodes) {
				
				switch(e.Name) {
					case "set":
						
						Set set = new Set( e );
						Children.Add( set );
						
						break;
					case "patch":
					case "file":
					case "filetype":
						break;
				}
				
			}
			
			String mutex = setElement.Attributes["mutex"] != null ? setElement.Attributes["mutex"].Value : String.Empty;
			if(mutex.Length > 0) {
				
				MutexIds = mutex.Split(' ');
				
			}
			
		}
		
		internal String[] MutexIds    { get; private set; }
		
		public SetCollection Mutex    { get; private set; }
		
		public SetCollection       Children   { get; private set; }
		public OperationCollection Operations { get; private set; }
		
		internal void Execute() {
			
			foreach(Set       set in Children)  set.Execute();
			
			foreach(Operation op  in Operations) op.Execute();
			
		}
		
	}
	
	public class SetCollection : Collection<Set> {
	}
	
}
