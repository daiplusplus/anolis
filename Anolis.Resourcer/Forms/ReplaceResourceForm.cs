using System;
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
			
			this.__factoryOptions.Click += new EventHandler(__factoryOptions_Click);
			
			this.__repLoad.Click += new EventHandler(__repLoad_Click);
			this.__browse.Click += new EventHandler(__browse_Click);
		}
		
		private void __factoryOptions_Click(object sender, EventArgs e) {
			
			FactoryOptionsForm form = new FactoryOptionsForm();
			form.ShowDialog(this);
		}
		
		private void __repLoad_Click(object sender, EventArgs e) {
			
			LoadReplacement( __repFilename.Text );
		}
		
		private FilterPair[] _filters;
		
		private void PopulateOfdFilter() {
			
			_filters = ResourceDataFactory.GetOpenFileFilters();
			
			String filter = String.Empty;
			foreach(FilterPair pair in _filters) filter += pair.Y + '|';
			if(filter.EndsWith("|")) filter = filter.Substring(0, filter.Length - 1);
			
			__ofd.Filter = filter;
			
		}
		
		public void PopulatePath(String fileName) {
			
			__repFilename.Text = fileName;
		}
		
		private void LoadReplacement(String fileName) {
			
			__repFilename.Enabled = false;
			__repLoad    .Enabled = false;
			__browse     .Enabled = false;
			
			if( !System.IO.File.Exists( fileName ) ) {
				
				__repSubclass.Text = "Specified file does not exist";
				return;
			}
			
			try {
				
				_replacement = ResourceData.FromFileToUpdate( fileName, _initial.Lang );
				
				_replacement.Tag["sourceFile"] = fileName;
				
			} catch( AnolisException ex) {
				
				__repSubclass.Text = "Unable to load file into a resource: " + ex.Message;
				return;
			}
			
			if(_replacement == null) {
				
				__repSubclass.Text = "Unable to load file into a resource";
			} else {
				
				__repSubclass.Text = _replacement.GetType().Name;
			}
			
			__ok.Enabled = true;
			
		}
		
		private void __browse_Click(Object sender, EventArgs e) {
			
			if( __ofd.ShowDialog(this) != DialogResult.OK ) return;
			
			__repFilename.Text = __ofd.FileName;
		}
		
		private ResourceData _initial;
		private ResourceData _replacement;
		
		public ResourceData InitialResource {
			get { return _initial; }
			set {
				_initial = value;
				__origPath.Text = value.Lang.ResourcePath;
				__origSubclass.Text = value.GetType().Name;
			}
		}
		
		public ResourceData ReplacementResource {
			get { return _replacement; }
		}
		
	}
}
