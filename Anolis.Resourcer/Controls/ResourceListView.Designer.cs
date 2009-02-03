namespace Anolis.Resourcer.Controls {
	partial class ResourceListView {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ToolStripSeparator @__cArrangeSep;
			System.Windows.Forms.ToolStripLabel @__tIconSizeLbl;
			System.Windows.Forms.ToolStripSeparator @__tSizeSep;
			System.Windows.Forms.ToolStripSeparator @__tSep1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceListView));
			System.Windows.Forms.ToolStripSeparator @__tSep2;
			this.@__list = new System.Windows.Forms.ListView();
			this.@__c = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.@__cView = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cViewThumbnails = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cViewTiles = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cViewIcons = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cViewList = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cViewDetails = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cArrange = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cArrangeName = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cArrangeSize = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cArrangeType = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cArrangeLang = new System.Windows.Forms.ToolStripMenuItem();
			this.@__cArrangeGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.@__t = new System.Windows.Forms.ToolStrip();
			this.@__tIconSize = new System.Windows.Forms.ToolStripDropDownButton();
			this.@__tSize16 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tSize24 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tSize32 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tSize48 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tSize128 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tSize256 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tSizeCustom = new System.Windows.Forms.ToolStripMenuItem();
			this.@__iconDepth = new System.Windows.Forms.ToolStripDropDownButton();
			this.@__tColor32 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tColor24 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tColor16 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tColor8 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tColor4 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tThumbnailsLbl = new System.Windows.Forms.ToolStripLabel();
			this.@__tThumbnails = new System.Windows.Forms.ToolStripDropDownButton();
			this.@__tThumb64 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tThumb96 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__tThumb256 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__progessBar = new System.Windows.Forms.ToolStripProgressBar();
			this.@__bg = new System.ComponentModel.BackgroundWorker();
			@__cArrangeSep = new System.Windows.Forms.ToolStripSeparator();
			@__tIconSizeLbl = new System.Windows.Forms.ToolStripLabel();
			@__tSizeSep = new System.Windows.Forms.ToolStripSeparator();
			@__tSep1 = new System.Windows.Forms.ToolStripSeparator();
			@__tSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.@__c.SuspendLayout();
			this.@__t.SuspendLayout();
			this.SuspendLayout();
			// 
			// __cArrangeSep
			// 
			@__cArrangeSep.Name = "__cArrangeSep";
			@__cArrangeSep.Size = new System.Drawing.Size(145, 6);
			// 
			// __tIconSizeLbl
			// 
			@__tIconSizeLbl.Name = "__tIconSizeLbl";
			@__tIconSizeLbl.Size = new System.Drawing.Size(37, 22);
			@__tIconSizeLbl.Text = "Icons:";
			// 
			// __tSizeSep
			// 
			@__tSizeSep.Name = "__tSizeSep";
			@__tSizeSep.Size = new System.Drawing.Size(149, 6);
			// 
			// __tSep1
			// 
			@__tSep1.Name = "__tSep1";
			@__tSep1.Size = new System.Drawing.Size(6, 25);
			// 
			// __list
			// 
			this.@__list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__list.Location = new System.Drawing.Point(0, 28);
			this.@__list.Name = "__list";
			this.@__list.Size = new System.Drawing.Size(537, 316);
			this.@__list.TabIndex = 0;
			this.@__list.UseCompatibleStateImageBehavior = false;
			// 
			// __c
			// 
			this.@__c.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__cView,
            this.@__cArrange});
			this.@__c.Name = "__c";
			this.@__c.Size = new System.Drawing.Size(158, 48);
			// 
			// __cView
			// 
			this.@__cView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__cViewThumbnails,
            this.@__cViewTiles,
            this.@__cViewIcons,
            this.@__cViewList,
            this.@__cViewDetails});
			this.@__cView.Name = "__cView";
			this.@__cView.Size = new System.Drawing.Size(157, 22);
			this.@__cView.Text = "View";
			// 
			// __cViewThumbnails
			// 
			this.@__cViewThumbnails.Name = "__cViewThumbnails";
			this.@__cViewThumbnails.Size = new System.Drawing.Size(127, 22);
			this.@__cViewThumbnails.Text = "Thumbnails";
			// 
			// __cViewTiles
			// 
			this.@__cViewTiles.Name = "__cViewTiles";
			this.@__cViewTiles.Size = new System.Drawing.Size(127, 22);
			this.@__cViewTiles.Text = "Tiles";
			// 
			// __cViewIcons
			// 
			this.@__cViewIcons.Name = "__cViewIcons";
			this.@__cViewIcons.Size = new System.Drawing.Size(127, 22);
			this.@__cViewIcons.Text = "Icons";
			// 
			// __cViewList
			// 
			this.@__cViewList.Name = "__cViewList";
			this.@__cViewList.Size = new System.Drawing.Size(127, 22);
			this.@__cViewList.Text = "List";
			// 
			// __cViewDetails
			// 
			this.@__cViewDetails.Name = "__cViewDetails";
			this.@__cViewDetails.Size = new System.Drawing.Size(127, 22);
			this.@__cViewDetails.Text = "Details";
			// 
			// __cArrange
			// 
			this.@__cArrange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__cArrangeName,
            this.@__cArrangeSize,
            this.@__cArrangeType,
            this.@__cArrangeLang,
            @__cArrangeSep,
            this.@__cArrangeGroup});
			this.@__cArrange.Name = "__cArrange";
			this.@__cArrange.Size = new System.Drawing.Size(157, 22);
			this.@__cArrange.Text = "Arrange Icons By";
			// 
			// __cArrangeName
			// 
			this.@__cArrangeName.Name = "__cArrangeName";
			this.@__cArrangeName.Size = new System.Drawing.Size(148, 22);
			this.@__cArrangeName.Text = "Name";
			// 
			// __cArrangeSize
			// 
			this.@__cArrangeSize.Name = "__cArrangeSize";
			this.@__cArrangeSize.Size = new System.Drawing.Size(148, 22);
			this.@__cArrangeSize.Text = "Size";
			// 
			// __cArrangeType
			// 
			this.@__cArrangeType.Name = "__cArrangeType";
			this.@__cArrangeType.Size = new System.Drawing.Size(148, 22);
			this.@__cArrangeType.Text = "Type";
			// 
			// __cArrangeLang
			// 
			this.@__cArrangeLang.Name = "__cArrangeLang";
			this.@__cArrangeLang.Size = new System.Drawing.Size(148, 22);
			this.@__cArrangeLang.Text = "Language";
			// 
			// __cArrangeGroup
			// 
			this.@__cArrangeGroup.Name = "__cArrangeGroup";
			this.@__cArrangeGroup.Size = new System.Drawing.Size(148, 22);
			this.@__cArrangeGroup.Text = "Show in Groups";
			// 
			// __t
			// 
			this.@__t.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__t.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            @__tIconSizeLbl,
            this.@__tIconSize,
            this.@__iconDepth,
            @__tSep1,
            this.@__tThumbnailsLbl,
            this.@__tThumbnails,
            @__tSep2,
            this.@__progessBar});
			this.@__t.Location = new System.Drawing.Point(0, 0);
			this.@__t.Name = "__t";
			this.@__t.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.@__t.Size = new System.Drawing.Size(537, 25);
			this.@__t.TabIndex = 1;
			this.@__t.Text = "toolStrip1";
			// 
			// __tIconSize
			// 
			this.@__tIconSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.@__tIconSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tSize16,
            this.@__tSize24,
            this.@__tSize32,
            this.@__tSize48,
            this.@__tSize128,
            this.@__tSize256,
            @__tSizeSep,
            this.@__tSizeCustom});
			this.@__tIconSize.Image = ((System.Drawing.Image)(resources.GetObject("__tIconSize.Image")));
			this.@__tIconSize.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tIconSize.Name = "__tIconSize";
			this.@__tIconSize.Size = new System.Drawing.Size(50, 22);
			this.@__tIconSize.Text = "32x32";
			// 
			// __tSize16
			// 
			this.@__tSize16.Name = "__tSize16";
			this.@__tSize16.Size = new System.Drawing.Size(152, 22);
			this.@__tSize16.Text = "16x16";
			// 
			// __tSize24
			// 
			this.@__tSize24.Name = "__tSize24";
			this.@__tSize24.Size = new System.Drawing.Size(152, 22);
			this.@__tSize24.Text = "24x24";
			// 
			// __tSize32
			// 
			this.@__tSize32.Name = "__tSize32";
			this.@__tSize32.Size = new System.Drawing.Size(152, 22);
			this.@__tSize32.Text = "32x32";
			// 
			// __tSize48
			// 
			this.@__tSize48.Name = "__tSize48";
			this.@__tSize48.Size = new System.Drawing.Size(152, 22);
			this.@__tSize48.Text = "48x48";
			// 
			// __tSize128
			// 
			this.@__tSize128.Name = "__tSize128";
			this.@__tSize128.Size = new System.Drawing.Size(152, 22);
			this.@__tSize128.Text = "128x128";
			// 
			// __tSize256
			// 
			this.@__tSize256.Name = "__tSize256";
			this.@__tSize256.Size = new System.Drawing.Size(152, 22);
			this.@__tSize256.Text = "256x256";
			// 
			// __tSizeCustom
			// 
			this.@__tSizeCustom.Name = "__tSizeCustom";
			this.@__tSizeCustom.Size = new System.Drawing.Size(152, 22);
			this.@__tSizeCustom.Text = "Custom...";
			// 
			// __iconDepth
			// 
			this.@__iconDepth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.@__iconDepth.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tColor32,
            this.@__tColor24,
            this.@__tColor16,
            this.@__tColor8,
            this.@__tColor4});
			this.@__iconDepth.Image = ((System.Drawing.Image)(resources.GetObject("__iconDepth.Image")));
			this.@__iconDepth.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__iconDepth.Name = "__iconDepth";
			this.@__iconDepth.Size = new System.Drawing.Size(49, 22);
			this.@__iconDepth.Text = "Color:";
			// 
			// __tColor32
			// 
			this.@__tColor32.Name = "__tColor32";
			this.@__tColor32.Size = new System.Drawing.Size(152, 22);
			this.@__tColor32.Text = "Up to 32-bit";
			// 
			// __tColor24
			// 
			this.@__tColor24.Name = "__tColor24";
			this.@__tColor24.Size = new System.Drawing.Size(152, 22);
			this.@__tColor24.Text = "Up to 24-bit";
			// 
			// __tColor16
			// 
			this.@__tColor16.Name = "__tColor16";
			this.@__tColor16.Size = new System.Drawing.Size(152, 22);
			this.@__tColor16.Text = "Up to 16-bit";
			// 
			// __tColor8
			// 
			this.@__tColor8.Name = "__tColor8";
			this.@__tColor8.Size = new System.Drawing.Size(152, 22);
			this.@__tColor8.Text = "Up to 256-color";
			// 
			// __tColor4
			// 
			this.@__tColor4.Name = "__tColor4";
			this.@__tColor4.Size = new System.Drawing.Size(152, 22);
			this.@__tColor4.Text = "Up to 16-color";
			// 
			// __tThumbnailsLbl
			// 
			this.@__tThumbnailsLbl.Name = "__tThumbnailsLbl";
			this.@__tThumbnailsLbl.Size = new System.Drawing.Size(64, 22);
			this.@__tThumbnailsLbl.Text = "Thumbnails:";
			// 
			// __tThumbnails
			// 
			this.@__tThumbnails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.@__tThumbnails.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tThumb64,
            this.@__tThumb96,
            this.@__tThumb256});
			this.@__tThumbnails.Image = ((System.Drawing.Image)(resources.GetObject("__tThumbnails.Image")));
			this.@__tThumbnails.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tThumbnails.Name = "__tThumbnails";
			this.@__tThumbnails.Size = new System.Drawing.Size(50, 22);
			this.@__tThumbnails.Text = "96x96";
			// 
			// __tThumb64
			// 
			this.@__tThumb64.Name = "__tThumb64";
			this.@__tThumb64.Size = new System.Drawing.Size(152, 22);
			this.@__tThumb64.Text = "64x64";
			// 
			// __tThumb96
			// 
			this.@__tThumb96.Name = "__tThumb96";
			this.@__tThumb96.Size = new System.Drawing.Size(152, 22);
			this.@__tThumb96.Text = "96x96";
			// 
			// __tThumb256
			// 
			this.@__tThumb256.Name = "__tThumb256";
			this.@__tThumb256.Size = new System.Drawing.Size(152, 22);
			this.@__tThumb256.Text = "256x256";
			// 
			// __progessBar
			// 
			this.@__progessBar.Name = "__progessBar";
			this.@__progessBar.Size = new System.Drawing.Size(150, 22);
			// 
			// __tSep2
			// 
			@__tSep2.Name = "__tSep2";
			@__tSep2.Size = new System.Drawing.Size(6, 25);
			// 
			// __bg
			// 
			this.@__bg.WorkerReportsProgress = true;
			// 
			// ResourceListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__t);
			this.Controls.Add(this.@__list);
			this.Name = "ResourceListView";
			this.Size = new System.Drawing.Size(537, 344);
			this.@__c.ResumeLayout(false);
			this.@__t.ResumeLayout(false);
			this.@__t.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView __list;
		private System.Windows.Forms.ContextMenuStrip __c;
		private System.Windows.Forms.ToolStripMenuItem __cView;
		private System.Windows.Forms.ToolStripMenuItem __cViewThumbnails;
		private System.Windows.Forms.ToolStripMenuItem __cViewTiles;
		private System.Windows.Forms.ToolStripMenuItem __cViewIcons;
		private System.Windows.Forms.ToolStripMenuItem __cViewList;
		private System.Windows.Forms.ToolStripMenuItem __cViewDetails;
		private System.Windows.Forms.ToolStripMenuItem __cArrange;
		private System.Windows.Forms.ToolStripMenuItem __cArrangeName;
		private System.Windows.Forms.ToolStripMenuItem __cArrangeSize;
		private System.Windows.Forms.ToolStripMenuItem __cArrangeType;
		private System.Windows.Forms.ToolStripMenuItem __cArrangeLang;
		private System.Windows.Forms.ToolStripMenuItem __cArrangeGroup;
		private System.Windows.Forms.ToolStrip __t;
		private System.Windows.Forms.ToolStripDropDownButton __tIconSize;
		private System.Windows.Forms.ToolStripMenuItem __tSize16;
		private System.Windows.Forms.ToolStripMenuItem __tSize24;
		private System.Windows.Forms.ToolStripMenuItem __tSize32;
		private System.Windows.Forms.ToolStripMenuItem __tSize48;
		private System.Windows.Forms.ToolStripMenuItem __tSize128;
		private System.Windows.Forms.ToolStripMenuItem __tSize256;
		private System.Windows.Forms.ToolStripMenuItem __tSizeCustom;
		private System.Windows.Forms.ToolStripLabel __tThumbnailsLbl;
		private System.Windows.Forms.ToolStripDropDownButton __tThumbnails;
		private System.Windows.Forms.ToolStripMenuItem __tThumb64;
		private System.Windows.Forms.ToolStripMenuItem __tThumb96;
		private System.Windows.Forms.ToolStripMenuItem __tThumb256;
		private System.Windows.Forms.ToolStripDropDownButton __iconDepth;
		private System.Windows.Forms.ToolStripMenuItem __tColor32;
		private System.Windows.Forms.ToolStripMenuItem __tColor24;
		private System.Windows.Forms.ToolStripMenuItem __tColor16;
		private System.Windows.Forms.ToolStripMenuItem __tColor8;
		private System.Windows.Forms.ToolStripMenuItem __tColor4;
		private System.Windows.Forms.ToolStripProgressBar __progessBar;
		private System.ComponentModel.BackgroundWorker __bg;
	}
}
