namespace Anolis.Installer.Pages {
	partial class MainActionPage {
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
			this.@__installRad = new System.Windows.Forms.RadioButton();
			this.@__installBlurb = new System.Windows.Forms.Label();
			this.@__uninstallRad = new System.Windows.Forms.RadioButton();
			this.@__uninstallBlurb = new System.Windows.Forms.Label();
			this.@__toolsRad = new System.Windows.Forms.RadioButton();
			this.@__toolsBlurb = new System.Windows.Forms.Label();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__installRad);
			this.@__content.Controls.Add(this.@__installBlurb);
			this.@__content.Controls.Add(this.@__uninstallRad);
			this.@__content.Controls.Add(this.@__uninstallBlurb);
			this.@__content.Controls.Add(this.@__toolsRad);
			this.@__content.Controls.Add(this.@__toolsBlurb);
			// 
			// __installRad
			// 
			this.@__installRad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__installRad.BackColor = System.Drawing.Color.Transparent;
			this.@__installRad.Checked = true;
			this.@__installRad.Location = new System.Drawing.Point(22, 20);
			this.@__installRad.Name = "__installRad";
			this.@__installRad.Size = new System.Drawing.Size(422, 25);
			this.@__installRad.TabIndex = 0;
			this.@__installRad.TabStop = true;
			this.@__installRad.Text = "Install a package";
			this.@__installRad.UseVisualStyleBackColor = false;
			// 
			// __installBlurb
			// 
			this.@__installBlurb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__installBlurb.BackColor = System.Drawing.Color.Transparent;
			this.@__installBlurb.Location = new System.Drawing.Point(39, 50);
			this.@__installBlurb.Name = "__installBlurb";
			this.@__installBlurb.Size = new System.Drawing.Size(408, 40);
			this.@__installBlurb.TabIndex = 1;
			this.@__installBlurb.Text = "Installs a package located in this Anolis distribution or elsewhere in this compu" +
				"ter. The package can be installed to a setup files (I386) folder.";
			// 
			// __uninstallRad
			// 
			this.@__uninstallRad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__uninstallRad.BackColor = System.Drawing.Color.Transparent;
			this.@__uninstallRad.Location = new System.Drawing.Point(22, 84);
			this.@__uninstallRad.Name = "__uninstallRad";
			this.@__uninstallRad.Size = new System.Drawing.Size(422, 25);
			this.@__uninstallRad.TabIndex = 2;
			this.@__uninstallRad.Text = "Undo changes made by this distribution";
			this.@__uninstallRad.UseVisualStyleBackColor = false;
			// 
			// __uninstallBlurb
			// 
			this.@__uninstallBlurb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__uninstallBlurb.BackColor = System.Drawing.Color.Transparent;
			this.@__uninstallBlurb.Location = new System.Drawing.Point(40, 113);
			this.@__uninstallBlurb.Name = "__uninstallBlurb";
			this.@__uninstallBlurb.Size = new System.Drawing.Size(407, 40);
			this.@__uninstallBlurb.TabIndex = 3;
			this.@__uninstallBlurb.Text = "Recovers files to their before-patched status. You will need the backup directory" +
				" that was created during installation";
			// 
			// __toolsRad
			// 
			this.@__toolsRad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__toolsRad.BackColor = System.Drawing.Color.Transparent;
			this.@__toolsRad.Location = new System.Drawing.Point(22, 152);
			this.@__toolsRad.Name = "__toolsRad";
			this.@__toolsRad.Size = new System.Drawing.Size(422, 25);
			this.@__toolsRad.TabIndex = 4;
			this.@__toolsRad.Text = "Make my own package or distribution";
			this.@__toolsRad.UseVisualStyleBackColor = false;
			// 
			// __toolsBlurb
			// 
			this.@__toolsBlurb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__toolsBlurb.BackColor = System.Drawing.Color.Transparent;
			this.@__toolsBlurb.Location = new System.Drawing.Point(39, 180);
			this.@__toolsBlurb.Name = "__toolsBlurb";
			this.@__toolsBlurb.Size = new System.Drawing.Size(408, 34);
			this.@__toolsBlurb.TabIndex = 5;
			this.@__toolsBlurb.Text = "Downloads tools so you can easily make your own package or distribution";
			// 
			// MainActionPage
			// 
			this.Name = "MainActionPage";
			this.PageSubtitle = "";
			this.PageTitle = "";
			this.@__content.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RadioButton __installRad;
		private System.Windows.Forms.Label __installBlurb;
		private System.Windows.Forms.Label __toolsBlurb;
		private System.Windows.Forms.RadioButton __toolsRad;
		private System.Windows.Forms.Label __uninstallBlurb;
		private System.Windows.Forms.RadioButton __uninstallRad;
	}
}
