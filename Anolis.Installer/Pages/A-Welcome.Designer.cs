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
			this.@__culture = new System.Windows.Forms.ComboBox();
			this.@__cultureLbl = new System.Windows.Forms.Label();
			this.@__cultureAttrib = new System.Windows.Forms.Label();
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
			this.@__openingText.Controls.Add(this.@__cultureAttrib);
			this.@__openingText.Controls.Add(this.@__inst1);
			this.@__openingText.Controls.Add(this.@__culture);
			this.@__openingText.Controls.Add(this.@__cultureLbl);
			this.@__openingText.Location = new System.Drawing.Point(174, 165);
			this.@__openingText.Size = new System.Drawing.Size(322, 135);
			// 
			// __inst1
			// 
			this.@__inst1.BackColor = System.Drawing.Color.Transparent;
			this.@__inst1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.@__inst1.Location = new System.Drawing.Point(0, 43);
			this.@__inst1.Margin = new System.Windows.Forms.Padding(3, 100, 3, 0);
			this.@__inst1.Name = "__inst1";
			this.@__inst1.Size = new System.Drawing.Size(322, 92);
			this.@__inst1.TabIndex = 0;
			this.@__inst1.Text = "This wizard will guide you through the steps involve in installing an Anolis pack" +
				"age\r\n\r\nTo continue, press Next";
			// 
			// __culture
			// 
			this.@__culture.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.@__culture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.@__culture.FormattingEnabled = true;
			this.@__culture.Location = new System.Drawing.Point(3, 16);
			this.@__culture.Name = "__culture";
			this.@__culture.Size = new System.Drawing.Size(189, 21);
			this.@__culture.TabIndex = 2;
			// 
			// __cultureLbl
			// 
			this.@__cultureLbl.AutoSize = true;
			this.@__cultureLbl.BackColor = System.Drawing.Color.Transparent;
			this.@__cultureLbl.Location = new System.Drawing.Point(3, 0);
			this.@__cultureLbl.Name = "__cultureLbl";
			this.@__cultureLbl.Size = new System.Drawing.Size(108, 13);
			this.@__cultureLbl.TabIndex = 3;
			this.@__cultureLbl.Text = "Installation Language";
			// 
			// __cultureAttrib
			// 
			this.@__cultureAttrib.Location = new System.Drawing.Point(198, 12);
			this.@__cultureAttrib.Name = "__cultureAttrib";
			this.@__cultureAttrib.Size = new System.Drawing.Size(117, 26);
			this.@__cultureAttrib.TabIndex = 4;
			this.@__cultureAttrib.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// WelcomePage
			// 
			this.Name = "WelcomePage";
			this.@__openingText.ResumeLayout(false);
			this.@__openingText.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label __inst1;
		private System.Windows.Forms.ComboBox __culture;
		private System.Windows.Forms.Label __cultureLbl;
		private System.Windows.Forms.Label __cultureAttrib;

	}
}
