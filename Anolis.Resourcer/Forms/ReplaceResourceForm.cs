using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Data;

using FilterPair = Anolis.Core.Utility.Pair<Anolis.Core.Data.ResourceDataFactory, System.String>;

namespace Anolis.Resourcer {
	
	public partial class ReplaceResourceForm : BaseForm {
		
		/// <remarks>The following properties must be set before showing this form:
		/// <list>
		///		<item>Source</item>
		///		<item>InitialResource</item>
		/// </list></remarks>
		public ReplaceResourceForm() {
			
			InitializeComponent();
			
			PopulateOfdFilter();
			
			this.__browse.Click += new EventHandler(__browse_Click);
		}
		
		private FilterPair[] _filters;
		
		private void PopulateOfdFilter() {
			
			_filters = ResourceDataFactory.GetOpenFileFilters();
			
			String filter = String.Empty;
			foreach(FilterPair pair in _filters) filter += pair.Y + '|';
			if(filter.EndsWith("|")) filter = filter.Substring(0, filter.Length - 1);
			
			__ofd.Filter = filter;
			
		}
		
		private void __browse_Click(Object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) != DialogResult.OK ) return;
			
			__repFilename.Text = __ofd.FileName;
			
			try {
				_replacement = ResourceData.FromFile( __ofd.FileName, _initial.Lang.Name.Type.Source );
			} catch( AnolisException ex) {
				
				__repSubclass.Text = "Unable to load file into a resource: " + ex.Message;
				return;
			}
			
			if(_replacement == null) {
				
				__repSubclass.Text = "Unable to load file into a resource";
			} else {
				
				__repSubclass.Text = _replacement.GetType().Name;
			}
			
		}
		
		private ResourceData _initial;
		private ResourceData _replacement;
		
		public ResourceData InitialResource {
			get { return _initial; }
			set {
				_initial = value;
				__origPath.Text = MainForm.GetResourcePath( value.Lang );
				__origSubclass.Text = value.GetType().Name;
			}
		}
		
		public ResourceData ReplacementResource {
			get { return _replacement; }
		}
		
	}
}
