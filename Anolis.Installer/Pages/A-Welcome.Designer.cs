namespace Anolis.Installer.Pages {
	partial class WelcomePage {
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
			this.@__inst1 = new System.Windows.Forms.Label();
			this.@__inst3 = new System.Windows.Forms.Label();
			this.@__inst2 = new System.Windows.Forms.Label();
			this.@__openingText.SuspendLayout();
			this.SuspendLayout();
			// 
			// __title
			// 
			this.@__title.BackColor = System.Drawing.Color.Transparent;
			this.@__title.Text = "Welcome to the Anolis Package Installer";
			// 
			// __openingText
			// 
			this.@__openingText.Controls.Add(this.@__inst2);
			this.@__openingText.Controls.Add(this.@__inst3);
			this.@__openingText.Controls.Add(this.@__inst1);
			this.@__openingText.Location = new System.Drawing.Point(174, 177);
			this.@__openingText.Size = new System.Drawing.Size(322, 123);
			// 
			// __inst1
			// 
			this.@__inst1.Dock = System.Windows.Forms.DockStyle.Top;
			this.@__inst1.Location = new System.Drawing.Point(0, 0);
			this.@__inst1.Margin = new System.Windows.Forms.Padding(3, 100, 3, 0);
			this.@__inst1.Name = "__inst1";
			this.@__inst1.Size = new System.Drawing.Size(322, 33);
			this.@__inst1.TabIndex = 0;
			this.@__inst1.Text = "This wizard will guide you through the steps involve in installing an Anolis pack" +
				"age";
			// 
			// __inst3
			// 
			this.@__inst3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.@__inst3.Location = new System.Drawing.Point(0, 110);
			this.@__inst3.Name = "__inst3";
			this.@__inst3.Size = new System.Drawing.Size(322, 13);
			this.@__inst3.TabIndex = 1;
			this.@__inst3.Text = "To continue, press Next";
			// 
			// __inst2
			// 
			this.@__inst2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__inst2.Location = new System.Drawing.Point(0, 33);
			this.@__inst2.Name = "__inst2";
			this.@__inst2.Size = new System.Drawing.Size(322, 77);
			this.@__inst2.TabIndex = 2;
			this.@__inst2.Text = "Distributions of Anolis can customise the text placed here and throughout the wiz" +
				"ard";
			// 
			// WelcomePage
			// 
			
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackgroundImage = global::Anolis.Installer.Properties.Resources.Background;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Name = "WelcomePage";
			this.@__openingText.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __inst2;
		private System.Windows.Forms.Label __inst3;
		private System.Windows.Forms.Label __inst1;
	}
}
