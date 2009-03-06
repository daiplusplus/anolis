namespace Anolis.Installer.Pages {
	partial class FinishedPage {
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.@__anolisWebsite = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.@__installationComplete = new System.Windows.Forms.Label();
			this.@__openingText.SuspendLayout();
			this.SuspendLayout();
			// 
			// __title
			// 
			this.@__title.BackColor = System.Drawing.Color.Transparent;
			this.@__title.Text = "Installation Complete";
			// 
			// __openingText
			// 
			this.@__openingText.BackColor = System.Drawing.Color.Transparent;
			this.@__openingText.Controls.Add(this.@__installationComplete);
			this.@__openingText.Controls.Add(this.label3);
			this.@__openingText.Controls.Add(this.@__anolisWebsite);
			this.@__openingText.Controls.Add(this.label2);
			this.@__openingText.Controls.Add(this.label1);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 178);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Credits";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 196);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(316, 60);
			this.label2.TabIndex = 1;
			this.label2.Text = "Anolis by David Rees\r\nSignificant contributions by Sven Groot, Rafael Rivera, and" +
				" Stanley Chan\r\nShouts to Long Zheng and Chris Kite";
			// 
			// __anolisWebsite
			// 
			this.@__anolisWebsite.AutoSize = true;
			this.@__anolisWebsite.Location = new System.Drawing.Point(99, 153);
			this.@__anolisWebsite.Name = "__anolisWebsite";
			this.@__anolisWebsite.Size = new System.Drawing.Size(68, 13);
			this.@__anolisWebsite.TabIndex = 2;
			this.@__anolisWebsite.TabStop = true;
			this.@__anolisWebsite.Text = "http://anol.is";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 153);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Anolis Homepage";
			// 
			// __installationComplete
			// 
			this.@__installationComplete.Location = new System.Drawing.Point(102, 0);
			this.@__installationComplete.Name = "__installationComplete";
			this.@__installationComplete.Size = new System.Drawing.Size(220, 88);
			this.@__installationComplete.TabIndex = 4;
			this.@__installationComplete.Text = "Installation has been completed succesfully. Your computer will now restart.\r\n\r\nP" +
				"lease close all open applications before continuing. Do not attempt to open any " +
				"new applications.";
			// 
			// FinishedPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Anolis.Gui.Properties.Resources.Background;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Name = "FinishedPage";
			this.@__openingText.ResumeLayout(false);
			this.@__openingText.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel __anolisWebsite;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label __installationComplete;

	}
}
