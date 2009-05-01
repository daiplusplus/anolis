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
			this.@__ttRemove = new System.Windows.Forms.ToolStripButton();
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__properties = new System.Windows.Forms.PropertyGrid();
			this.@__tIconEditor = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			@__tSep1 = new System.Windows.Forms.ToolStripSeparator();
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
			// __tools
			// 
			this.@__tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__tNew,
            this.@__tOpen,
            this.@__tSave,
            @__tSep1,
            this.@__tValidate,
            this.toolStripSeparator1,
            this.@__tResourcer,
            this.@__tIconEditor});
			this.@__tools.Location = new System.Drawing.Point(0, 0);
			this.@__tools.Name = "__tools";
			this.@__tools.Size = new System.Drawing.Size(596, 25);
			this.@__tools.TabIndex = 0;
			this.@__tools.Text = "toolStrip1";
			// 
			// __tNew
			// 
			this.@__tNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tNew.Image = ((System.Drawing.Image)(resources.GetObject("__tNew.Image")));
			this.@__tNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tNew.Name = "__tNew";
			this.@__tNew.Size = new System.Drawing.Size(23, 22);
			this.@__tNew.Text = "&New";
			// 
			// __tOpen
			// 
			this.@__tOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tOpen.Image = ((System.Drawing.Image)(resources.GetObject("__tOpen.Image")));
			this.@__tOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tOpen.Name = "__tOpen";
			this.@__tOpen.Size = new System.Drawing.Size(23, 22);
			this.@__tOpen.Text = "&Open";
			// 
			// __tSave
			// 
			this.@__tSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tSave.Image = ((System.Drawing.Image)(resources.GetObject("__tSave.Image")));
			this.@__tSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tSave.Name = "__tSave";
			this.@__tSave.Size = new System.Drawing.Size(23, 22);
			this.@__tSave.Text = "&Save";
			// 
			// __tValidate
			// 
			this.@__tValidate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tValidate.Image = ((System.Drawing.Image)(resources.GetObject("__tValidate.Image")));
			this.@__tValidate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tValidate.Name = "__tValidate";
			this.@__tValidate.Size = new System.Drawing.Size(23, 22);
			this.@__tValidate.Text = "Validate XML";
			// 
			// __tResourcer
			// 
			this.@__tResourcer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tResourcer.Image = ((System.Drawing.Image)(resources.GetObject("__tResourcer.Image")));
			this.@__tResourcer.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tResourcer.Name = "__tResourcer";
			this.@__tResourcer.Size = new System.Drawing.Size(23, 22);
			this.@__tResourcer.Text = "Launch Resourcer";
			// 
			// __tree
			// 
			this.@__tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__tree.Location = new System.Drawing.Point(0, 25);
			this.@__tree.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.@__tree.Name = "__tree";
			this.@__tree.Size = new System.Drawing.Size(223, 259);
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
            this.@__ttRemove});
			this.@__treeTools.Location = new System.Drawing.Point(0, 0);
			this.@__treeTools.Name = "__treeTools";
			this.@__treeTools.Size = new System.Drawing.Size(223, 25);
			this.@__treeTools.TabIndex = 5;
			this.@__treeTools.Text = "toolStrip1";
			// 
			// __ttAddGroup
			// 
			this.@__ttAddGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("__ttAddGroup.Image")));
			this.@__ttAddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__ttAddGroup.Name = "__ttAddGroup";
			this.@__ttAddGroup.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddGroup.Text = "Add Group";
			// 
			// __ttAddPatch
			// 
			this.@__ttAddPatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddPatch.Image = ((System.Drawing.Image)(resources.GetObject("__ttAddPatch.Image")));
			this.@__ttAddPatch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__ttAddPatch.Name = "__ttAddPatch";
			this.@__ttAddPatch.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddPatch.Text = "Add Patch";
			// 
			// __ttAddFile
			// 
			this.@__ttAddFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddFile.Image = ((System.Drawing.Image)(resources.GetObject("__ttAddFile.Image")));
			this.@__ttAddFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__ttAddFile.Name = "__ttAddFile";
			this.@__ttAddFile.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddFile.Text = "Add File";
			// 
			// __ttAddFileAssoc
			// 
			this.@__ttAddFileAssoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddFileAssoc.Image = ((System.Drawing.Image)(resources.GetObject("__ttAddFileAssoc.Image")));
			this.@__ttAddFileAssoc.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__ttAddFileAssoc.Name = "__ttAddFileAssoc";
			this.@__ttAddFileAssoc.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddFileAssoc.Text = "Add File Association";
			// 
			// __ttAddExtra
			// 
			this.@__ttAddExtra.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttAddExtra.Image = ((System.Drawing.Image)(resources.GetObject("__ttAddExtra.Image")));
			this.@__ttAddExtra.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__ttAddExtra.Name = "__ttAddExtra";
			this.@__ttAddExtra.Size = new System.Drawing.Size(23, 22);
			this.@__ttAddExtra.Text = "Add Extra";
			// 
			// __ttRemove
			// 
			this.@__ttRemove.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.@__ttRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__ttRemove.Image = ((System.Drawing.Image)(resources.GetObject("__ttRemove.Image")));
			this.@__ttRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
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
			this.@__split.Panel1.Controls.Add(this.@__tree);
			this.@__split.Panel1.Controls.Add(this.@__treeTools);
			// 
			// __split.Panel2
			// 
			this.@__split.Panel2.Controls.Add(this.@__properties);
			this.@__split.Size = new System.Drawing.Size(596, 284);
			this.@__split.SplitterDistance = 223;
			this.@__split.TabIndex = 6;
			// 
			// __properties
			// 
			this.@__properties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__properties.Location = new System.Drawing.Point(0, 0);
			this.@__properties.Name = "__properties";
			this.@__properties.Size = new System.Drawing.Size(369, 284);
			this.@__properties.TabIndex = 0;
			// 
			// __tIconEditor
			// 
			this.@__tIconEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__tIconEditor.Image = ((System.Drawing.Image)(resources.GetObject("__tIconEditor.Image")));
			this.@__tIconEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__tIconEditor.Name = "__tIconEditor";
			this.@__tIconEditor.Size = new System.Drawing.Size(23, 22);
			this.@__tIconEditor.Text = "Launch Icon Editor";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// DefinitionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(596, 309);
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
		private System.Windows.Forms.ToolStripButton __tIconEditor;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}