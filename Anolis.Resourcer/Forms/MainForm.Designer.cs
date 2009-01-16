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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.@__tSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.@__tSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.@__sSep1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.@__sSep2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.@__resCMSep = new System.Windows.Forms.ToolStripSeparator();
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
			this.@__tResUndo = new System.Windows.Forms.ToolStripButton();
			this.@__tGenOptions = new System.Windows.Forms.ToolStripButton();
			this.@__resCM = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.@__resCMCast = new System.Windows.Forms.ToolStripMenuItem();
			this.@__resCMExtract = new System.Windows.Forms.ToolStripMenuItem();
			this.@__resCMReplace = new System.Windows.Forms.ToolStripMenuItem();
			this.@__resCMDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.@__resCMCancel = new System.Windows.Forms.ToolStripMenuItem();
			this.@__treeStateImages = new System.Windows.Forms.ImageList(this.components);
			this.@__split.Panel1.SuspendLayout();
			this.@__split.SuspendLayout();
			this.@__status.SuspendLayout();
			this.@__t.SuspendLayout();
			this.@__resCM.SuspendLayout();
			this.SuspendLayout();
			// 
			// __tSep1
			// 
			this.@__tSep1.Name = "__tSep1";
			this.@__tSep1.Size = new System.Drawing.Size(6, 74);
			// 
			// __tSep2
			// 
			this.@__tSep2.Name = "__tSep2";
			this.@__tSep2.Padding = new System.Windows.Forms.Padding(3);
			this.@__tSep2.Size = new System.Drawing.Size(6, 74);
			// 
			// __sSep1
			// 
			this.@__sSep1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.@__sSep1.Name = "__sSep1";
			this.@__sSep1.Size = new System.Drawing.Size(4, 17);
			// 
			// __sSep2
			// 
			this.@__sSep2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.@__sSep2.Name = "__sSep2";
			this.@__sSep2.Size = new System.Drawing.Size(4, 17);
			// 
			// __resCMSep
			// 
			this.@__resCMSep.Name = "__resCMSep";
			this.@__resCMSep.Size = new System.Drawing.Size(141, 6);
			// 
			// __resources
			// 
			this.@__resources.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.@__resources.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__resources.Location = new System.Drawing.Point(0, 0);
			this.@__resources.Name = "__resources";
			this.@__resources.Size = new System.Drawing.Size(146, 284);
			this.@__resources.TabIndex = 3;
			// 
			// __split
			// 
			this.@__split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__split.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.@__split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.@__split.Location = new System.Drawing.Point(0, 74);
			this.@__split.Margin = new System.Windows.Forms.Padding(0);
			this.@__split.Name = "__split";
			// 
			// __split.Panel1
			// 
			this.@__split.Panel1.Controls.Add(this.@__resources);
			this.@__split.Size = new System.Drawing.Size(629, 288);
			this.@__split.SplitterDistance = 150;
			this.@__split.TabIndex = 4;
			// 
			// __status
			// 
			this.@__status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__sPath,
            this.@__sSep1,
            this.@__sType,
            this.@__sSep2,
            this.@__sSize});
			this.@__status.Location = new System.Drawing.Point(0, 362);
			this.@__status.Name = "__status";
			this.@__status.Size = new System.Drawing.Size(629, 22);
			this.@__status.TabIndex = 5;
			this.@__status.Text = "statusStrip1";
			// 
			// __sPath
			// 
			this.@__sPath.Name = "__sPath";
			this.@__sPath.Size = new System.Drawing.Size(124, 17);
			this.@__sPath.Text = "                                       ";
			// 
			// __sType
			// 
			this.@__sType.Name = "__sType";
			this.@__sType.Size = new System.Drawing.Size(121, 17);
			this.@__sType.Text = "                                      ";
			// 
			// __sSize
			// 
			this.@__sSize.Name = "__sSize";
			this.@__sSize.Size = new System.Drawing.Size(61, 17);
			this.@__sSize.Text = "                  ";
			// 
			// __t
			// 
			this.@__t.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__t.ImageScalingSize = new System.Drawing.Size(48, 48);
			this.@__t.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tSrcOpen,
            this.@__tSrcSave,
            this.@__tSrcReload,
            this.@__tSep1,
            this.@__tResAdd,
            this.@__tResExtract,
            this.@__tResReplace,
            this.@__tResDelete,
            this.@__tResUndo,
            this.@__tSep2,
            this.@__tGenOptions});
			this.@__t.Location = new System.Drawing.Point(0, 0);
			this.@__t.Name = "__t";
			this.@__t.Size = new System.Drawing.Size(629, 74);
			this.@__t.TabIndex = 6;
			// 
			// __tSrcOpen
			// 
			this.@__tSrcOpen.Image = global::Anolis.Resourcer.Properties.Resources.Toolbar_SrcOpen;
			this.@__tSrcOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tSrcOpen.Name = "__tSrcOpen";
			this.@__tSrcOpen.Padding = new System.Windows.Forms.Padding(3);
			this.@__tSrcOpen.Size = new System.Drawing.Size(70, 71);
			this.@__tSrcOpen.Text = "Open";
			this.@__tSrcOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__tSrcOpen.ToolTipText = "Open a file containing resources";
			// 
			// __tSrcSave
			// 
			this.@__tSrcSave.Image = global::Anolis.Resourcer.Properties.Resources.Toolbar_SrcSave;
			this.@__tSrcSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tSrcSave.Name = "__tSrcSave";
			this.@__tSrcSave.Padding = new System.Windows.Forms.Padding(3);
			this.@__tSrcSave.Size = new System.Drawing.Size(58, 71);
			this.@__tSrcSave.Text = "Save";
			this.@__tSrcSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__tSrcSave.ToolTipText = "Save any changes made to the current file containing resources";
			// 
			// __tSrcReload
			// 
			this.@__tSrcReload.Image = global::Anolis.Resourcer.Properties.Resources.Toolbar_SrcRev;
			this.@__tSrcReload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tSrcReload.Name = "__tSrcReload";
			this.@__tSrcReload.Padding = new System.Windows.Forms.Padding(3);
			this.@__tSrcReload.Size = new System.Drawing.Size(58, 71);
			this.@__tSrcReload.Text = "Revert";
			this.@__tSrcReload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__tSrcReload.ToolTipText = "Revert the resource containing file to its last-saved state";
			// 
			// __tResAdd
			// 
			this.@__tResAdd.Image = global::Anolis.Resourcer.Properties.Resources.Toolbar_ResAdd;
			this.@__tResAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResAdd.Name = "__tResAdd";
			this.@__tResAdd.Padding = new System.Windows.Forms.Padding(3);
			this.@__tResAdd.Size = new System.Drawing.Size(58, 71);
			this.@__tResAdd.Text = "Add";
			this.@__tResAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__tResAdd.ToolTipText = "Add a new resource";
			// 
			// __tResExtract
			// 
			this.@__tResExtract.Image = global::Anolis.Resourcer.Properties.Resources.Toolbar_ResExt;
			this.@__tResExtract.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResExtract.Name = "__tResExtract";
			this.@__tResExtract.Padding = new System.Windows.Forms.Padding(3);
			this.@__tResExtract.Size = new System.Drawing.Size(58, 71);
			this.@__tResExtract.Text = "Extract";
			this.@__tResExtract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__tResExtract.ToolTipText = "Extract the currently selected resource to disk";
			// 
			// __tResReplace
			// 
			this.@__tResReplace.Image = global::Anolis.Resourcer.Properties.Resources.Toolbar_ResRep;
			this.@__tResReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResReplace.Name = "__tResReplace";
			this.@__tResReplace.Padding = new System.Windows.Forms.Padding(3);
			this.@__tResReplace.Size = new System.Drawing.Size(58, 71);
			this.@__tResReplace.Text = "Replace";
			this.@__tResReplace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__tResReplace.ToolTipText = "Replace the currently selected resource wth a new file from disk";
			// 
			// __tResDelete
			// 
			this.@__tResDelete.Image = global::Anolis.Resourcer.Properties.Resources.Toolbar_ResDel;
			this.@__tResDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResDelete.Name = "__tResDelete";
			this.@__tResDelete.Padding = new System.Windows.Forms.Padding(3);
			this.@__tResDelete.Size = new System.Drawing.Size(58, 71);
			this.@__tResDelete.Text = "Delete";
			this.@__tResDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__tResDelete.ToolTipText = "Delete the currently selected resource";
			// 
			// __tResUndo
			// 
			this.@__tResUndo.Image = global::Anolis.Resourcer.Properties.Resources.Toolbar_ResRev;
			this.@__tResUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResUndo.Name = "__tResUndo";
			this.@__tResUndo.Size = new System.Drawing.Size(52, 71);
			this.@__tResUndo.Text = "Cancel";
			this.@__tResUndo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__tResUndo.ToolTipText = "Cancel the pending operation on the current resource";
			// 
			// __tGenOptions
			// 
			this.@__tGenOptions.Image = global::Anolis.Resourcer.Properties.Resources.Toolbar_Options;
			this.@__tGenOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tGenOptions.Name = "__tGenOptions";
			this.@__tGenOptions.Padding = new System.Windows.Forms.Padding(3);
			this.@__tGenOptions.Size = new System.Drawing.Size(58, 71);
			this.@__tGenOptions.Text = "Options";
			this.@__tGenOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__tGenOptions.ToolTipText = "Options, Credits, and License";
			// 
			// __resCM
			// 
			this.@__resCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__resCMCast,
            this.@__resCMSep,
            this.@__resCMExtract,
            this.@__resCMReplace,
            this.@__resCMDelete,
            this.@__resCMCancel});
			this.@__resCM.Name = "__resCM";
			this.@__resCM.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.@__resCM.Size = new System.Drawing.Size(145, 120);
			// 
			// __resCMCast
			// 
			this.@__resCMCast.Name = "__resCMCast";
			this.@__resCMCast.Size = new System.Drawing.Size(144, 22);
			this.@__resCMCast.Text = "Cast {0} as:";
			// 
			// __resCMExtract
			// 
			this.@__resCMExtract.Name = "__resCMExtract";
			this.@__resCMExtract.Size = new System.Drawing.Size(144, 22);
			this.@__resCMExtract.Text = "Extract {0}";
			// 
			// __resCMReplace
			// 
			this.@__resCMReplace.Name = "__resCMReplace";
			this.@__resCMReplace.Size = new System.Drawing.Size(144, 22);
			this.@__resCMReplace.Text = "Replace {0}";
			// 
			// __resCMDelete
			// 
			this.@__resCMDelete.Name = "__resCMDelete";
			this.@__resCMDelete.Size = new System.Drawing.Size(144, 22);
			this.@__resCMDelete.Text = "Delete {0}";
			// 
			// __resCMCancel
			// 
			this.@__resCMCancel.Name = "__resCMCancel";
			this.@__resCMCancel.Size = new System.Drawing.Size(144, 22);
			this.@__resCMCancel.Text = "Cancel {0}";
			// 
			// __treeStateImages
			// 
			this.@__treeStateImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.@__treeStateImages.ImageSize = new System.Drawing.Size(16, 16);
			this.@__treeStateImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(629, 384);
			this.Controls.Add(this.@__t);
			this.Controls.Add(this.@__status);
			this.Controls.Add(this.@__split);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Anolis Resourcer";
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.ResumeLayout(false);
			this.@__status.ResumeLayout(false);
			this.@__status.PerformLayout();
			this.@__t.ResumeLayout(false);
			this.@__t.PerformLayout();
			this.@__resCM.ResumeLayout(false);
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
		private System.Windows.Forms.ContextMenuStrip __resCM;
		private System.Windows.Forms.ToolStripMenuItem __resCMExtract;
		private System.Windows.Forms.ToolStripMenuItem __resCMCast;
		private System.Windows.Forms.ToolStripMenuItem __resCMReplace;
		private System.Windows.Forms.ToolStripMenuItem __resCMDelete;
		private System.Windows.Forms.ToolStripSeparator __tSep1;
		private System.Windows.Forms.ToolStripSeparator __tSep2;
		private System.Windows.Forms.ToolStripStatusLabel __sSep1;
		private System.Windows.Forms.ToolStripStatusLabel __sSep2;
		private System.Windows.Forms.ToolStripSeparator __resCMSep;
		private System.Windows.Forms.ToolStripButton __tResUndo;
		private System.Windows.Forms.ToolStripMenuItem __resCMCancel;
		private System.Windows.Forms.ImageList __treeStateImages;
	}
}

