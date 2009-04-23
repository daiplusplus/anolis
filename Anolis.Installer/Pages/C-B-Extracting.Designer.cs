namespace Anolis.Installer.Pages {
	partial class ExtractingPage {
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
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__progress = new System.Windows.Forms.ProgressBar();
			this.@__statusLbl = new System.Windows.Forms.Label();
			this.@__packageMessages = new System.Windows.Forms.TextBox();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__packageMessages);
			this.@__content.Controls.Add(this.@__statusLbl);
			this.@__content.Controls.Add(this.@__progress);
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "Anolis Package (*.anop)|*.anop|All files (*.*)|*.*";
			this.@__ofd.Title = "Select Anolis Package File";
			// 
			// __progress
			// 
			this.@__progress.Location = new System.Drawing.Point(22, 54);
			this.@__progress.Name = "__progress";
			this.@__progress.Size = new System.Drawing.Size(413, 23);
			this.@__progress.TabIndex = 0;
			// 
			// __statusLbl
			// 
			this.@__statusLbl.AutoSize = true;
			this.@__statusLbl.Location = new System.Drawing.Point(19, 80);
			this.@__statusLbl.Name = "__statusLbl";
			this.@__statusLbl.Size = new System.Drawing.Size(98, 13);
			this.@__statusLbl.TabIndex = 1;
			this.@__statusLbl.Text = "{0}% complete - {1}";
			// 
			// __packageMessages
			// 
			this.@__packageMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__packageMessages.BackColor = System.Drawing.SystemColors.Window;
			this.@__packageMessages.Location = new System.Drawing.Point(22, 109);
			this.@__packageMessages.Multiline = true;
			this.@__packageMessages.Name = "__packageMessages";
			this.@__packageMessages.ReadOnly = true;
			this.@__packageMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.@__packageMessages.Size = new System.Drawing.Size(413, 116);
			this.@__packageMessages.TabIndex = 2;
			this.@__packageMessages.Visible = false;
			this.@__packageMessages.WordWrap = false;
			// 
			// ExtractingPage
			// 
			this.Name = "ExtractingPage";
			this.PageSubtitle = "Please wait whilst the installer prepares the package for installation";
			this.PageTitle = "Preparing Package";
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.Label __statusLbl;
		private System.Windows.Forms.ProgressBar __progress;
		private System.Windows.Forms.TextBox __packageMessages;
	}
}
