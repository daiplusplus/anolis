using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Core.Utility;

using FilterPair = Anolis.Core.Utility.Pair<Anolis.Core.Data.ResourceDataFactory, System.String>;

namespace Anolis.Resourcer {
	
	internal partial class AddResourceForm : BaseForm {
		
		private ResourceData _data;
		private FilterPair[] _filters;
		
		public AddResourceForm() {
			
			InitializeComponent();
			
			__nameCustom.Maximum = Int32.MaxValue - 1;
			
			PopulateOfdFilter();
			
			PopulateWin32Types(true);
			
			PopulateLanguages(true);

			this.__continue  .Click += new EventHandler(__continue_Click);
			this.__fileBrowse.Click += new EventHandler(__fileBrowse_Click);
			this.__langSort  .Click += new EventHandler(__langSort_Click);
			this.__typeSort  .Click += new EventHandler(__typeSort_Click);
			
			this.__ok        .Click += new EventHandler(__ok_Click);
			
			this.__nameAuto.CheckedChanged += new EventHandler(__nameAuto_CheckedChanged);
			
		}
		
		private void __nameAuto_CheckedChanged(Object sender, EventArgs e) {
			
			__nameCustom.Enabled = !__nameAuto.Checked;
			
		}
		
		public void LoadFile(String filename) {
			
			__continue.Visible = true;
			__fileBrowse.Visible = false;
			
			__file.Text = filename;
			
		}
		
		private void PopulateOfdFilter() {
			
			_filters = ResourceDataFactory.GetOpenFileFilters();
			
			String filter = String.Empty;
			foreach(FilterPair pair in _filters) filter += pair.Y + '|';
			if(filter.EndsWith("|")) filter = filter.Substring(0, filter.Length - 1);
			
			__ofd.Filter = filter;
			
		}
		
		private void PopulateWin32Types(Boolean sortByName) {
			
			__typeKnown.Items.Clear();
			
			List<Pair<String,Win32ResourceType>> items = new List<Pair<String,Win32ResourceType>>();
			
			Int32[] vals = (Int32[])Enum.GetValues(typeof(Win32ResourceType));
			foreach(Int32 v in vals) {
				
				if(v <= 0) continue;
				
				Win32ResourceType val = (Win32ResourceType)v;
				
				String           name = ResourceTypeIdentifier.GetTypeFriendlyName( v ) + " (" + v.ToString(CultureInfo.InvariantCulture) + ')';
				
				Pair<String,Win32ResourceType> item = new Pair<String,Win32ResourceType>(name, val);
				
				items.Add( item );
				
			}
			
			if(sortByName) {
				items.Sort( (x,y) => x.X.CompareTo( y.X ) );
			} else {
				items.Sort( (x,y) => x.Y.CompareTo( y.Y ) );
			}
			
			foreach( Pair<String,Win32ResourceType> item in items )
				__typeKnown.Items.Add( item );
			
		}
		
		private void PopulateLanguages(Boolean sortByDisplayName) {
			
			__lang.BeginUpdate();
			
			__lang.Items.Clear();
			
			__lang.Items.AddRange( sortByDisplayName ? Cultures.CulturesByName : Cultures.CulturesByLcid );
			
			Int32 systemLcid = Anolis.Core.Packages.PackageUtility.GetSystemInstallLanguage();
			foreach(ListBoxItem<CultureInfo> c in __lang.Items) {
				
				if(c.Item.LCID == systemLcid) {
					__lang.SelectedItem = c;
					break;
				}
			}
			
			__lang.EndUpdate();
			
		}
		
		//////////////////////////////
		
