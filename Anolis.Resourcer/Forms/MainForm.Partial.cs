using System;
using System.Windows.Forms;

using Anolis.Core;

namespace Anolis.Resourcer {
	
	// This file contains all the "task logic" which is called by UI events
	
	public partial class MainForm {
		
		private List<TypeViewer> _viewers;
		private Mru _mru;
		private Settings.Settings _settings;
		
		private void MainFormInit() {
			
			_settings = Anolis.Resourcer.Settings.Settings.Default;
//			_settings.Upgrade();
			
			if( _settings.MruList == null ) _settings.MruList = new StringCollection();
			
			_viewers = new List<TypeViewer>();
			_mru     = new Mru( _settings.MruCount, _settings.MruList, StringComparison.InvariantCultureIgnoreCase );
			
		}
		
		private void ToolbarUpdate() {
			
			this.SuspendLayout();
			
			Boolean is24 = Settings.Settings.Default.Toolbar24;
			
			__t.ImageScalingSize            = is24 ? new System.Drawing.Size(24, 24)   : new System.Drawing.Size(48, 48);
			
			__tSrcOpen.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText ;
			__tSrcSave.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
			__tSrcReve.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
			
			__tResAdd.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText;
			__tResExt.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText;
			__tResRep.TextImageRelation = is24 ? TextImageRelation.ImageBeforeText : TextImageRelation.ImageAboveText;
			__tResDel.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
			__tResCan.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
			
			__tGenOpt.DisplayStyle      = is24 ? ToolStripItemDisplayStyle.Image   : ToolStripItemDisplayStyle.ImageAndText;
			
			__tSrcOpen.Image            = is24 ? Properties.Resources.Toolbar_SrcOpen24 : Properties.Resources.Toolbar_SrcOpen;
			__tSrcSave.Image            = is24 ? Properties.Resources.Toolbar_SrcSave24 : Properties.Resources.Toolbar_SrcOpen;
			__tSrcReve.Image            = is24 ? Properties.Resources.Toolbar_SrcRev24  : Properties.Resources.Toolbar_SrcOpen;
			
			__tResAdd.Image             = is24 ? Properties.Resources.Toolbar_ResAdd24 : Properties.Resources.Toolbar_ResAdd;
			__tResExt.Image             = is24 ? Properties.Resources.Toolbar_ResExt24 : Properties.Resources.Toolbar_ResExt;
			__tResRep.Image             = is24 ? Properties.Resources.Toolbar_ResRep24 : Properties.Resources.Toolbar_ResRep;
			__tResDel.Image             = is24 ? Properties.Resources.Toolbar_ResDel24 : Properties.Resources.Toolbar_ResDel;
			__tResCan.Image             = is24 ? Properties.Resources.Toolbar_ResCan24 : Properties.Resources.Toolbar_ResCan;
			
			__tGenOpt.Image             = is24 ? Properties.Resources.Toolbar_GenOpt24 : Properties.Resources.Toolbar_GenOpt;
			
			__t.PerformLayout();
			
			this.ResumeLayout(true);
			this.PerformLayout();
			
		}
		
#region ResourceSource
		
		public ResourceTypeCollection SourceLoad(String path) {
			
			CurrentPath = path;
			
			///////////////////////////
			
			ResourceSource source = ResourceSource.Open(path, false);
			if(source == null) return null;
			
			CurrentSource = source;
			Mru.Push( path );
			
			ResourceTypeCollection types = source.Types;
			
			/////////////////////////
			
			OnCurrentSourceChanged();
			
			return types;
			
		}
		
		public void SourceSave() {
			
			CurrentSource.CommitChanges(true);
			
		}
		
#endregion
		
#region ResourceData
		
		private void DataLoad(ResourceLang lang) {
			
			CurrentLang = lang;
			CurrentData = lang.Data;
			
			OnCurrentDataChanged();
			
		}
		
