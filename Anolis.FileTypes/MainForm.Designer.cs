namespace Anolis.FileTypes {
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
			this.@__typesList = new System.Windows.Forms.ListView();
			this.@__typesListCType = new Anolis.FileTypes.ColumnHeader2();
			this.@__typesListCName = new Anolis.FileTypes.ColumnHeader2();
			this.@__TypesListCExts = new Anolis.FileTypes.ColumnHeader2();
			this.@__tabs = new System.Windows.Forms.TabControl();
			this.@__tabTypes = new System.Windows.Forms.TabPage();
			this.@__typeEdit = new System.Windows.Forms.Button();
			this.@__typeAdd = new System.Windows.Forms.Button();
			this.@__tabExts = new System.Windows.Forms.TabPage();
			this.@__extAdd = new System.Windows.Forms.Button();
			this.@__extEdit = new System.Windows.Forms.Button();
			this.@__extGotoType = new System.Windows.Forms.Button();
			this.@__extsList = new System.Windows.Forms.ListView();
			this.@__extsListCExt = new Anolis.FileTypes.ColumnHeader2();
			this.@__extsListCProgId = new Anolis.FileTypes.ColumnHeader2();
			this.@__extsListCType = new Anolis.FileTypes.ColumnHeader2();
			this.@__reload = new System.Windows.Forms.Button();
			this.@__close = new System.Windows.Forms.Button();
			this.@__tabs.SuspendLayout();
			this.@__tabTypes.SuspendLayout();
			this.@__tabExts.SuspendLayout();
			this.SuspendLayout();
			// 
			// __typesList
			// 
			this.@__typesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__typesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.@__typesListCType,
            this.@__typesListCName,
            this.@__TypesListCExts});
			this.@__typesList.FullRowSelect = true;
			this.@__typesList.Location = new System.Drawing.Point(6, 6);
			this.@__typesList.Name = "__typesList";
			this.@__typesList.Size = new System.Drawing.Size(566, 273);
			this.@__typesList.TabIndex = 0;
			this.@__typesList.UseCompatibleStateImageBehavior = false;
			this.@__typesList.View = System.Windows.Forms.View.Details;
			// 
			// __typesListCType
			// 
			this.@__typesListCType.Text = "ProgID";
			this.@__typesListCType.Width = 120;
			// 
			// __typesListCName
			// 
			this.@__typesListCName.Text = "Friendly Name";
			this.@__typesListCName.Width = 250;
			// 
			// __TypesListCExts
			// 
			this.@__TypesListCExts.Text = "Extensions";
			this.@__TypesListCExts.Width = 150;
			// 
			// __tabs
			// 
			this.@__tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__tabs.Controls.Add(this.@__tabTypes);
			this.@__tabs.Controls.Add(this.@__tabExts);
			this.@__tabs.Location = new System.Drawing.Point(6, 7);
			this.@__tabs.Margin = new System.Windows.Forms.Padding(0);
			this.@__tabs.Name = "__tabs";
			this.@__tabs.SelectedIndex = 0;
			this.@__tabs.Size = new System.Drawing.Size(586, 340);
			this.@__tabs.TabIndex = 0;
			// 
			// __tabTypes
			// 
			this.@__tabTypes.Controls.Add(this.@__typeEdit);
			this.@__tabTypes.Controls.Add(this.@__typeAdd);
			this.@__tabTypes.Controls.Add(this.@__typesList);
			this.@__tabTypes.Location = new System.Drawing.Point(4, 22);
			this.@__tabTypes.Name = "__tabTypes";
			this.@__tabTypes.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabTypes.Size = new System.Drawing.Size(578, 314);
			this.@__tabTypes.TabIndex = 0;
			this.@__tabTypes.Text = "Types";
			this.@__tabTypes.UseVisualStyleBackColor = true;
			// 
			// __typeEdit
			// 
			this.@__typeEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__typeEdit.Enabled = false;
			this.@__typeEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__typeEdit.Location = new System.Drawing.Point(497, 285);
			this.@__typeEdit.Name = "__typeEdit";
			this.@__typeEdit.Size = new System.Drawing.Size(75, 23);
			this.@__typeEdit.TabIndex = 2;
			this.@__typeEdit.Text = "Edit Type";
			this.@__typeEdit.UseVisualStyleBackColor = true;
			// 
			// __typeAdd
			// 
			this.@__typeAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__typeAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__typeAdd.Location = new System.Drawing.Point(6, 285);
			this.@__typeAdd.Name = "__typeAdd";
			this.@__typeAdd.Size = new System.Drawing.Size(75, 23);
			this.@__typeAdd.TabIndex = 1;
			this.@__typeAdd.Text = "Add Type";
			this.@__typeAdd.UseVisualStyleBackColor = true;
			// 
			// __tabExts
			// 
			this.@__tabExts.Controls.Add(this.@__extAdd);
			this.@__tabExts.Controls.Add(this.@__extEdit);
			this.@__tabExts.Controls.Add(this.@__extGotoType);
			this.@__tabExts.Controls.Add(this.@__extsList);
			this.@__tabExts.Location = new System.Drawing.Point(4, 22);
			this.@__tabExts.Name = "__tabExts";
			this.@__tabExts.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabExts.Size = new System.Drawing.Size(578, 314);
			this.@__tabExts.TabIndex = 1;
			this.@__tabExts.Text = "Extensions";
			this.@__tabExts.UseVisualStyleBackColor = true;
			// 
			// __extAdd
			// 
			this.@__extAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__extAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__extAdd.Location = new System.Drawing.Point(6, 285);
			this.@__extAdd.Name = "__extAdd";
			this.@__extAdd.Size = new System.Drawing.Size(101, 23);
			this.@__extAdd.TabIndex = 1;
			this.@__extAdd.Text = "Add Extension";
			this.@__extAdd.UseVisualStyleBackColor = true;
			// 
			// __extEdit
			// 
			this.@__extEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__extEdit.Enabled = false;
			this.@__extEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__extEdit.Location = new System.Drawing.Point(378, 285);
			this.@__extEdit.Name = "__extEdit";
			this.@__extEdit.Size = new System.Drawing.Size(103, 23);
			this.@__extEdit.TabIndex = 2;
			this.@__extEdit.Text = "Edit Extension";
			this.@__extEdit.UseVisualStyleBackColor = true;
			// 
			// __extGotoType
			// 
			this.@__extGotoType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__extGotoType.Enabled = false;
			this.@__extGotoType.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__extGotoType.Location = new System.Drawing.Point(487, 285);
			this.@__extGotoType.Name = "__extGotoType";
			this.@__extGotoType.Size = new System.Drawing.Size(85, 23);
			this.@__extGotoType.TabIndex = 3;
			this.@__extGotoType.Text = "Go to Type";
			this.@__extGotoType.UseVisualStyleBackColor = true;
			// 
			// __extsList
			// 
			this.@__extsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__extsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.@__extsListCExt,
            this.@__extsListCProgId,
            this.@__extsListCType});
			this.@__extsList.FullRowSelect = true;
			this.@__extsList.Location = new System.Drawing.Point(6, 6);
			this.@__extsList.Name = "__extsList";
			this.@__extsList.Size = new System.Drawing.Size(566, 273);
			this.@__extsList.TabIndex = 0;
			this.@__extsList.UseCompatibleStateImageBehavior = false;
			this.@__extsList.View = System.Windows.Forms.View.Details;
			// 
			// __extsListCExt
			// 
			this.@__extsListCExt.Text = "Extension";
			this.@__extsListCExt.Width = 100;
			// 
			// __extsListCProgId
			// 
			this.@__extsListCProgId.Text = "ProgId";
			this.@__extsListCProgId.Width = 150;
			// 
			// __extsListCType
			// 
			this.@__extsListCType.Text = "ProgId Friendly Name";
			this.@__extsListCType.Width = 300;
			// 
			// __reload
			// 
			this.@__reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__reload.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__reload.Location = new System.Drawing.Point(6, 350);
			this.@__reload.Name = "__reload";
			this.@__reload.Size = new System.Drawing.Size(75, 23);
			this.@__reload.TabIndex = 1;
			this.@__reload.Text = "Reload";
			this.@__reload.UseVisualStyleBackColor = true;
			// 
			// __close
			// 
			this.@__close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__close.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__close.Location = new System.Drawing.Point(513, 350);
			this.@__close.Name = "__close";
			this.@__close.Size = new System.Drawing.Size(75, 23);
			this.@__close.TabIndex = 2;
			this.@__close.Text = "Close";
			this.@__close.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(596, 378);
			this.Controls.Add(this.@__close);
			this.Controls.Add(this.@__reload);
			this.Controls.Add(this.@__tabs);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "File Type Editor";
			this.@__tabs.ResumeLayout(false);
			this.@__tabTypes.ResumeLayout(false);
			this.@__tabExts.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView __typesList;
		private System.Windows.Forms.TabControl __tabs;
		private System.Windows.Forms.TabPage __tabTypes;
		private System.Windows.Forms.TabPage __tabExts;
		private System.Windows.Forms.ListView __extsList;
		private System.Windows.Forms.Button __reload;
		private System.Windows.Forms.ColumnHeader __extsListCExt;
		private System.Windows.Forms.ColumnHeader __extsListCType;
		private System.Windows.Forms.ColumnHeader __typesListCType;
		private System.Windows.Forms.ColumnHeader __typesListCName;
		private System.Windows.Forms.ColumnHeader __TypesListCExts;
		private System.Windows.Forms.ColumnHeader __extsListCProgId;
		private System.Windows.Forms.Button __extAdd;
		private System.Windows.Forms.Button __extEdit;
		private System.Windows.Forms.Button __extGotoType;
		private System.Windows.Forms.Button __close;
		private System.Windows.Forms.Button __typeEdit;
		private System.Windows.Forms.Button __typeAdd;
	}
}

