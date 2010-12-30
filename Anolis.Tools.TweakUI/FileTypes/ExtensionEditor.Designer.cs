namespace Anolis.Tools.TweakUI.FileTypes {
	partial class ExtensionEditor {
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
			this.@__persistLbl = new System.Windows.Forms.Label();
			this.@__persistentHandler = new System.Windows.Forms.ComboBox();
			this.@__perceived = new System.Windows.Forms.TextBox();
			this.@__perceivedLbl = new System.Windows.Forms.Label();
			this.@__extensionLbl = new System.Windows.Forms.Label();
			this.@__typeCreate = new System.Windows.Forms.Button();
			this.@__typeLbl = new System.Windows.Forms.Label();
			this.@__contentType = new System.Windows.Forms.TextBox();
			this.@__contentTypeLbl = new System.Windows.Forms.Label();
			this.@__extension = new System.Windows.Forms.TextBox();
			this.@__save = new System.Windows.Forms.Button();
			this.@__reload = new System.Windows.Forms.Button();
			this.@__type = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// __persistLbl
			// 
			this.@__persistLbl.AutoSize = true;
			this.@__persistLbl.Location = new System.Drawing.Point(7, 42);
			this.@__persistLbl.Name = "__persistLbl";
			this.@__persistLbl.Size = new System.Drawing.Size(93, 13);
			this.@__persistLbl.TabIndex = 2;
			this.@__persistLbl.Text = "Persistent Handler";
			// 
			// __persistentHandler
			// 
			this.@__persistentHandler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__persistentHandler.FormattingEnabled = true;
			this.@__persistentHandler.Items.AddRange(new object[] {
            "None",
            "Text",
            "Null",
            "Html",
            "Other..."});
			this.@__persistentHandler.Location = new System.Drawing.Point(106, 39);
			this.@__persistentHandler.Name = "__persistentHandler";
			this.@__persistentHandler.Size = new System.Drawing.Size(111, 21);
			this.@__persistentHandler.TabIndex = 1;
			// 
			// __perceived
			// 
			this.@__perceived.Location = new System.Drawing.Point(106, 66);
			this.@__perceived.Name = "__perceived";
			this.@__perceived.Size = new System.Drawing.Size(111, 20);
			this.@__perceived.TabIndex = 2;
			// 
			// __perceivedLbl
			// 
			this.@__perceivedLbl.AutoSize = true;
			this.@__perceivedLbl.Location = new System.Drawing.Point(18, 69);
			this.@__perceivedLbl.Name = "__perceivedLbl";
			this.@__perceivedLbl.Size = new System.Drawing.Size(82, 13);
			this.@__perceivedLbl.TabIndex = 6;
			this.@__perceivedLbl.Text = "Perceived Type";
			// 
			// __extensionLbl
			// 
			this.@__extensionLbl.AutoSize = true;
			this.@__extensionLbl.Location = new System.Drawing.Point(47, 16);
			this.@__extensionLbl.Name = "__extensionLbl";
			this.@__extensionLbl.Size = new System.Drawing.Size(53, 13);
			this.@__extensionLbl.TabIndex = 8;
			this.@__extensionLbl.Text = "Extension";
			// 
			// __typeCreate
			// 
			this.@__typeCreate.Location = new System.Drawing.Point(560, 11);
			this.@__typeCreate.Name = "__typeCreate";
			this.@__typeCreate.Size = new System.Drawing.Size(61, 23);
			this.@__typeCreate.TabIndex = 4;
			this.@__typeCreate.Text = "Create";
			this.@__typeCreate.UseVisualStyleBackColor = true;
			// 
			// __typeLbl
			// 
			this.@__typeLbl.AutoSize = true;
			this.@__typeLbl.Location = new System.Drawing.Point(269, 16);
			this.@__typeLbl.Name = "__typeLbl";
			this.@__typeLbl.Size = new System.Drawing.Size(31, 13);
			this.@__typeLbl.TabIndex = 10;
			this.@__typeLbl.Text = "Type";
			// 
			// __contentType
			// 
			this.@__contentType.Location = new System.Drawing.Point(306, 39);
			this.@__contentType.Name = "__contentType";
			this.@__contentType.Size = new System.Drawing.Size(175, 20);
			this.@__contentType.TabIndex = 5;
			// 
			// __contentTypeLbl
			// 
			this.@__contentTypeLbl.AutoSize = true;
			this.@__contentTypeLbl.Location = new System.Drawing.Point(238, 42);
			this.@__contentTypeLbl.Name = "__contentTypeLbl";
			this.@__contentTypeLbl.Size = new System.Drawing.Size(62, 13);
			this.@__contentTypeLbl.TabIndex = 12;
			this.@__contentTypeLbl.Text = "MIME Type";
			// 
			// __extension
			// 
			this.@__extension.Location = new System.Drawing.Point(106, 13);
			this.@__extension.Name = "__extension";
			this.@__extension.Size = new System.Drawing.Size(111, 20);
			this.@__extension.TabIndex = 0;
			// 
			// __save
			// 
			this.@__save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__save.Location = new System.Drawing.Point(493, 64);
			this.@__save.Name = "__save";
			this.@__save.Size = new System.Drawing.Size(61, 23);
			this.@__save.TabIndex = 6;
			this.@__save.Text = "Save";
			this.@__save.UseVisualStyleBackColor = true;
			// 
			// __reload
			// 
			this.@__reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__reload.Location = new System.Drawing.Point(560, 64);
			this.@__reload.Name = "__reload";
			this.@__reload.Size = new System.Drawing.Size(61, 23);
			this.@__reload.TabIndex = 7;
			this.@__reload.Text = "Reset";
			this.@__reload.UseVisualStyleBackColor = true;
			// 
			// __type
			// 
			this.@__type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.@__type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.@__type.Location = new System.Drawing.Point(306, 13);
			this.@__type.Name = "__type";
			this.@__type.Size = new System.Drawing.Size(248, 20);
			this.@__type.TabIndex = 13;
			// 
			// ExtensionEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__type);
			this.Controls.Add(this.@__reload);
			this.Controls.Add(this.@__save);
			this.Controls.Add(this.@__extension);
			this.Controls.Add(this.@__contentType);
			this.Controls.Add(this.@__contentTypeLbl);
			this.Controls.Add(this.@__typeLbl);
			this.Controls.Add(this.@__typeCreate);
			this.Controls.Add(this.@__extensionLbl);
			this.Controls.Add(this.@__perceived);
			this.Controls.Add(this.@__perceivedLbl);
			this.Controls.Add(this.@__persistentHandler);
			this.Controls.Add(this.@__persistLbl);
			this.Name = "ExtensionEditor";
			this.Size = new System.Drawing.Size(633, 102);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label __persistLbl;
		private System.Windows.Forms.ComboBox __persistentHandler;
		private System.Windows.Forms.TextBox __perceived;
		private System.Windows.Forms.Label __perceivedLbl;
		private System.Windows.Forms.Label __extensionLbl;
		private System.Windows.Forms.Button __typeCreate;
		private System.Windows.Forms.Label __typeLbl;
		private System.Windows.Forms.TextBox __contentType;
		private System.Windows.Forms.Label __contentTypeLbl;
		private System.Windows.Forms.TextBox __extension;
		private System.Windows.Forms.Button __save;
		private System.Windows.Forms.Button __reload;
		private System.Windows.Forms.TextBox __type;
	}
}
