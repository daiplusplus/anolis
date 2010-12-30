using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using W3b.Wizards.WindowsForms;

using Anolis.Core;
using Anolis.Packages.Operations;
using Anolis.Core.Utility;
using Anolis.Packages;
using System.IO;

namespace Anolis.Installer.Pages {
	
	public partial class SelectorPage : BaseInteriorPage {
		
		public SelectorPage() {
			InitializeComponent();
			
			this.__advanced.Click += new EventHandler(__advanced_Click);
			this.__presets.SelectedIndexChanged += new EventHandler(__presets_SelectedIndexChanged);
			
			this.__wallpaper.SelectionChangeCommitted    += new EventHandler(SelectionChangeCommitted);
			this.__logonPreview.CheckedChanged           += new EventHandler(SelectionChangeCommitted);
			this.__logon.SelectionChangeCommitted        += new EventHandler(SelectionChangeCommitted);
			this.__bitmaps.SelectionChangeCommitted      += new EventHandler(SelectionChangeCommitted);
			this.__visualStyles.SelectionChangeCommitted += new EventHandler(SelectionChangeCommitted);
			
			this.PageLoad += new EventHandler(SelectorPage_PageLoad);

			this.PageUnload += new EventHandler<W3b.Wizards.PageChangeEventArgs>(SelectorPage_PageUnload);
			
			_customPreset = new Preset( InstallerResources.GetString("C_E1_Custom") );
		}
		
		private void SelectorPage_PageUnload(object sender, W3b.Wizards.PageChangeEventArgs e) {
			
			if(e.PageToBeLoaded == Program.PageCFInstallOpts || e.PageToBeLoaded == Program.PageCE2ModifyPackage) {
				
				PackageInfo.Package.ApplySelections( CurrentPreset );
			}
			
		}
		
		private void SelectorPage_PageLoad(object sender, EventArgs e) {
			
			PopulatePresets();
		}
		
		protected override String LocalizePrefix {
			get { return "C_E1"; }
		}
		
		public override BaseWizardPage NextPage {
			get { return Program.PageCFInstallOpts; }
		}
		
		public override BaseWizardPage PrevPage {
			get { return Program.PageCDReleaseNotes; }
		}
		
		private void __presets_SelectedIndexChanged(object sender, EventArgs e) {
			
			PopulateCombos();
		}
		
		private void __advanced_Click(Object sender, EventArgs e) {
			
			InstallationInfo.UseSelector = false;
			
			WizardForm.LoadPage( Program.PageCE2ModifyPackage );
		}
		
#region Presets and Selections
		
		private Preset CurrentPreset {
			get {
				return __presets.SelectedItem as Preset;
			}
		}
		
		private Preset _customPreset;
		
		private void SelectionChangeCommitted(object sender, EventArgs e) {
			
			Preset makeThisTheSelectedPreset = GetPresetForSelection();
			if( makeThisTheSelectedPreset == _customPreset ) {
				
				if( !__presets.Items.Contains( makeThisTheSelectedPreset ) ) {
					
					__presets.Items.Add( makeThisTheSelectedPreset );
				}
				
			} else {
				
				__presets.Items.Remove( _customPreset );
			}
			
			Boolean compose =  __presets.SelectedItem == makeThisTheSelectedPreset; // i.e. if _customPreset is already selected, so the .SelectedIndexChanged event won't fire
			
			__presets.SelectedItem = makeThisTheSelectedPreset;
			
			if( compose ) ComposePreview();
		}
		
		private Preset GetPresetForSelection() {
			
			Pair<String,Group> gPair = __bitmaps.SelectedItem as Pair<String,Group>;
			Group group = gPair.Y;
			
			Pair<String,WallpaperExtraOperation> wPair = __wallpaper.SelectedItem as Pair<String,WallpaperExtraOperation>;
			WallpaperExtraOperation wop = wPair.Y;
			
			Pair<String,VisualStyleExtraOperation> vPair = __visualStyles.SelectedItem as Pair<String,VisualStyleExtraOperation>;
			VisualStyleExtraOperation vop = vPair.Y;
			
			Pair<String,ResPatchOperation> lPair = __logon.SelectedItem as Pair<String,ResPatchOperation>;
			ResPatchOperation lop = lPair.Y;
			
			foreach(Preset preset in __presets.Items) {
				
				if( preset.Group == group && preset.VisualStyle == vop && preset.Wallpaper == wop && preset.WelcomeScreen == lop) return preset;
				
			}
			
			// Set the custom preset then
			
			_customPreset.Group         = group;
			_customPreset.VisualStyle   = vop;
			_customPreset.Wallpaper     = wop;
			_customPreset.WelcomeScreen = lop;
			return _customPreset;
			
		}
		
		private void PopulatePresets() {
			
			Package package = PackageInfo.Package;
			
			__presets.Items.Clear();
			
			foreach(Preset preset in package.Presets) {
				
				__presets.Items.Add( preset );
			}
			
			__presets.SelectedIndex = 0;
			
		}
		
		private Package                         _lastPackage;
		
		private List<Group>                     _opsB;
		private List<ResPatchOperation>         _opsR;
		private List<WallpaperExtraOperation>   _opsW;
		private List<VisualStyleExtraOperation> _opsV;
		
