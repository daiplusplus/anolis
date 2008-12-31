using System;
using System.Globalization;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Core.Utility;

using FilterPair = Anolis.Core.Utility.Pair<Anolis.Core.Data.ResourceDataFactory, System.String>;

namespace Anolis.Resourcer {
	
	internal partial class AddResourceForm : Form {
		
		private ResourceData _data;
		private FilterPair[] _filters;
		
		public AddResourceForm() {
			
			InitializeComponent();
			
			__nameCustom.Maximum = Int32.MaxValue - 1;
			
			PopulateOfdFilter();
			
			PopulateWin32Types(true);
			
			PopulateLanguages(true);
			
			__fileBrowse.Click += new EventHandler(__fileBrowse_Click);
			__langSort  .Click += new EventHandler(__langSort_Click);
			__typeSort  .Click += new EventHandler(__typeSort_Click);
			
			this.__ok.Click += new EventHandler(__ok_Click);
			
		}
		
		private void __ok_Click(object sender, EventArgs e) {
			
			this.DialogResult = DialogResult.OK;
			this.Close();
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
			
			System.Collections.Generic.List< Pair<String,Win32ResourceType> > items = new System.Collections.Generic.List<Pair<string,Win32ResourceType>>();
			
			for(int i=1;i<=24;i++) { // skip -1 = Custom and 0 = Unknown
				
				if( Enum.IsDefined(typeof(Win32ResourceType), i ) ) {// <0, >25, 13, 15, and 18 are undefined
					
					Win32ResourceType val = (Win32ResourceType)i;
					String           name = ResourceTypeIdentifier.GetTypeFriendlyName( i ) + " (" + i.ToString(CultureInfo.InvariantCulture) + ')';
					
					Pair<String,Win32ResourceType> item = new Pair<String,Win32ResourceType>(name, val);
					
					items.Add( item );
					
				}
				
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
			
			__lang.Items.Clear();
			
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
			
			if(sortByDisplayName) {
				Array.Sort<CultureInfo>(cultures, (x,y) => x.DisplayName.CompareTo( y.DisplayName ) );
			} else {
				Array.Sort<CultureInfo>(cultures, (x,y) => x.LCID.CompareTo( y.LCID ) );
			}
			
			__lang.Items.Add( new Pair<String,Int32>("Neutral (0)", 0) );
			
			foreach(CultureInfo cult in cultures) {
				
				String itemText = cult.DisplayName + " (" + cult.LCID.ToString(CultureInfo.InvariantCulture) + ')';
				
				__lang.Items.Add( new Pair<String,Int32>(itemText, cult.LCID) );
				
			}
			
			__lang.SelectedIndex = 0;
			
		}
		
		public AddResourceForm(ResourcerContext context) : this() {
			Context = context;
		}
		
		private void CreateResourceData() {
			
			ResourceDataFactory factory = _filters[ __ofd.FilterIndex - 1].X;
			
			_data = Anolis.Core.ResourceData.FromFile( __ofd.FileName, Context.CurrentSource );
			
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
			
			///////////////////////////
			// Recommended Name
			
			__nameAuto.Checked = true;
			__nameCustom.Value = Context.CurrentSource.GetUnusedName( typeId ).IntegerId.Value;
			
			///////////////////////////
			// Recommended Lang
			
			Int32 currentLcid = CultureInfo.CurrentCulture.LCID;
			foreach(Pair<String,Int32> p in __lang.Items) {
				
				if(p.Y == currentLcid) {
					__lang.SelectedItem = p;
					break;
				}
			}
			
			//////////////////////////////
			
			this.ResourceTypeIdentifier = typeId;
			this.ResourceNameIdentifier = new ResourceIdentifier( Convert.ToInt32( __nameCustom.Value ) );
			this.ResourceLangId         = (UInt16)currentLcid;
			
		}
		
		private Boolean ValidateResourceIdentifiers() {
			
			///////////////////////////
			// Resource Type
			
			///////////////////////////
			// Resource Name / Lang combo
			
			// ensure the selected name and lang is not in use, if it is prompt to Replace rather than Add
//			Context.CurrentSource.IsNameInUse( );
			
			return true;
			
		}
		
		public ResourcerContext Context {
			get; set;
		}
		
		//////////////////////////////
		
		public ResourceData ResourceData {
			get { return _data; }
		}
		
		public ResourceTypeIdentifier ResourceTypeIdentifier { get; private set; }
		public ResourceIdentifier     ResourceNameIdentifier { get; private set; }
		public UInt16                 ResourceLangId         { get; private set; }
		
		//////////////////////////////
		
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
				
				__langSort.Image = Properties.Resources.ARF_SortByName;
				
				PopulateLanguages( _langSortedByName = !_langSortedByName );
				
			} else {
				
				__langSort.Image = Properties.Resources.ARF_SortByLCID;
				
				PopulateLanguages( _langSortedByName = !_langSortedByName );
			}
			
		}
		
		private Boolean _typeSortedByName = true;
		
		private void __typeSort_Click(object sender, EventArgs e) {
			
			if( _typeSortedByName ) {
				
				// so now sort it by LCID
				
				__typeSort.Image = Properties.Resources.ARF_SortByName;
				
				PopulateWin32Types( _typeSortedByName = !_typeSortedByName );
				
			} else {
				
				__typeSort.Image = Properties.Resources.ARF_SortByLCID;
				
				PopulateWin32Types( _typeSortedByName = !_typeSortedByName );
			}
			
		}
		
	}
}
