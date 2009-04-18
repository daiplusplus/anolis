namespace Anolis.Resourcer {
	partial class OptionsForm {
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
			this.@__tabs = new System.Windows.Forms.TabControl();
			this.@__tSettings = new System.Windows.Forms.TabPage();
			this.@__sUiGrp = new System.Windows.Forms.GroupBox();
			this.@__sAssocWarnLbl = new System.Windows.Forms.Label();
			this.@__sAssocLbl = new System.Windows.Forms.Label();
			this.@__sAssoc = new System.Windows.Forms.CheckBox();
			this.@__sUIButtonsLargeLbl = new System.Windows.Forms.Label();
			this.@__sUIButtonsLarge = new System.Windows.Forms.CheckBox();
			this.@__sMoreGrp = new System.Windows.Forms.GroupBox();
			this.@__sLibDel = new System.Windows.Forms.Button();
			this.@__sLibAdd = new System.Windows.Forms.Button();
			this.@__sLib = new System.Windows.Forms.ListBox();
			this.@__sLibLbl = new System.Windows.Forms.Label();
			this.@__tAbout = new System.Windows.Forms.TabPage();
			this.@__version = new System.Windows.Forms.Label();
			this.@__versionLbl = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.@__makesUseOf = new System.Windows.Forms.Label();
			this.@__linksLbl = new System.Windows.Forms.Label();
			this.@__aboutLinkAnolis = new System.Windows.Forms.LinkLabel();
			this.@__aboutBlurb = new System.Windows.Forms.Label();
			this.@__aboutAnolis = new System.Windows.Forms.Label();
			this.@__tLegal = new System.Windows.Forms.TabPage();
			this.@__legalToggle = new System.Windows.Forms.Button();
			this.@__legalText = new System.Windows.Forms.TextBox();
			this.@__cancel = new System.Windows.Forms.Button();
			this.@__ok = new System.Windows.Forms.Button();
			this.@__update = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.@__tabs.SuspendLayout();
			this.@__tSettings.SuspendLayout();
			this.@__sUiGrp.SuspendLayout();
			this.@__sMoreGrp.SuspendLayout();
			this.@__tAbout.SuspendLayout();
			this.@__tLegal.SuspendLayout();
			this.SuspendLayout();
			// 
			// __tabs
			// 
			this.@__tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__tabs.Controls.Add(this.@__tSettings);
			this.@__tabs.Controls.Add(this.@__tAbout);
			this.@__tabs.Controls.Add(this.@__tLegal);
			this.@__tabs.Location = new System.Drawing.Point(6, 7);
			this.@__tabs.Margin = new System.Windows.Forms.Padding(0);
			this.@__tabs.Name = "__tabs";
			this.@__tabs.SelectedIndex = 0;
			this.@__tabs.Size = new System.Drawing.Size(363, 370);
			this.@__tabs.TabIndex = 0;
			// 
			// __tSettings
			// 
			this.@__tSettings.Controls.Add(this.@__sUiGrp);
			this.@__tSettings.Controls.Add(this.@__sMoreGrp);
			this.@__tSettings.Location = new System.Drawing.Point(4, 22);
			this.@__tSettings.Name = "__tSettings";
			this.@__tSettings.Padding = new System.Windows.Forms.Padding(3);
			this.@__tSettings.Size = new System.Drawing.Size(355, 344);
			this.@__tSettings.TabIndex = 0;
			this.@__tSettings.Text = "Settings";
			this.@__tSettings.UseVisualStyleBackColor = true;
			// 
			// __sUiGrp
			// 
			this.@__sUiGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__sUiGrp.Controls.Add(this.@__sAssocWarnLbl);
			this.@__sUiGrp.Controls.Add(this.@__sAssocLbl);
			this.@__sUiGrp.Controls.Add(this.@__sAssoc);
			this.@__sUiGrp.Controls.Add(this.@__sUIButtonsLargeLbl);
			this.@__sUiGrp.Controls.Add(this.@__sUIButtonsLarge);
			this.@__sUiGrp.Location = new System.Drawing.Point(6, 6);
			this.@__sUiGrp.Name = "__sUiGrp";
			this.@__sUiGrp.Size = new System.Drawing.Size(340, 127);
			this.@__sUiGrp.TabIndex = 1;
			this.@__sUiGrp.TabStop = false;
			this.@__sUiGrp.Text = "Resourcer";
			// 
			// __sAssocWarnLbl
			// 
			this.@__sAssocWarnLbl.AutoSize = true;
			this.@__sAssocWarnLbl.Location = new System.Drawing.Point(15, 101);
			this.@__sAssocWarnLbl.Name = "__sAssocWarnLbl";
			this.@__sAssocWarnLbl.Size = new System.Drawing.Size(218, 13);
			this.@__sAssocWarnLbl.TabIndex = 3;
			this.@__sAssocWarnLbl.Text = "This feature requires Administrative privileges";
			// 
			// __sAssocLbl
			// 
			this.@__sAssocLbl.AutoSize = true;
			this.@__sAssocLbl.Location = new System.Drawing.Point(15, 80);
			this.@__sAssocLbl.Name = "__sAssocLbl";
			this.@__sAssocLbl.Size = new System.Drawing.Size(299, 13);
			this.@__sAssocLbl.TabIndex = 1;
			this.@__sAssocLbl.Text = "Resourcer will be an option on the \"Open With\" context menu";
			// 
			// __sAssoc
			// 
			this.@__sAssoc.AutoSize = true;
			this.@__sAssoc.Location = new System.Drawing.Point(6, 60);
			this.@__sAssoc.Name = "__sAssoc";
			this.@__sAssoc.Size = new System.Drawing.Size(232, 17);
			this.@__sAssoc.TabIndex = 2;
			this.@__sAssoc.Text = "Associate with exe, dll, cpl, ocx and scr files";
			this.@__sAssoc.UseVisualStyleBackColor = true;
			// 
			// __sUIButtonsLargeLbl
			// 
			this.@__sUIButtonsLargeLbl.AutoSize = true;
			this.@__sUIButtonsLargeLbl.Location = new System.Drawing.Point(15, 40);
			this.@__sUIButtonsLargeLbl.Name = "__sUIButtonsLargeLbl";
			this.@__sUIButtonsLargeLbl.Size = new System.Drawing.Size(207, 13);
			this.@__sUIButtonsLargeLbl.TabIndex = 2;
			this.@__sUIButtonsLargeLbl.Text = "Use 48x48 rather than 24x24 toolbar icons";
			// 
			// __sUIButtonsLarge
			// 
			this.@__sUIButtonsLarge.AutoSize = true;
			this.@__sUIButtonsLarge.Checked = true;
			this.@__sUIButtonsLarge.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__sUIButtonsLarge.Location = new System.Drawing.Point(6, 20);
			this.@__sUIButtonsLarge.Name = "__sUIButtonsLarge";
			this.@__sUIButtonsLarge.Size = new System.Drawing.Size(121, 17);
			this.@__sUIButtonsLarge.TabIndex = 1;
			this.@__sUIButtonsLarge.Text = "Large Toolbar Icons";
			this.@__sUIButtonsLarge.UseVisualStyleBackColor = true;
			// 
			// __sMoreGrp
			// 
			this.@__sMoreGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__sMoreGrp.Controls.Add(this.label1);
			this.@__sMoreGrp.Controls.Add(this.@__sLibDel);
			this.@__sMoreGrp.Controls.Add(this.@__sLibAdd);
			this.@__sMoreGrp.Controls.Add(this.@__sLib);
			this.@__sMoreGrp.Controls.Add(this.@__sLibLbl);
			this.@__sMoreGrp.Location = new System.Drawing.Point(6, 139);
			this.@__sMoreGrp.Name = "__sMoreGrp";
			this.@__sMoreGrp.Size = new System.Drawing.Size(340, 184);
			this.@__sMoreGrp.TabIndex = 2;
			this.@__sMoreGrp.TabStop = false;
			this.@__sMoreGrp.Text = "Extensibility";
			// 
			// __sLibDel
			// 
			this.@__sLibDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__sLibDel.Enabled = false;
			this.@__sLibDel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__sLibDel.Location = new System.Drawing.Point(259, 155);
			this.@__sLibDel.Name = "__sLibDel";
			this.@__sLibDel.Size = new System.Drawing.Size(75, 23);
			this.@__sLibDel.TabIndex = 2;
			this.@__sLibDel.Text = "Remove";
			this.@__sLibDel.UseVisualStyleBackColor = true;
			// 
			// __sLibAdd
			// 
			this.@__sLibAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__sLibAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__sLibAdd.Location = new System.Drawing.Point(178, 155);
			this.@__sLibAdd.Name = "__sLibAdd";
			this.@__sLibAdd.Size = new System.Drawing.Size(75, 23);
			this.@__sLibAdd.TabIndex = 1;
			this.@__sLibAdd.Text = "Add...";
			this.@__sLibAdd.UseVisualStyleBackColor = true;
			// 
			// __sLib
			// 
			this.@__sLib.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__sLib.FormattingEnabled = true;
			this.@__sLib.IntegralHeight = false;
			this.@__sLib.Location = new System.Drawing.Point(9, 66);
			this.@__sLib.Name = "__sLib";
			this.@__sLib.Size = new System.Drawing.Size(325, 83);
			this.@__sLib.TabIndex = 3;
			// 
			// __sLibLbl
			// 
			this.@__sLibLbl.Location = new System.Drawing.Point(9, 17);
			this.@__sLibLbl.Name = "__sLibLbl";
			this.@__sLibLbl.Size = new System.Drawing.Size(325, 33);
			this.@__sLibLbl.TabIndex = 0;
			this.@__sLibLbl.Text = "Load ResourceData, ResourceSource, and TypeViewer subclasses from these assemblie" +
				"s:";
			// 
			// __tAbout
			// 
			this.@__tAbout.Controls.Add(this.@__version);
			this.@__tAbout.Controls.Add(this.@__versionLbl);
			this.@__tAbout.Controls.Add(this.linkLabel1);
			this.@__tAbout.Controls.Add(this.@__makesUseOf);
			this.@__tAbout.Controls.Add(this.@__linksLbl);
			this.@__tAbout.Controls.Add(this.@__aboutLinkAnolis);
			this.@__tAbout.Controls.Add(this.@__aboutBlurb);
			this.@__tAbout.Controls.Add(this.@__aboutAnolis);
			this.@__tAbout.Location = new System.Drawing.Point(4, 22);
			this.@__tAbout.Name = "__tAbout";
			this.@__tAbout.Padding = new System.Windows.Forms.Padding(3);
			this.@__tAbout.Size = new System.Drawing.Size(355, 344);
			this.@__tAbout.TabIndex = 1;
			this.@__tAbout.Text = "About";
			this.@__tAbout.UseVisualStyleBackColor = true;
			// 
			// __version
			// 
			this.@__version.AutoSize = true;
			this.@__version.Location = new System.Drawing.Point(10, 153);
			this.@__version.Name = "__version";
			this.@__version.Size = new System.Drawing.Size(106, 13);
			this.@__version.TabIndex = 23;
			this.@__version.Text = "%VersionGoesHere%";
			// 
			// __versionLbl
			// 
			this.@__versionLbl.AutoSize = true;
			this.@__versionLbl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.@__versionLbl.Location = new System.Drawing.Point(9, 138);
			this.@__versionLbl.Name = "__versionLbl";
			this.@__versionLbl.Size = new System.Drawing.Size(49, 13);
			this.@__versionLbl.TabIndex = 22;
			this.@__versionLbl.Text = "Version";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 14);
			this.linkLabel1.Location = new System.Drawing.Point(9, 199);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(184, 14);
			this.linkLabel1.TabIndex = 20;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://anol.is";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// __makesUseOf
			// 
			this.@__makesUseOf.AutoSize = true;
			this.@__makesUseOf.Location = new System.Drawing.Point(9, 113);
			this.@__makesUseOf.Name = "__makesUseOf";
			this.@__makesUseOf.Size = new System.Drawing.Size(326, 13);
			this.@__makesUseOf.TabIndex = 18;
			this.@__makesUseOf.Text = "Resourcer makes use of the Be HexBox control, used under license";
			// 
			// __linksLbl
			// 
			this.@__linksLbl.AutoSize = true;
			this.@__linksLbl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.@__linksLbl.Location = new System.Drawing.Point(9, 186);
			this.@__linksLbl.Name = "__linksLbl";
			this.@__linksLbl.Size = new System.Drawing.Size(36, 13);
			this.@__linksLbl.TabIndex = 16;
			this.@__linksLbl.Text = "Links";
			// 
			// __aboutLinkAnolis
			// 
			this.@__aboutLinkAnolis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__aboutLinkAnolis.LinkArea = new System.Windows.Forms.LinkArea(0, 30);
			this.@__aboutLinkAnolis.Location = new System.Drawing.Point(9, 212);
			this.@__aboutLinkAnolis.Name = "__aboutLinkAnolis";
			this.@__aboutLinkAnolis.Size = new System.Drawing.Size(184, 14);
			this.@__aboutLinkAnolis.TabIndex = 3;
			this.@__aboutLinkAnolis.TabStop = true;
			this.@__aboutLinkAnolis.Text = "http://www.codeplex.com/anolis";
			this.@__aboutLinkAnolis.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// __aboutBlurb
			// 
			this.@__aboutBlurb.AutoSize = true;
			this.@__aboutBlurb.Location = new System.Drawing.Point(9, 32);
			this.@__aboutBlurb.Name = "__aboutBlurb";
			this.@__aboutBlurb.Size = new System.Drawing.Size(277, 65);
			this.@__aboutBlurb.TabIndex = 1;
			this.@__aboutBlurb.Text = "Resource hacker for the Anolis project. GPL Licensed.\r\n\r\nBy David Rees\r\nSignifica" +
				"nt contributions by Sven Groot and Stanley Chan\r\nAcknowledgements to Colin Wilso" +
				"n and Angus Johnson";
			// 
			// __aboutAnolis
			// 
			this.@__aboutAnolis.AutoSize = true;
			this.@__aboutAnolis.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
			this.@__aboutAnolis.Location = new System.Drawing.Point(8, 9);
			this.@__aboutAnolis.Name = "__aboutAnolis";
			this.@__aboutAnolis.Size = new System.Drawing.Size(171, 23);
			this.@__aboutAnolis.TabIndex = 0;
			this.@__aboutAnolis.Text = "Anolis Resourcer";
			// 
			// __tLegal
			// 
			this.@__tLegal.Controls.Add(this.@__legalToggle);
			this.@__tLegal.Controls.Add(this.@__legalText);
			this.@__tLegal.Location = new System.Drawing.Point(4, 22);
			this.@__tLegal.Name = "__tLegal";
			this.@__tLegal.Size = new System.Drawing.Size(355, 344);
			this.@__tLegal.TabIndex = 2;
			this.@__tLegal.Text = "Legal";
			this.@__tLegal.UseVisualStyleBackColor = true;
			// 
			// __legalToggle
			// 
			this.@__legalToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__legalToggle.Location = new System.Drawing.Point(3, 312);
			this.@__legalToggle.Name = "__legalToggle";
			this.@__legalToggle.Size = new System.Drawing.Size(170, 23);
			this.@__legalToggle.TabIndex = 1;
			this.@__legalToggle.Text = "View GPLv2 License";
			this.@__legalToggle.UseVisualStyleBackColor = true;
			// 
			// __legalText
			// 
			this.@__legalText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__legalText.BackColor = System.Drawing.SystemColors.Window;
			this.@__legalText.Location = new System.Drawing.Point(3, 3);
			this.@__legalText.Multiline = true;
			this.@__legalText.Name = "__legalText";
			this.@__legalText.ReadOnly = true;
			this.@__legalText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.@__legalText.Size = new System.Drawing.Size(349, 303);
			this.@__legalText.TabIndex = 0;
			// 
			// __cancel
			// 
			this.@__cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__cancel.Location = new System.Drawing.Point(294, 383);
			this.@__cancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(75, 23);
			this.@__cancel.TabIndex = 1;
			this.@__cancel.Text = "Cancel";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__ok.Location = new System.Drawing.Point(213, 383);
			this.@__ok.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 0;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// __update
			// 
			this.@__update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__update.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.@__update.Location = new System.Drawing.Point(6, 383);
			this.@__update.Name = "__update";
			this.@__update.Size = new System.Drawing.Size(137, 23);
			this.@__update.TabIndex = 4;
			this.@__update.Text = "Check for Updates...";
			this.@__update.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.FileName = "openFileDialog1";
			this.@__ofd.Filter = "Managed Assemblies (*.exe;*.dll)|*.exe;*.dll";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(291, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Extensibility changes require a program restart to take effect.";
			// 
			// OptionsForm
			// 
			this.AcceptButton = this.@__ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__cancel;
			this.ClientSize = new System.Drawing.Size(375, 413);
			this.Controls.Add(this.@__update);
			this.Controls.Add(this.@__ok);
			this.Controls.Add(this.@__cancel);
			this.Controls.Add(this.@__tabs);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Options";
			this.@__tabs.ResumeLayout(false);
			this.@__tSettings.ResumeLayout(false);
			this.@__sUiGrp.ResumeLayout(false);
			this.@__sUiGrp.PerformLayout();
			this.@__sMoreGrp.ResumeLayout(false);
			this.@__sMoreGrp.PerformLayout();
			this.@__tAbout.ResumeLayout(false);
			this.@__tAbout.PerformLayout();
			this.@__tLegal.ResumeLayout(false);
			this.@__tLegal.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl __tabs;
		private System.Windows.Forms.TabPage __tSettings;
		private System.Windows.Forms.TabPage __tAbout;
		private System.Windows.Forms.Button __cancel;
		private System.Windows.Forms.Button __ok;
		private System.Windows.Forms.Label __aboutBlurb;
		private System.Windows.Forms.Label __aboutAnolis;
		private System.Windows.Forms.LinkLabel __aboutLinkAnolis;
		private System.Windows.Forms.TabPage __tLegal;
		private System.Windows.Forms.TextBox __legalText;
		private System.Windows.Forms.GroupBox __sUiGrp;
		private System.Windows.Forms.Label __sUIButtonsLargeLbl;
		private System.Windows.Forms.CheckBox __sUIButtonsLarge;
		private System.Windows.Forms.GroupBox __sMoreGrp;
		private System.Windows.Forms.Label __sAssocLbl;
		private System.Windows.Forms.CheckBox __sAssoc;
		private System.Windows.Forms.Button __sLibDel;
		private System.Windows.Forms.Button __sLibAdd;
		private System.Windows.Forms.Label __sLibLbl;
		private System.Windows.Forms.Label __linksLbl;
		private System.Windows.Forms.Label __makesUseOf;
		private System.Windows.Forms.Button __legalToggle;
		private System.Windows.Forms.Button __update;
		private System.Windows.Forms.Label __sAssocWarnLbl;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.ListBox __sLib;
		private System.Windows.Forms.Label __version;
		private System.Windows.Forms.Label __versionLbl;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.Label label1;
	}
}