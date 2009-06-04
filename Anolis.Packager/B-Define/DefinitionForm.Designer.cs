namespace Anolis.Packager {
	partial class DefinitionForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefinitionForm));
			this.@__tools = new System.Windows.Forms.ToolStrip();
			this.@__tNew = new System.Windows.Forms.ToolStripButton();
			this.@__tOpen = new System.Windows.Forms.ToolStripButton();
			this.@__tSave = new System.Windows.Forms.ToolStripButton();
			this.@__tValidate = new System.Windows.Forms.ToolStripButton();
			this.@__tResourcer = new System.Windows.Forms.ToolStripButton();
			this.@__tree = new System.Windows.Forms.TreeView();
			this.@__treeTools = new System.Windows.Forms.ToolStrip();
			this.@__ttAddGroup = new System.Windows.Forms.ToolStripButton();
			this.@__ttAddPatch = new System.Windows.Forms.ToolStripButton();
			this.@__ttAddFile = new System.Windows.Forms.ToolStripButton();
			this.@__ttAddFileAssoc = new System.Windows.Forms.ToolStripButton();
			this.@__ttAddExtra = new System.Windows.Forms.ToolStripButton();
			this.@__ttSep = new System.Windows.Forms.ToolStripSeparator();
			this.@__ttRemove = new System.Windows.Forms.ToolStripButton();
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__properties = new System.Windows.Forms.PropertyGrid();
			@__tSep1 = new System.Windows.Forms.ToolStripSeparator();
			@__tSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.@__tools.SuspendLayout();
			this.@__treeTools.SuspendLayout();
			this.@__split.Panel1.SuspendLayout();
			this.@__split.Panel2.SuspendLayout();
			this.@__split.SuspendLayout();
			this.SuspendLayout();
			// 
			// __tSep1
			// 
			@__tSep1.Name = "__tSep1";
			@__tSep1.Size = new System.Drawing.Size(6, 25);
			// 
			// __tSep2
			// 
			@__tSep2.Name = "__tSep2";
			@__tSep2.Size = new System.Drawing.Size(6, 25);
			// 
			// __tools
			// 
			this.@__tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tNew,
            this.@__tOpen,
            this.@__tSave,
            @__tSep1,
            this.@__tValidate,
            @__tSep2,
            this.@__tResourcer});
			this.@__tools.Location = new System.Drawing.Point(0, 0);
			this.@__tools.Name = "__tools";
			this.@__tools.Size = new System.Drawing.Size(580, 25);
			this.@__tools.TabIndex = 0;
			this.@__tools.Text = "toolStrip1";
			// 
			// __tNew
			// 
			this.@__tNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tNew.Image = global::Anolis.Packager.AppResources.Define_TNew;
			this.@__tNew.Name = "__tNew";
			this.@__tNew.Size = new System.Drawing.Size(23, 22);
			this.@__tNew.Text = "&New";
			// 
			// __tOpen
			// 
			this.@__tOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tOpen.Image = global::Anolis.Packager.AppResources.Define_TOpen;
			this.@__tOpen.Name = "__tOpen";
			this.@__tOpen.Size = new System.Drawing.Size(23, 22);
			this.@__tOpen.Text = "&Open";
			// 
			// __tSave
			// 
			this.@__tSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tSave.Image = global::Anolis.Packager.AppResources.Define_TSave;
			this.@__tSave.Name = "__tSave";
			this.@__tSave.Size = new System.Drawing.Size(23, 22);
			this.@__tSave.Text = "&Save";
			// 
			// __tValidate
			// 
			this.@__tValidate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tValidate.Image = ((System.Drawing.Image)(resources.GetObject("__tValidate.Image")));
			this.@__tValidate.Name = "__tValidate";
			this.@__tValidate.Size = new System.Drawing.Size(23, 22);
			this.@__tValidate.Text = "Validate XML";
			// 
			// __tResourcer
			// 
			this.@__tResourcer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tResourcer.Image = global::Anolis.Packager.AppResources.Define_TResourcer;
			this.@__tResourcer.Name = "__tResourcer";
			this.@__tResourcer.Size = new System.Drawing.Size(23, 22);
			this.@__tResourcer.Text = "Launch Resourcer";
			// 
			// __tree
			// 
			this.@__tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__tree.Location = new System.Drawing.Point(0, 0);
			this.@__tree.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.@__tree.Name = "__tree";
			this.@__tree.Size = new System.Drawing.Size(217, 380);
			this.@__tree.TabIndex = 1;
			// 
			// __treeTools
			// 
			this.@__treeTools.AutoSize = false;
			this.@__treeTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__treeTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__ttAddGroup,
            this.@__ttAddPatch,
            this.@__ttAddFile,
            this.@__ttAddFileAssoc,
            this.@__ttAddExtra,
            this.@__ttSep,
            this.@__ttRemove});
			this.@__treeTools.Location = new System.Drawing.Point(0, 0);
			this.@__treeTools.Name = "__treeTools";
			this.@__treeTools.Size = new System.Drawing.Size(217, 25);
			this.@__treeTools.TabIndex = 5;
			this.@__treeTools.Text = "toolStrip1";
			// 
			// __ttAddGroup
			// 
			this.@__ttAddGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddGroup.Image = global::Anolis.Packager.AppResources.Define_AddGroup;
			this.@__ttAddGroup.Name = "__ttAddGroup";
			this.@__ttAddGroup.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddGroup.Text = "Add Group";
			// 
			// __ttAddPatch
			// 
			this.@__ttAddPatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddPatch.Image = global::Anolis.Packager.AppResources.Define_AddPatch;
			this.@__ttAddPatch.Name = "__ttAddPatch";
			this.@__ttAddPatch.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddPatch.Text = "Add Patch";
			// 
			// __ttAddFile
			// 
			this.@__ttAddFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddFile.Image = global::Anolis.Packager.AppResources.Define_AddFile;
			this.@__ttAddFile.Name = "__ttAddFile";
			this.@__ttAddFile.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddFile.Text = "Add File";
			// 
			// __ttAddFileAssoc
			// 
			this.@__ttAddFileAssoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddFileAssoc.Image = global::Anolis.Packager.AppResources.Define_AddFileAssoc;
			this.@__ttAddFileAssoc.Name = "__ttAddFileAssoc";
			this.@__ttAddFileAssoc.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddFileAssoc.Text = "Add File Association";
			// 
			// __ttAddExtra
			// 
			this.@__ttAddExtra.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddExtra.Image = global::Anolis.Packager.AppResources.Define_AddExtra;
			this.@__ttAddExtra.Name = "__ttAddExtra";
			this.@__ttAddExtra.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddExtra.Text = "Add Extra";
			// 
			// __ttSep
			// 
			this.@__ttSep.Name = "__ttSep";
			this.@__ttSep.Size = new System.Drawing.Size(6, 25);
			// 
			// __ttRemove
			// 
			this.@__ttRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttRemove.Image = global::Anolis.Packager.AppResources.Define_AddRemove;
			this.@__ttRemove.Name = "__ttRemove";
			this.@__ttRemove.Size = new System.Drawing.Size(23, 22);
			this.@__ttRemove.Text = "Remove";
			// 
			// __split
			// 
			this.@__split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__split.Location = new System.Drawing.Point(0, 25);
			this.@__split.Name = "__split";
			// 
			// __split.Panel1
			// 
			this.@__split.Panel1.Controls.Add(this.@__treeTools);
			this.@__split.Panel1.Controls.Add(this.@__tree);
			// 
			// __split.Panel2
			// 
			this.@__split.Panel2.Controls.Add(this.@__properties);
			this.@__split.Size = new System.Drawing.Size(580, 380);
			this.@__split.SplitterDistance = 217;
			this.@__split.TabIndex = 6;
			// 
			// __properties
			// 
			this.@__properties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__properties.Location = new System.Drawing.Point(0, 0);
			this.@__properties.Name = "__properties";
			this.@__properties.Size = new System.Drawing.Size(359, 380);
			this.@__properties.TabIndex = 0;
			// 
			// DefinitionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(580, 405);
			this.Controls.Add(this.@__split);
			this.Controls.Add(this.@__tools);
			this.Name = "DefinitionForm";
			this.Text = "Package Definition";
			this.@__tools.ResumeLayout(false);
			this.@__tools.PerformLayout();
			this.@__treeTools.ResumeLayout(false);
			this.@__treeTools.PerformLayout();
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.Panel2.ResumeLayout(false);
			this.@__split.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip __tools;
		private System.Windows.Forms.ToolStripButton __tNew;
		private System.Windows.Forms.ToolStripButton __tOpen;
		private System.Windows.Forms.ToolStripButton __tSave;
		private System.Windows.Forms.TreeView __tree;
		private System.Windows.Forms.ToolStrip __treeTools;
		private System.Windows.Forms.ToolStripButton __ttAddGroup;
		private System.Windows.Forms.ToolStripButton __ttAddPatch;
		private System.Windows.Forms.ToolStripButton __ttAddFile;
		private System.Windows.Forms.ToolStripButton __ttRemove;
		private System.Windows.Forms.ToolStripButton __tValidate;
		private System.Windows.Forms.ToolStripButton __tResourcer;
		private System.Windows.Forms.SplitContainer __split;
		private System.Windows.Forms.ToolStripButton __ttAddFileAssoc;
		private System.Windows.Forms.ToolStripButton __ttAddExtra;
		private System.Windows.Forms.PropertyGrid __properties;
		private System.Windows.Forms.ToolStripSeparator __ttSep;
	}
}