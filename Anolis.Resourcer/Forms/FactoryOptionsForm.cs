using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer {
	
	public partial class FactoryOptionsForm : BaseForm {
		
		public FactoryOptionsForm() {
			InitializeComponent();
			
			this.Load += new EventHandler(FactoryOptions_Load);
		}
		
		private void FactoryOptions_Load(object sender, EventArgs e) {
			
			LoadDictionary();
		}
		
		
		private void LoadDictionary() {
			
			FactoryOptions.Populate();
			
			IDictionary dictionary = FactoryOptions.Instance;
			
			DictionaryPropertyGridAdapter adapter = new DictionaryPropertyGridAdapter( dictionary );
			
			__grid.SelectedObject = adapter;
			
		}
		
	}
	
	internal class DictionaryPropertyGridAdapter : ICustomTypeDescriptor {
		
		private IDictionary _dictionary;
		
		public DictionaryPropertyGridAdapter(IDictionary d) {
			_dictionary = d;
		}
		
#region Boring Interface Conformance and Filler
		
		public String GetComponentName() {
			return TypeDescriptor.GetComponentName(this, true);
		}
		
		public EventDescriptor GetDefaultEvent() {
			return TypeDescriptor.GetDefaultEvent(this, true);
		}
		
		public String GetClassName() {
			return TypeDescriptor.GetClassName(this, true);
		}
		
		public EventDescriptorCollection GetEvents(Attribute[] attributes) {
			return TypeDescriptor.GetEvents(this, attributes, true);
		}
		
		EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents() {
			return TypeDescriptor.GetEvents(this, true);
		}
		
		public TypeConverter GetConverter() {
			return TypeDescriptor.GetConverter(this, true);
		}
		
		public Object GetPropertyOwner(PropertyDescriptor pd) {
			return _dictionary;
		}
		
		public AttributeCollection GetAttributes() {
			return TypeDescriptor.GetAttributes(this, true);
		}
		
		public Object GetEditor(Type editorBaseType) {
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}
		
		public PropertyDescriptor GetDefaultProperty() {
			return null;
		}
		
#endregion
		
		PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties() {
			return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
		}
		
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
			
			ArrayList properties = new ArrayList();
			
			foreach(DictionaryEntry e in _dictionary) {
				
				properties.Add( new DictionaryPropertyDescriptor(_dictionary, e.Key) );
			}
			
			PropertyDescriptor[] props = (PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor));
			
			return new PropertyDescriptorCollection(props);
		}
		
	}
	
	internal class DictionaryPropertyDescriptor : PropertyDescriptor {
		
		private IDictionary _dictionary;
		private Object      _key;
		
		internal DictionaryPropertyDescriptor(IDictionary d, Object key) : base(key.ToString(), null) {
			_dictionary = d;
			_key = key;
		}
		
		private FactoryOptionValue Value {
			get {
				return (FactoryOptionValue)_dictionary[ _key ];
			}
		}
		
		public override Type PropertyType {
			get {
				return Value.ValueType;
			}
		}
		
		public override void SetValue(Object component, Object value) {
			Value.Value = value;
		}
		
		public override Object GetValue(Object component) {
			return Value.Value;
		}
		
		public override Boolean IsReadOnly {
			get { return false; }
		}
		
		public override Type ComponentType {
			get { return null; }
		}
		
		public override Boolean CanResetValue(Object component) {
			return false;
		}
		
		public override void ResetValue(Object component) {
		}
		
		public override Boolean ShouldSerializeValue(Object component) {
			return false;
		}
		
		public override String Category {
			get { return Value.Category; }
		}
		
		public override String Description {
			get { return Value.Description; }
		}
		
	}

}