		private void GetAllOperations(Package currentPackage, out List<Group> bmps, out List<ResPatchOperation> rops, out List<WallpaperExtraOperation> wops, out List<VisualStyleExtraOperation> vops) {
			
			if( currentPackage == _lastPackage ) {
				
				bmps = _opsB;
				rops = _opsR;
				wops = _opsW;
				vops = _opsV;
				return;
			}
			
			Preset.GetPackageItems(PackageInfo.Package.RootGroup, out bmps, out rops, out wops, out vops);
			_opsB = bmps;
			_opsR = rops;
			_opsW = wops;
			_opsV = vops;
			
			_lastPackage = currentPackage;
			
		}
		
		/// <summary>Populates the comboboxes with the default selections of the selected Preset</summary>
		private void PopulateCombos() {
			
			Preset preset = __presets.SelectedItem as Preset;
			
			List<Group>                     bmps;
			List<ResPatchOperation>         rops;
			List<WallpaperExtraOperation>   wops;
			List<VisualStyleExtraOperation> vops;
			
			GetAllOperations( PackageInfo.Package, out bmps, out rops, out wops, out vops);
			
			///////// Bitmaps
			
			__bitmaps.Items.Clear();
			Pair<String,Group> selectedGroupPair = null;
			foreach(Group group in bmps) {
				
				Pair<String,Group> groupPair = new Pair<String,Group>(group.Name, group);
				__bitmaps.Items.Add( groupPair );
				if( preset.Group == group ) selectedGroupPair = groupPair;
			}
			__bitmaps.SelectedItem = selectedGroupPair;
			
			///////// Welcome Screen
			
			__logon.Items.Clear();
			Pair<String,ResPatchOperation> selectedLogonPair = null;
			foreach(ResPatchOperation rop in rops) {
				
				Pair<String,ResPatchOperation> ropPair = new Pair<String,ResPatchOperation>(rop.Name, rop);
				__logon.Items.Add( ropPair );
				if( preset.WelcomeScreen == rop ) selectedLogonPair = ropPair;
			}
			__logon.SelectedItem = selectedLogonPair;
			
			///////// Wallpaper
			
			__wallpaper.Items.Clear();
			Pair<String,WallpaperExtraOperation> selectedWallPair = null;
			foreach(WallpaperExtraOperation wop in wops) {
				
				Pair<String,WallpaperExtraOperation> wopPair = new Pair<String,WallpaperExtraOperation>( wop.Name.Length > 0 ? wop.Name : Path.GetFileNameWithoutExtension( wop.Path ), wop);
				__wallpaper.Items.Add( wopPair );
				if( preset.Wallpaper == wop ) selectedWallPair = wopPair;
			}
			__wallpaper.SelectedItem = selectedWallPair;
			
			///////// Visual Style
			
			__visualStyles.Items.Clear();
			Pair<String,VisualStyleExtraOperation> selecteVisPair = null;
			foreach(VisualStyleExtraOperation vop in vops) {
				
				Pair<String,VisualStyleExtraOperation> vopPair = new Pair<String,VisualStyleExtraOperation>(vop.Name, vop);
				__visualStyles.Items.Add( vopPair );
				if( preset.VisualStyle == vop ) selecteVisPair = vopPair;
			}
			__visualStyles.SelectedItem = selecteVisPair;
			
			//////////////////////////////////////
			
			ComposePreview();
		}
		
		private void ComposePreview() {
			
			Bitmap preview = new Bitmap( __preview.ClientSize.Width, __preview.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb );
			Graphics g = Graphics.FromImage( preview );
			g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			g.FillRectangle( new SolidBrush( SystemColors.AppWorkspace ), 0, 0, preview.Width, preview.Height );
			
			Image bmp;
			if( __logonPreview.Checked ) {
				
				///////// Welcome Screen
				Pair<String,ResPatchOperation> rPair = __logon.SelectedItem as Pair<String,ResPatchOperation>;
				bmp = rPair.Y.PreviewImage;
				if( bmp != null ) g.DrawImage( bmp, 0, 0, preview.Width, preview.Height );
				
			} else {
				
				///////// Wallpaper
				Pair<String,WallpaperExtraOperation> wPair = __wallpaper.SelectedItem as Pair<String,WallpaperExtraOperation>;
				String wPath = wPair.Y.Package.RootDirectory.GetFile( wPair.Y.Path ).FullName;
				if( File.Exists( wPath ) )
					using(bmp = Bitmap.FromFile( wPath )) {
						g.DrawImage( bmp, 0, 0, preview.Width, preview.Height );
					}
				
				///////// Visual Style
				Pair<String,VisualStyleExtraOperation> vPair = __visualStyles.SelectedItem as Pair<String,VisualStyleExtraOperation>;
				bmp = vPair.Y.PreviewImage;
				if( bmp != null ) g.DrawImage( bmp, 0, 0, preview.Width, preview.Height );
				
				///////// Bitmaps
				Pair<String,Group> gPair = __bitmaps.SelectedItem as Pair<String,Group>;
				bmp = gPair.Y.PreviewImage;
				if( bmp != null ) g.DrawImage( bmp, 0, 0, preview.Width, preview.Height );
			}
			
			if( __preview.Image != null ) __preview.Image.Dispose();
			__preview.Image = preview;
			
		}
		

		
#endregion
		
	}
}
