using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Windows.Forms;

using Anolis.Core;
using Anolis.Resourcer.TypeViewers;
using Anolis.Resourcer.Settings;

using Cult = System.Globalization.CultureInfo;
using Anolis.Core.Data;
using Anolis.Core.Utility;

namespace Anolis.Resourcer {
	
	/// <summary>All the information for Resourcer in one place.</summary>
	/// <remarks>The hosting UI should ask the user for confirmation before entering methods here. So this class separates user actions with system actions.</remarks>
	internal sealed class ResourcerContext {
		
		private List<TypeViewer> _viewers;
		
		private Mru _mru;
		private Settings.Settings _settings;
		
		public ResourcerContext() {
			
			_settings = Anolis.Resourcer.Settings.Settings.Default;
//			_settings.Upgrade();
			
			if( _settings.MruList == null ) _settings.MruList = new StringCollection();
			
			_viewers = new List<TypeViewer>();
			_mru     = new Mru( _settings.MruCount, _settings.MruList, StringComparison.InvariantCultureIgnoreCase );
			
		}
		
		/// <summary>Saves the ResourcerContext state to the Settings.</summary>
		public void Save() {
			
			_settings.MruList.AddRange( _mru.Items ); 
			_settings.MruCount = _mru.Capacity;
			
			_settings.Save();
			
		}
		
		public String         CurrentPath   { get; private set; }
		public ResourceSource CurrentSource { get; private set; }
		public ResourceLang   CurrentLang   { get; set; }
		public ResourceData   CurrentData   { get; set; }
		
		public Mru            Mru    { get { return _mru; } }
		
		public Settings.Settings Settings { get { return _settings; } }
		
		public event EventHandler CurrentSourceChanged;
		public event EventHandler CurrentDataChanged;
		
		///////////////////////
		
		private void OnCurrentSourceChanged() {
			if(CurrentSourceChanged != null) CurrentSourceChanged(this, EventArgs.Empty);
		}
		
		private void OnCurrentDataChanged() {
			if(CurrentDataChanged != null) CurrentDataChanged(this, EventArgs.Empty);
		}
		
#region ResourceSource
		
		public ResourceTypeCollection LoadSource(String path) {
			
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
		
		public void SaveSource() {
			
			CurrentSource.CommitChanges(true);
			
		}
		
#endregion
		
#region ResourceData
		
		public void LoadData(ResourceLang lang) {
			
			CurrentLang = lang;
			CurrentData = lang.Data;
			
			OnCurrentDataChanged();
			
		}
		
		public void SaveData(IWin32Window window, SaveFileDialog sfd) {
			
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
		
		public void RemoveData() {
			
			CurrentSource.Remove( CurrentData );
		}
		
		public void AddData(IWin32Window window) {
			
			AddResourceForm form = new AddResourceForm(this);
			
			if(form.ShowDialog(window) == DialogResult.OK) {
				
				ResourceData data = form.ResourceData;
				
				ResourceTypeIdentifier typeId = form.ResourceTypeIdentifier;
				ResourceIdentifier     nameId = form.ResourceNameIdentifier;
				UInt16                 langId = form.ResourceLangId;
				
				CurrentSource.Add( typeId, nameId, langId, data );
				
			}
			
		}
		
#endregion
		
		////////////////////////////////////////////////////////
		
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
		
	}
}
