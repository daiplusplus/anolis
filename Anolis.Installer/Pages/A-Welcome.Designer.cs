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
			this.@__openingText.BackColor = System.Drawing.Color.Transparent;
			this.@__openingText.Controls.Add(this.@__inst1);
			this.@__openingText.Location = new System.Drawing.Point(174, 177);
			this.@__openingText.Size = new System.Drawing.Size(322, 123);
			// 
			// __inst1
			// 
			this.@__inst1.BackColor = System.Drawing.Color.Transparent;
			this.@__inst1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__inst1.Location = new System.Drawing.Point(0, 0);
			this.@__inst1.Margin = new System.Windows.Forms.Padding(3, 100, 3, 0);
			this.@__inst1.Name = "__inst1";
			this.@__inst1.Size = new System.Drawing.Size(322, 123);
			this.@__inst1.TabIndex = 0;
			this.@__inst1.Text = "This wizard will guide you through the steps involve in installing an Anolis pack" +
				"age\r\n\r\nDistributions of Anolis can customise the text placed here and throughout" +
				" the wizard\r\n\r\nTo continue, press Next";
			// 
			// WelcomePage
			// 
			this.Name = "WelcomePage";
			this.@__openingText.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __inst1;

	}
}
