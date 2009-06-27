namespace Anolis.Installer.Pages {
	partial class ReleaseNotesPage {
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
			this.@__packageRtf = new System.Windows.Forms.RichTextBox();
			this.@__tabs = new System.Windows.Forms.TabControl();
			this.@__packageTab = new System.Windows.Forms.TabPage();
			this.@__installerTab = new System.Windows.Forms.TabPage();
			this.@__installerRtf = new System.Windows.Forms.RichTextBox();
			this.@__content.SuspendLayout();
			this.@__tabs.SuspendLayout();
			this.@__packageTab.SuspendLayout();
			this.@__installerTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__tabs);
			// 
			// __packageRtf
			// 
			this.@__packageRtf.BackColor = System.Drawing.SystemColors.Window;
			this.@__packageRtf.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__packageRtf.Location = new System.Drawing.Point(0, 0);
			this.@__packageRtf.Margin = new System.Windows.Forms.Padding(0);
			this.@__packageRtf.Name = "__packageRtf";
			this.@__packageRtf.ReadOnly = true;
			this.@__packageRtf.Size = new System.Drawing.Size(439, 206);
			this.@__packageRtf.TabIndex = 0;
			this.@__packageRtf.Text = "";
			// 
			// __tabs
			// 
			this.@__tabs.Controls.Add(this.@__packageTab);
			this.@__tabs.Controls.Add(this.@__installerTab);
			this.@__tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__tabs.Location = new System.Drawing.Point(0, 0);
			this.@__tabs.Name = "__tabs";
			this.@__tabs.SelectedIndex = 0;
			this.@__tabs.Size = new System.Drawing.Size(447, 232);
			this.@__tabs.TabIndex = 1;
			// 
			// __packageTab
			// 
			this.@__packageTab.Controls.Add(this.@__packageRtf);
			this.@__packageTab.Location = new System.Drawing.Point(4, 22);
			this.@__packageTab.Name = "__packageTab";
			this.@__packageTab.Size = new System.Drawing.Size(439, 206);
			this.@__packageTab.TabIndex = 0;
			this.@__packageTab.Text = "Package Release Notes";
			this.@__packageTab.UseVisualStyleBackColor = true;
			// 
			// __installerTab
			// 
			this.@__installerTab.Controls.Add(this.@__installerRtf);
			this.@__installerTab.Location = new System.Drawing.Point(4, 22);
			this.@__installerTab.Name = "__installerTab";
			this.@__installerTab.Size = new System.Drawing.Size(439, 206);
			this.@__installerTab.TabIndex = 1;
			this.@__installerTab.Text = "Anolis Installer Readme";
			this.@__installerTab.UseVisualStyleBackColor = true;
			// 
			// __installerRtf
			// 
			this.@__installerRtf.BackColor = System.Drawing.SystemColors.Window;
			this.@__installerRtf.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__installerRtf.Location = new System.Drawing.Point(0, 0);
			this.@__installerRtf.Margin = new System.Windows.Forms.Padding(0);
			this.@__installerRtf.Name = "__installerRtf";
			this.@__installerRtf.ReadOnly = true;
			this.@__installerRtf.Size = new System.Drawing.Size(439, 206);
			this.@__installerRtf.TabIndex = 0;
			this.@__installerRtf.Text = "";
			// 
			// ReleaseNotesPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "ReleaseNotesPage";
			this.PageSubtitle = "Review any release notes made by the package\'s author";
			this.PageTitle = "Package Release Notes";
			this.@__content.ResumeLayout(false);
			this.@__tabs.ResumeLayout(false);
			this.@__packageTab.ResumeLayout(false);
			this.@__installerTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox __packageRtf;
		private System.Windows.Forms.TabControl __tabs;
		private System.Windows.Forms.TabPage __packageTab;
		private System.Windows.Forms.TabPage __installerTab;
		private System.Windows.Forms.RichTextBox __installerRtf;
	}
}
