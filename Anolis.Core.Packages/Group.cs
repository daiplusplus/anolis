using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

using Anolis.Packages.Operations;
using System.Drawing;

namespace Anolis.Packages {
	
	/// <summary>An arbitrary grouping of elements</summary>
	public class Group : PackageItem {
		
		public Group(Package package, Group parent, XmlElement element) : base( package, parent, element) {
			
			/////////////////////////////////////////////////////
			
			Children   = new GroupCollection(this);
			Operations = new OperationCollection(this);
			
			IsResGroup       = element.GetAttribute("isResGroup") == "true" ||  element.GetAttribute("isResGroup") == "1";
			
			foreach(XmlNode node in element.ChildNodes) {
				
				if( node.NodeType != XmlNodeType.Element ) continue;
				
				XmlElement e = node as XmlElement;
				
				switch(e.Name) {
					case "group":
						
						Group group = new Group(package, this, e);
						Children.Add( group );
						
						break;
					
					default:
						
						Operation op = Operation.FromElement(this, e);
						
						if( op != null ) Operations.Add( op );
						break;
						
				}
				
			}
			
		}
		
		public Group(Package package, Group parent) : base(package, parent) {
			
			Children   = new GroupCollection(this);
			Operations = new OperationCollection(this);
		}
		
		public Boolean IsResGroup { get; set; }
		
#region Enabled
		
//		public override Boolean Enabled {
//			get { return base.Enabled; }
//			set {
//				base.Enabled = value;
//				
//				// Enabled.set can be called from the PackageItem constructor
//				if( Operations != null ) foreach(Operation op in Operations) op.Enabled = value;
//				
//				if( Children   != null ) foreach(Group      g in Children  )  g.Enabled = value;
//			}
//		}
		
/*		public EnabledState EnabledState {
			get {
				
				Boolean hasAloEnabled = false; // Alo = At Least One
				Boolean hasAloDisabled = false; // Alo = At Least One
				
				foreach(Group group in Children) {
					
					switch( group.EnabledState ) {
						case EnabledState.Partial:
							return EnabledState.Partial;
						case EnabledState.Enabled:
							hasAloEnabled = true; break;
						case EnabledState.Disabled:
							hasAloDisabled = true; break;
					}
					
				}
				
				if( hasAloEnabled && hasAloDisabled ) return EnabledState.Partial;
				
				/////////////////////////////////////////////////////////
				
				foreach(Operation op in Operations) {
					
					if( op.IsEnabled ) hasAloEnabled  = true;
					else               hasAloDisabled = true;
					
				}
				
				if( hasAloEnabled && hasAloDisabled ) return EnabledState.Partial;
				if( !hasAloDisabled ) return EnabledState.Enabled;
				
				return EnabledState.Disabled;
				
			}
		} */
		
#endregion
		
		public override void Write(XmlElement parent) {
			
			XmlElement element = CreateElement(parent, "group");
			
			foreach(Operation op in Operations) {
				
				op.Write( element );
			}
			
			foreach(Group group in Children) {
				
				group.Write( element );
			}
			
		}
		
		public GroupCollection     Children   { get; private set; }
		public OperationCollection Operations { get; private set; }
		
		internal void Flatten(List<Operation> operations) {
			
			foreach(Group       set in Children)  set.Flatten(operations);
			
			operations.AddRange( Operations );
			
		}
		
		public PackageItem Find(String id) {
			
			if( Id == id ) return this;
			
			return Find( this, id );
		}
		
		private static PackageItem Find(Group parent, String id) {
			
			foreach(Operation op in parent.Operations) {
				if( op.Id == id ) return op;
			}
			
			foreach(Group group in parent.Children) {
				if( group.Id == id ) return group;
			}
			
			PackageItem ret;
			foreach(Group group in parent.Children) {
				ret = Find( group, id );
				if( ret != null ) return ret;
			}
			
			return null;
		}
		
	}
	
	public class GroupCollection : Collection<Group> {
		
		private Group _parent;
		
		public GroupCollection(Group parent) {
			_parent = parent;
		}
		
//		protected override void InsertItem(int index, Group item) {
//			base.InsertItem(index, item);
//			
//			if( !_parent.IsEnabled ) item.Enabled = false;
//		}
		
	}
	
	public class OperationCollection : Collection<Operation> {
		
		private Group _parent;
		
		public OperationCollection(Group parent) {
			_parent = parent;
		}
		
//		protected override void InsertItem(int index, Operation item) {
//			base.InsertItem(index, item);
//			
//			if( !_parent.Enabled ) item.Enabled = false;
//		}
		
	}
	
	public enum EnabledState {
		Enabled,
		Partial,
		Disabled
	}
	
}
