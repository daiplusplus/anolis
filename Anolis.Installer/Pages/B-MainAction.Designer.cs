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
			this.@__optInstallRad = new System.Windows.Forms.RadioButton();
			this.@__optInstallNotes = new System.Windows.Forms.Label();
			this.@__optUndoRad = new System.Windows.Forms.RadioButton();
			this.@__optUndoNotes = new System.Windows.Forms.Label();
			this.@__optCreateRad = new System.Windows.Forms.RadioButton();
			this.@__optCreateNotes = new System.Windows.Forms.Label();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__optCreateRad);
			this.@__content.Controls.Add(this.@__optUndoRad);
			this.@__content.Controls.Add(this.@__optInstallRad);
			this.@__content.Controls.Add(this.@__optCreateNotes);
			this.@__content.Controls.Add(this.@__optUndoNotes);
			this.@__content.Controls.Add(this.@__optInstallNotes);
			// 
			// __optInstallRad
			// 
			this.@__optInstallRad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__optInstallRad.BackColor = System.Drawing.Color.Transparent;
			this.@__optInstallRad.Checked = true;
			this.@__optInstallRad.Location = new System.Drawing.Point(22, 20);
			this.@__optInstallRad.Name = "__optInstallRad";
			this.@__optInstallRad.Size = new System.Drawing.Size(434, 25);
			this.@__optInstallRad.TabIndex = 0;
			this.@__optInstallRad.TabStop = true;
			this.@__optInstallRad.Text = "Install a package";
			this.@__optInstallRad.UseVisualStyleBackColor = false;
			// 
			// __optInstallNotes
			// 
			this.@__optInstallNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__optInstallNotes.BackColor = System.Drawing.Color.Transparent;
			this.@__optInstallNotes.Location = new System.Drawing.Point(39, 50);
			this.@__optInstallNotes.Name = "__optInstallNotes";
			this.@__optInstallNotes.Size = new System.Drawing.Size(408, 40);
			this.@__optInstallNotes.TabIndex = 1;
			this.@__optInstallNotes.Text = "Installs a package located in this Anolis distribution or elsewhere in this compu" +
				"ter. The package can be installed to a setup files (I386) folder.";
			// 
			// __optUndoRad
			// 
			this.@__optUndoRad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__optUndoRad.BackColor = System.Drawing.Color.Transparent;
			this.@__optUndoRad.Location = new System.Drawing.Point(22, 84);
			this.@__optUndoRad.Name = "__optUndoRad";
			this.@__optUndoRad.Size = new System.Drawing.Size(434, 25);
			this.@__optUndoRad.TabIndex = 2;
			this.@__optUndoRad.Text = "Undo changes made by this distribution";
			this.@__optUndoRad.UseVisualStyleBackColor = false;
			// 
			// __optUndoNotes
			// 
			this.@__optUndoNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__optUndoNotes.BackColor = System.Drawing.Color.Transparent;
			this.@__optUndoNotes.Location = new System.Drawing.Point(40, 113);
			this.@__optUndoNotes.Name = "__optUndoNotes";
			this.@__optUndoNotes.Size = new System.Drawing.Size(416, 40);
			this.@__optUndoNotes.TabIndex = 3;
			this.@__optUndoNotes.Text = "Recovers files to their before-patched status. You will need the backup directory" +
				" that was created during installation";
			// 
			// __optCreateRad
			// 
			this.@__optCreateRad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__optCreateRad.BackColor = System.Drawing.Color.Transparent;
			this.@__optCreateRad.Location = new System.Drawing.Point(22, 152);
			this.@__optCreateRad.Name = "__optCreateRad";
			this.@__optCreateRad.Size = new System.Drawing.Size(434, 25);
			this.@__optCreateRad.TabIndex = 4;
			this.@__optCreateRad.Text = "Make my own package or distribution";
			this.@__optCreateRad.UseVisualStyleBackColor = false;
			// 
			// __optCreateNotes
			// 
			this.@__optCreateNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__optCreateNotes.BackColor = System.Drawing.Color.Transparent;
			this.@__optCreateNotes.Location = new System.Drawing.Point(39, 180);
			this.@__optCreateNotes.Name = "__optCreateNotes";
			this.@__optCreateNotes.Size = new System.Drawing.Size(417, 34);
			this.@__optCreateNotes.TabIndex = 5;
			this.@__optCreateNotes.Text = "Downloads tools so you can easily make your own package or distribution";
			// 
			// MainActionPage
			// 
			this.Name = "MainActionPage";
			this.PageTitle = "Select a Task";
			this.Subtitle = "Choose an operation to perform";
			this.@__content.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RadioButton __optInstallRad;
		private System.Windows.Forms.Label __optInstallNotes;
		private System.Windows.Forms.Label __optCreateNotes;
		private System.Windows.Forms.RadioButton __optCreateRad;
		private System.Windows.Forms.Label __optUndoNotes;
		private System.Windows.Forms.RadioButton __optUndoRad;
	}
}
