using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.IO;

using ProgressEventArgs = Anolis.Core.Packages.PackageProgressEventArgs;
using System.Collections.Generic;

namespace Anolis.Core.Packages {
	
	/// <summary>An arbitrary grouping of elements</summary>
	public class Set :  PackageItem {
		
		public Set(Package package, XmlElement element) : base(package, element) {
			
			/////////////////////////////////////////////////////
			
			Children   = new SetCollection();
			Operations = new OperationCollection();
			
			foreach(XmlElement e in element.ChildNodes) {
				
				switch(e.Name) {
					case "set":
						
						Set set = new Set(package, e);
						Children.Add( set );
						
						break;
						
					case "patch":
						
						PatchOperation patch = new PatchOperation(package, e);
						Operations.Add( patch );
						break;
						
					case "file":
						
						FileOperation file = new FileOperation(package, e);
						Operations.Add( file );
						break;
						
					case "filetype":
					case "extra":
					default:
						
						
						
						
						break;
				}
				
			}
			
			/////////////////////////////////////////////////////
			
			Mutex    = new SetCollection();
			// set Mutex members after all the siblings have been read in
			
			String mutex = element.Attributes["mutex"] != null ? element.Attributes["mutex"].Value : String.Empty;
			if(mutex.Length > 0) {
				
				MutexIds = mutex.Split(' ');
				
			}
			
		}
		
		internal String[] MutexIds    { get; private set; }
		
		public SetCollection Mutex    { get; private set; }
		
		public SetCollection       Children   { get; private set; }
		public OperationCollection Operations { get; private set; }
		
		internal void Flatten(List<Operation> operations) {
			
			foreach(Set       set in Children)  set.Flatten(operations);
			
			operations.AddRange( Operations );
			
		}
		
	}
	
	public class SetCollection : Collection<Set> {
	}
}
