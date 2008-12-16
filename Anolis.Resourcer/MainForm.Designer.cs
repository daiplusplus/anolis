namespace Anolis.Resourcer {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.ToolStripSeparator @__tSep1;
			System.Windows.Forms.ToolStripSeparator @__tSep2;
			System.Windows.Forms.ToolStripStatusLabel @__sSep1;
			System.Windows.Forms.ToolStripStatusLabel @__sSep2;
			this.@__resources = new System.Windows.Forms.TreeView();
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__sfd = new System.Windows.Forms.SaveFileDialog();
			this.@__status = new System.Windows.Forms.StatusStrip();
			this.@__sPath = new System.Windows.Forms.ToolStripStatusLabel();
			this.@__sType = new System.Windows.Forms.ToolStripStatusLabel();
			this.@__sSize = new System.Windows.Forms.ToolStripStatusLabel();
			this.@__t = new System.Windows.Forms.ToolStrip();
			this.@__tSrcOpen = new System.Windows.Forms.ToolStripSplitButton();
			this.@__tSrcSave = new System.Windows.Forms.ToolStripButton();
			this.@__tSrcReload = new System.Windows.Forms.ToolStripButton();
			this.@__tResAdd = new System.Windows.Forms.ToolStripButton();
			this.@__tResExtract = new System.Windows.Forms.ToolStripButton();
			this.@__tResReplace = new System.Windows.Forms.ToolStripButton();
			this.@__tResDelete = new System.Windows.Forms.ToolStripButton();
			this.@__tGenOptions = new System.Windows.Forms.ToolStripButton();
			@__tSep1 = new System.Windows.Forms.ToolStripSeparator();
			@__tSep2 = new System.Windows.Forms.ToolStripSeparator();
			@__sSep1 = new System.Windows.Forms.ToolStripStatusLabel();
			@__sSep2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.@__split.Panel1.SuspendLayout();
			this.@__split.SuspendLayout();
			this.@__status.SuspendLayout();
			this.@__t.SuspendLayout();
			this.SuspendLayout();
			// 
			// __tSep1
			// 
			@__tSep1.Name = "__tSep1";
			@__tSep1.Size = new System.Drawing.Size(6, 74);
			// 
			// __tSep2
			// 
			@__tSep2.Name = "__tSep2";
			@__tSep2.Padding = new System.Windows.Forms.Padding(3);
			@__tSep2.Size = new System.Drawing.Size(6, 74);
			// 
			// __sSep1
			// 
			@__sSep1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			@__sSep1.Name = "__sSep1";
			@__sSep1.Size = new System.Drawing.Size(4, 17);
			// 
			// __sSep2
			// 
			@__sSep2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			@__sSep2.Name = "__sSep2";
			@__sSep2.Size = new System.Drawing.Size(4, 17);
			// 
			// __resources
			// 
			this.@__resources.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__resources.Location = new System.Drawing.Point(0, 0);
			this.@__resources.Name = "__resources";
			this.@__resources.Size = new System.Drawing.Size(134, 380);
			this.@__resources.TabIndex = 3;
			// 
			// __split
			// 
			this.@__split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.@__split.Location = new System.Drawing.Point(0, 74);
			this.@__split.Margin = new System.Windows.Forms.Padding(0);
			this.@__split.Name = "__split";
			// 
			// __split.Panel1
			// 
			this.@__split.Panel1.Controls.Add(this.@__resources);
			this.@__split.Size = new System.Drawing.Size(630, 380);
			this.@__split.SplitterDistance = 134;
			this.@__split.TabIndex = 4;
			// 
			// __status
			// 
			this.@__status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__sPath,
            @__sSep1,
            this.@__sType,
            @__sSep2,
            this.@__sSize});
			this.@__status.Location = new System.Drawing.Point(0, 454);
			this.@__status.Name = "__status";
			this.@__status.Size = new System.Drawing.Size(630, 22);
			this.@__status.TabIndex = 5;
			this.@__status.Text = "statusStrip1";
			// 
			// __sPath
			// 
			this.@__sPath.Name = "__sPath";
			this.@__sPath.Size = new System.Drawing.Size(214, 17);
			this.@__sPath.Text = "C:\\Windows\\system32\\shell32.dll,9\\1\\1033";
			// 
			// __sType
			// 
			this.@__sType.Name = "__sType";
			this.@__sType.Size = new System.Drawing.Size(119, 17);
			this.@__sType.Text = "UnknownResourceData";
			// 
			// __sSize
			// 
			this.@__sSize.Name = "__sSize";
			this.@__sSize.Size = new System.Drawing.Size(61, 17);
			this.@__sSize.Text = "1024 Bytes";
			// 
			// __t
			// 
			this.@__t.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tSrcOpen,
            this.@__tSrcSave,
            this.@__tSrcReload,
            @__tSep1,
            this.@__tResAdd,
            this.@__tResExtract,
            this.@__tResReplace,
            this.@__tResDelete,
            @__tSep2,
            this.@__tGenOptions});
			this.@__t.Location = new System.Drawing.Point(0, 0);
			this.@__t.Name = "__t";
			this.@__t.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.@__t.Size = new System.Drawing.Size(630, 74);
			this.@__t.TabIndex = 6;
			this.@__t.Text = "toolStrip1";
			// 
			// __tSrcOpen
			// 
			this.@__tSrcOpen.Image = global::Anolis.Resourcer.Properties.Resources.SrcOpen;
			this.@__tSrcOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.@__tSrcOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tSrcOpen.Name = "__tSrcOpen";
			this.@__tSrcOpen.Padding = new System.Windows.Forms.Padding(3);
			this.@__tSrcOpen.Size = new System.Drawing.Size(70, 71);
			this.@__tSrcOpen.Text = "Open";
			this.@__tSrcOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tSrcSave
			// 
			this.@__tSrcSave.Image = global::Anolis.Resourcer.Properties.Resources.SrcSave;
			this.@__tSrcSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.@__tSrcSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tSrcSave.Name = "__tSrcSave";
			this.@__tSrcSave.Padding = new System.Windows.Forms.Padding(3);
			this.@__tSrcSave.Size = new System.Drawing.Size(58, 71);
			this.@__tSrcSave.Text = "Save";
			this.@__tSrcSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tSrcReload
			// 
			this.@__tSrcReload.Image = global::Anolis.Resourcer.Properties.Resources.SrcRev;
			this.@__tSrcReload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.@__tSrcReload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tSrcReload.Name = "__tSrcReload";
			this.@__tSrcReload.Padding = new System.Windows.Forms.Padding(3);
			this.@__tSrcReload.Size = new System.Drawing.Size(58, 71);
			this.@__tSrcReload.Text = "Revert";
			this.@__tSrcReload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tResAdd
			// 
			this.@__tResAdd.Image = global::Anolis.Resourcer.Properties.Resources.ResAdd;
			this.@__tResAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.@__tResAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResAdd.Name = "__tResAdd";
			this.@__tResAdd.Padding = new System.Windows.Forms.Padding(3);
			this.@__tResAdd.Size = new System.Drawing.Size(58, 71);
			this.@__tResAdd.Text = "Add";
			this.@__tResAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tResExtract
			// 
			this.@__tResExtract.Image = global::Anolis.Resourcer.Properties.Resources.ResExt;
			this.@__tResExtract.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.@__tResExtract.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResExtract.Name = "__tResExtract";
			this.@__tResExtract.Padding = new System.Windows.Forms.Padding(3);
			this.@__tResExtract.Size = new System.Drawing.Size(58, 71);
			this.@__tResExtract.Text = "Extract";
			this.@__tResExtract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tResReplace
			// 
			this.@__tResReplace.Image = global::Anolis.Resourcer.Properties.Resources.ResRep;
			this.@__tResReplace.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.@__tResReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResReplace.Name = "__tResReplace";
			this.@__tResReplace.Padding = new System.Windows.Forms.Padding(3);
			this.@__tResReplace.Size = new System.Drawing.Size(58, 71);
			this.@__tResReplace.Text = "Replace";
			this.@__tResReplace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tResDelete
			// 
			this.@__tResDelete.Image = global::Anolis.Resourcer.Properties.Resources.ResDel;
			this.@__tResDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.@__tResDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResDelete.Name = "__tResDelete";
			this.@__tResDelete.Padding = new System.Windows.Forms.Padding(3);
			this.@__tResDelete.Size = new System.Drawing.Size(58, 71);
			this.@__tResDelete.Text = "Delete";
			this.@__tResDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tGenOptions
			// 
			this.@__tGenOptions.Image = global::Anolis.Resourcer.Properties.Resources.Options;
			this.@__tGenOptions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.@__tGenOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tGenOptions.Name = "__tGenOptions";
			this.@__tGenOptions.Padding = new System.Windows.Forms.Padding(3);
			this.@__tGenOptions.Size = new System.Drawing.Size(58, 71);
			this.@__tGenOptions.Text = "Options";
			this.@__tGenOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(630, 476);
			this.Controls.Add(this.@__t);
			this.Controls.Add(this.@__status);
			this.Controls.Add(this.@__split);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Anolis Resourcer";
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.ResumeLayout(false);
			this.@__status.ResumeLayout(false);
			this.@__status.PerformLayout();
			this.@__t.ResumeLayout(false);
			this.@__t.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView __resources;
		private System.Windows.Forms.SplitContainer __split;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.SaveFileDialog __sfd;
		private System.Windows.Forms.StatusStrip __status;
		private System.Windows.Forms.ToolStrip __t;
		private System.Windows.Forms.ToolStripButton __tResExtract;
		private System.Windows.Forms.ToolStripButton __tSrcSave;
		private System.Windows.Forms.ToolStripButton __tSrcReload;
		private System.Windows.Forms.ToolStripButton __tResAdd;
		private System.Windows.Forms.ToolStripButton __tResReplace;
		private System.Windows.Forms.ToolStripButton __tResDelete;
		private System.Windows.Forms.ToolStripButton __tGenOptions;
		private System.Windows.Forms.ToolStripStatusLabel __sPath;
		private System.Windows.Forms.ToolStripStatusLabel __sType;
		private System.Windows.Forms.ToolStripStatusLabel __sSize;
		private System.Windows.Forms.ToolStripSplitButton __tSrcOpen;
	}
}

