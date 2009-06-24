﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

using Anolis.Core.Packages.Operations;


namespace Anolis.Core.Packages {
	
	/// <summary>An arbitrary grouping of elements</summary>
	public class Group : PackageItem {
		
		public Group(Package package, Group parent, XmlElement element) : base(package, parent, element) {
			
			/////////////////////////////////////////////////////
			
			Children   = new GroupCollection();
			Operations = new OperationCollection(this);
			
			foreach(XmlNode node in element.ChildNodes) {
				
				if( node.NodeType != XmlNodeType.Element ) continue;
				
				XmlElement e = node as XmlElement;
				
				switch(e.Name) {
					case "group":
						
						Group group = new Group(package, this, e);
						Children.Add( group );
						
						break;
					
					default:
						
						Operation op = Operation.FromElement(package, this, e);
						
						if( op != null ) Operations.Add( op );
						break;
						
				}
				
			}
			
			/////////////////////////////////////////////////////
			
			Mutex    = new GroupCollection();
			// set Mutex members after all the siblings have been read in
			
			String mutex = element.Attributes["mutex"] != null ? element.Attributes["mutex"].Value : String.Empty;
			if(mutex.Length > 0) {
				
				MutexIds = mutex.Split(' ');
				
			}
			
		}
		
		public Group(Package package, Group parent, String[] mutexIds) : base(package, parent) {
			
			Children   = new GroupCollection();
			Operations = new OperationCollection(this);
			Mutex      = new GroupCollection();
			
			MutexIds = mutexIds;
			
		}
		
		public Boolean IsEnabled {
			get {
				
				if( ParentGroup == null ) return true;
				
				return ParentGroup.IsEnabled ? Enabled : false;
				
			}
		}
		
		public EnabledState EnabledState {
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
		}
		
		public override void Write(XmlElement parent) {
			
			XmlElement element = CreateElement(parent, "group");
			
			////////////////////////////////
			// Build mutex="" attribute
			
			if( MutexIds != null ) {
				
				StringBuilder sb = new StringBuilder();
				foreach(String mutexId in MutexIds) {
					sb.Append( mutexId );
					sb.Append(" ");
				}
				
				AddAttribute(element, "mutex", sb.ToString().Trim());
				
			}
			
			foreach(Operation op in Operations) {
				
				op.Write( element );
			}
			
			foreach(Group group in Children) {
				
				group.Write( element );
			}
			
		}
		
		internal String[] MutexIds    { get; private set; }
		
		public GroupCollection Mutex    { get; private set; }
		
		public GroupCollection     Children   { get; private set; }
		public OperationCollection Operations { get; private set; }
		
		internal void Flatten(List<Operation> operations) {
			
			foreach(Group       set in Children)  set.Flatten(operations);
			
			operations.AddRange( Operations );
			
		}
		
		
		
	}
	
	public class GroupCollection : Collection<Group> {
	}
	
	public enum EnabledState {
		Enabled,
		Partial,
		Disabled
	}
	
}