		private void DataSave(IWin32Window window, SaveFileDialog sfd) {
			
			if( CurrentData == null ) throw new ApplicationException("No current ResourceData");
			
			String filter = String.Empty;
			foreach(String f in CurrentLang.Data.SaveFileFilters) filter += f + '|';
			if(filter.EndsWith("|")) filter = filter.Substring(0, filter.Length - 1);
			
			String extensionOfFirstFilter = CurrentLang.Data.SaveFileFilters[0].Substring( CurrentLang.Data.SaveFileFilters[0].LastIndexOf('.') );
			
			sfd.Filter = filter;
			
			String initialFilename =
				CurrentLang.Name.Type.Identifier.FriendlyName + "-" +
				CurrentLang.Name.Identifier.FriendlyName + "-" +
				CurrentLang.LanguageId.ToString(Cult.InvariantCulture) +
				extensionOfFirstFilter;
			
			sfd.FileName = RemoveIllegalChars( initialFilename, null );
			
			if(sfd.ShowDialog(window) != DialogResult.OK) return;
			
			CurrentLang.Data.Save( sfd.FileName );
			
		}
		
		private void DataRemove() {
			
			if( CurrentData is DirectoryResourceData ) {
				
				//DialogResult r = MessageBox.Show("Do you want to delete just this ResourceData for the directory, or the directory and all of its members?", "Anolis Resourcer", MessageBoxButtons.);
				// TODO: How do you do a messagebox with custom button labels?
				
			}
			
			if( CurrentData is IconCursorImageResourceData ) {
				
				DialogResult r = MessageBox.Show("You are attempting to delete an Icon or Cursor member image. This will render the state of the parent Icon or Cursor Directory invalid and will cause errors in any applications attempting to read the icon. Are you sure you want to continue?", "Anolis Resourcer", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if( r != DialogResult.OK ) return;
				
			}
			
			CurrentSource.Remove( CurrentData );
		}
		
		private void DataAdd(IWin32Window window) {
			
			AddResourceForm form = new AddResourceForm(this);
			
			if(form.ShowDialog(window) == DialogResult.OK) {
				
				ResourceData data = form.ResourceData;
				
				ResourceTypeIdentifier typeId = form.ResourceTypeIdentifier;
				ResourceIdentifier     nameId = form.ResourceNameIdentifier;
				UInt16                 langId = form.ResourceLangId;
				
				CurrentSource.Add( typeId, nameId, langId, data );
				
			}
			
		}
		
		/// <summarydata>Replaces the current ResourceData in the ResourceSource with the provided ResourceData.</summary>
		private void DataReplace(ResourceData newData) {
			
			if( CurrentData == null ) throw new InvalidOperationException("There is no current ResourceData.");
			
		}
		
#endregion
		
#region Utility
		
		public void ProgramSave() {
			
			_settings.MruList.AddRange( _mru.Items ); 
			_settings.MruCount = _mru.Capacity;
			
			_settings.Save();
			
		}
		
		private static String RemoveIllegalChars(String s, Char? replaceWith) {
			
			Char[] illegals = System.IO.Path.GetInvalidFileNameChars();
			Char[] str      = s.ToCharArray();
			
			if(replaceWith == null) {
				
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				
				for(int i=0;i<s.Length;i++) {
					
					Boolean isIllegal = false;
					for(int j=0;j<illegals.Length;j++) {
						if(str[i] == illegals[j]) {
							isIllegal = true;
							break;
						}
					}
					
					if(!isIllegal) sb.Append( str[i] );
					
				}
				
				return sb.ToString();
				
			} else {
				
				for(int i=0;i<s.Length;i++) {
					
					Boolean isIllegal = false;
					for(int j=0;j<illegals.Length;j++) {
						if(str[i] == illegals[j]) {
							isIllegal = true;
							break;
						}
					}
					
					if(isIllegal) str[i] = replaceWith.Value;
					
				}
				
				return new String( str );
				
			}
			
		}
		
#endregion
		
	}
}