		private void __ok_Click(object sender, EventArgs e) {
			
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		
		private void __continue_Click(object sender, EventArgs e) {
			
			CreateResourceData();
		}
		
		private void __fileBrowse_Click(Object sender, EventArgs e) {
			
			if(__ofd.ShowDialog(this) == DialogResult.OK) {
				
				__file.Text = __ofd.FileName;
				
				CreateResourceData();
				
			}
			
		}
		
		private Boolean _langSortedByName = true;
		
		private void __langSort_Click(object sender, EventArgs e) {
			
			if( _langSortedByName ) {
				
				// so now sort it by LCID
				
				__langSort.Image = Resources.ARF_SortByName;
				
				PopulateLanguages( _langSortedByName = !_langSortedByName );
				
			} else {
				
				__langSort.Image = Resources.ARF_SortByLCID;
				
				PopulateLanguages( _langSortedByName = !_langSortedByName );
			}
			
		}
		
		private Boolean _typeSortedByName = true;
		
		private void __typeSort_Click(object sender, EventArgs e) {
			
			if( _typeSortedByName ) {
				
				// so now sort it by LCID
				
				__typeSort.Image = Resources.ARF_SortByName;
				
				PopulateWin32Types( _typeSortedByName = !_typeSortedByName );
				
			} else {
				
				__typeSort.Image = Resources.ARF_SortByLCID;
				
				PopulateWin32Types( _typeSortedByName = !_typeSortedByName );
			}
			
		}
		
		//////////////////////////////
		
		private void CreateResourceData() {
			
			MainForm parent = Owner as MainForm;
			
			_data = ResourceData.FromFileToAdd( __file.Text, 1033, parent.CurrentSource );
			
			_data.Tag["sourceFile"] = __file.Text;
			
			SetDetailsEnabled( true );
			
			///////////////////////////
			// Recommended Type
			
			ResourceTypeIdentifier typeId = _data.RecommendedTypeId;
			if(typeId.KnownType != Win32ResourceType.Custom) {
				
				if(typeId.KnownType == Win32ResourceType.Unknown) {
					// add the type to the list
					
					// for now, just throw
					throw new NotSupportedException("Resourcer does not support Unknown Win32 types.");
					
				} else {
					
					__typeWin32Rad.Checked = true;
					
					foreach( Pair<String,Win32ResourceType> item in __typeKnown.Items ) {
						
						if( item.Y == typeId.KnownType ) {
							__typeKnown.SelectedItem = item;
							break;
						}
						
					}
					
				}
				
			} else {
				
				__typeStringRad.Checked = true;
				
				__typeString.Text = typeId.StringId;
				
			}
			
			// Set recommended control values
			
			///////////////////////////
			// Recommended Name
			
			__nameAuto.Checked = true;
			__nameCustom.Value = MainForm.LatestInstance.CurrentSource.GetUnusedName( typeId ).IntegerId.Value;
			
		}
		
		private Boolean ValidateResourceIdentifiers() {
			
			// TODO AddResourceForm.ValidateResourceIdentifiers
			
			///////////////////////////
			// Resource Type
			
			///////////////////////////
			// Resource Name / Lang combo
			
			// ensure the selected name and lang is not in use, if it is prompt to Replace rather than Add
//			Context.CurrentSource.IsNameInUse( );
			
			return true;
			
		}
		
		//////////////////////////////
		
		public ResourceData ResourceData {
			get { return _data; }
		}
		
		public ResourceTypeIdentifier ResourceTypeIdentifier {
			get {
				
				if( __typeWin32Rad.Checked ) {
					
					Pair<String,Win32ResourceType> item = __typeKnown.SelectedItem as Pair<String,Win32ResourceType>;
					
					return new ResourceTypeIdentifier( item.Y );
					
				} else {
					
					return new ResourceTypeIdentifier( __typeString.Text );
					
				}
				
			}
		}
		
		public ResourceIdentifier ResourceNameIdentifier {
			get {
				
				if( __nameIntRad.Checked ) {
					
					return new ResourceIdentifier( (Int32)__nameCustom.Value );
					
				} else {
					
					return new ResourceIdentifier( __nameString.Text );
					
				}
				
			}
		}
		
		public UInt16 ResourceLangId {
			get {
				
				ListBoxItem<CultureInfo> item = __lang.SelectedItem as ListBoxItem<CultureInfo>;
				
				return (UInt16)item.Item.LCID;
				
			}
		}
		
		//////////////////////////////
		
		private void SetDetailsEnabled(Boolean value) {
			
			this.__langGrp.Enabled = !value;
			this.__fileGrp.Enabled = !value;
			this.__typeGrp.Enabled = value;
			this.__nameGrp.Enabled = value;
		}
		
		//////////////////////////////
		
		
		
	}
}
