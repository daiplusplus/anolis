namespace Anolis.IconEditor {
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
			if(disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.ToolStripSeparator @__tSep1;
			this.@__t = new System.Windows.Forms.ToolStrip();
			this.@__tNew = new System.Windows.Forms.ToolStripButton();
			this.@__tOpen = new System.Windows.Forms.ToolStripButton();
			this.@__tSave = new System.Windows.Forms.ToolStripButton();
			this.@__tAdd = new System.Windows.Forms.ToolStripButton();
			this.@__tExport = new System.Windows.Forms.ToolStripButton();
			this.@__tReplace = new System.Windows.Forms.ToolStripButton();
			this.@__tDelete = new System.Windows.Forms.ToolStripButton();
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__status = new System.Windows.Forms.StatusStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.@__tOptions = new System.Windows.Forms.ToolStripButton();
			@__tSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.@__t.SuspendLayout();
			this.@__split.SuspendLayout();
			this.SuspendLayout();
			// 
			// __t
			// 
			this.@__t.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__t.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tNew,
            this.@__tOpen,
            this.@__tSave,
            @__tSep1,
            this.@__tAdd,
            this.@__tExport,
            this.@__tReplace,
            this.@__tDelete,
            this.toolStripSeparator1,
            this.@__tOptions});
			this.@__t.Location = new System.Drawing.Point(0, 0);
			this.@__t.Name = "__t";
			this.@__t.Size = new System.Drawing.Size(636, 36);
			this.@__t.TabIndex = 0;
			// 
			// __tNew
			// 
			this.@__tNew.Image = ((System.Drawing.Image)(resources.GetObject("__tNew.Image")));
			this.@__tNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tNew.Name = "__tNew";
			this.@__tNew.Size = new System.Drawing.Size(56, 33);
			this.@__tNew.Text = "New Icon";
			this.@__tNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tOpen
			// 
			this.@__tOpen.Image = ((System.Drawing.Image)(resources.GetObject("__tOpen.Image")));
			this.@__tOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tOpen.Name = "__tOpen";
			this.@__tOpen.Size = new System.Drawing.Size(37, 33);
			this.@__tOpen.Text = "Open";
			this.@__tOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tSave
			// 
			this.@__tSave.Image = ((System.Drawing.Image)(resources.GetObject("__tSave.Image")));
			this.@__tSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tSave.Name = "__tSave";
			this.@__tSave.Size = new System.Drawing.Size(35, 33);
			this.@__tSave.Text = "Save";
			this.@__tSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tSep1
			// 
			@__tSep1.Name = "__tSep1";
			@__tSep1.Size = new System.Drawing.Size(6, 36);
			// 
			// __tAdd
			// 
			this.@__tAdd.Image = ((System.Drawing.Image)(resources.GetObject("__tAdd.Image")));
			this.@__tAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tAdd.Name = "__tAdd";
			this.@__tAdd.Size = new System.Drawing.Size(79, 33);
			this.@__tAdd.Text = "Add Subimage";
			this.@__tAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tExport
			// 
			this.@__tExport.Image = ((System.Drawing.Image)(resources.GetObject("__tExport.Image")));
			this.@__tExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tExport.Name = "__tExport";
			this.@__tExport.Size = new System.Drawing.Size(92, 33);
			this.@__tExport.Text = "Export Subimage";
			this.@__tExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tReplace
			// 
			this.@__tReplace.Image = ((System.Drawing.Image)(resources.GetObject("__tReplace.Image")));
			this.@__tReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tReplace.Name = "__tReplace";
			this.@__tReplace.Size = new System.Drawing.Size(98, 33);
			this.@__tReplace.Text = "Replace Subimage";
			this.@__tReplace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __tDelete
			// 
			this.@__tDelete.Image = ((System.Drawing.Image)(resources.GetObject("__tDelete.Image")));
			this.@__tDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tDelete.Name = "__tDelete";
			this.@__tDelete.Size = new System.Drawing.Size(91, 33);
			this.@__tDelete.Text = "Delete Subimage";
			this.@__tDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// __split
			// 
			this.@__split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.@__split.Location = new System.Drawing.Point(0, 36);
			this.@__split.Name = "__split";
			this.@__split.Size = new System.Drawing.Size(636, 325);
			this.@__split.SplitterDistance = 130;
			this.@__split.TabIndex = 1;
			// 
			// __status
			// 
			this.@__status.Location = new System.Drawing.Point(0, 361);
			this.@__status.Name = "__status";
			this.@__status.Size = new System.Drawing.Size(636, 22);
			this.@__status.TabIndex = 0;
			this.@__status.Text = "statusStrip1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
			// 
			// __tOptions
			// 
			this.@__tOptions.Image = ((System.Drawing.Image)(resources.GetObject("__tOptions.Image")));
			this.@__tOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tOptions.Name = "__tOptions";
			this.@__tOptions.Size = new System.Drawing.Size(48, 33);
			this.@__tOptions.Text = "Options";
			this.@__tOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(636, 383);
			this.Controls.Add(this.@__split);
			this.Controls.Add(this.@__status);
			this.Controls.Add(this.@__t);
			this.Name = "MainForm";
			this.Text = "Anolis Icon Editor";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.@__t.ResumeLayout(false);
			this.@__t.PerformLayout();
			this.@__split.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip __t;
		private System.Windows.Forms.ToolStripButton __tNew;
		private System.Windows.Forms.ToolStripButton __tOpen;
		private System.Windows.Forms.ToolStripButton __tSave;
		private System.Windows.Forms.ToolStripButton __tAdd;
		private System.Windows.Forms.ToolStripButton __tExport;
		private System.Windows.Forms.ToolStripButton __tReplace;
		private System.Windows.Forms.ToolStripButton __tDelete;
		private System.Windows.Forms.SplitContainer __split;
		private System.Windows.Forms.StatusStrip __status;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton __tOptions;
	}
}

