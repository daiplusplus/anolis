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
			this.@__optUndo = new System.Windows.Forms.RadioButton();
			this.@__optUndoNotes = new System.Windows.Forms.Label();
			this.@__optCreateRad = new System.Windows.Forms.RadioButton();
			this.@__optCreateNotes = new System.Windows.Forms.Label();
			this.@__banner.SuspendLayout();
			this.@__content.SuspendLayout();
			this.SuspendLayout();
			this.@__banner.Controls.SetChildIndex(this.@__bannerTitle, 0);
			this.@__banner.Controls.SetChildIndex(this.@__bannerSubtitle, 0);
			// 
			// __content
			// 
			this.@__content.Controls.Add(this.@__optCreateNotes);
			this.@__content.Controls.Add(this.@__optCreateRad);
			this.@__content.Controls.Add(this.@__optUndoNotes);
			this.@__content.Controls.Add(this.@__optUndo);
			this.@__content.Controls.Add(this.@__optInstallNotes);
			this.@__content.Controls.Add(this.@__optInstallRad);
			// 
			// __bannerSubtitle
			// 
			this.@__bannerSubtitle.Size = new System.Drawing.Size(356, 30);
			this.@__bannerSubtitle.Text = "Choose an operation to perform";
			// 
			// __bannerTitle
			// 
			this.@__bannerTitle.Size = new System.Drawing.Size(378, 15);
			this.@__bannerTitle.Text = "Select a Task";
			// 
			// __optInstallRad
			// 
			this.@__optInstallRad.AutoSize = true;
			this.@__optInstallRad.Checked = true;
			this.@__optInstallRad.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__optInstallRad.Location = new System.Drawing.Point(22, 24);
			this.@__optInstallRad.Name = "__optInstallRad";
			this.@__optInstallRad.Size = new System.Drawing.Size(122, 17);
			this.@__optInstallRad.TabIndex = 0;
			this.@__optInstallRad.TabStop = true;
			this.@__optInstallRad.Text = "Install a package";
			this.@__optInstallRad.UseVisualStyleBackColor = true;
			// 
			// __optInstallNotes
			// 
			this.@__optInstallNotes.Location = new System.Drawing.Point(39, 44);
			this.@__optInstallNotes.Name = "__optInstallNotes";
			this.@__optInstallNotes.Size = new System.Drawing.Size(414, 31);
			this.@__optInstallNotes.TabIndex = 1;
			this.@__optInstallNotes.Text = "Installs a package located in this Anolis distribution or elsewhere in this compu" +
				"ter";
			// 
			// __optUndo
			// 
			this.@__optUndo.AutoSize = true;
			this.@__optUndo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__optUndo.Location = new System.Drawing.Point(22, 78);
			this.@__optUndo.Name = "__optUndo";
			this.@__optUndo.Size = new System.Drawing.Size(248, 17);
			this.@__optUndo.TabIndex = 2;
			this.@__optUndo.Text = "Undo changes made by this distribution";
			this.@__optUndo.UseVisualStyleBackColor = true;
			// 
			// __optUndoNotes
			// 
			this.@__optUndoNotes.Location = new System.Drawing.Point(39, 98);
			this.@__optUndoNotes.Name = "__optUndoNotes";
			this.@__optUndoNotes.Size = new System.Drawing.Size(414, 33);
			this.@__optUndoNotes.TabIndex = 3;
			this.@__optUndoNotes.Text = "Recovers files to their before-patched status. You will need the backup directory" +
				" that was created during installation";
			// 
			// __optCreateRad
			// 
			this.@__optCreateRad.AutoSize = true;
			this.@__optCreateRad.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__optCreateRad.Location = new System.Drawing.Point(22, 145);
			this.@__optCreateRad.Name = "__optCreateRad";
			this.@__optCreateRad.Size = new System.Drawing.Size(237, 17);
			this.@__optCreateRad.TabIndex = 4;
			this.@__optCreateRad.Text = "Make my own package or distribution";
			this.@__optCreateRad.UseVisualStyleBackColor = true;
			// 
			// __optCreateNotes
			// 
			this.@__optCreateNotes.Location = new System.Drawing.Point(39, 165);
			this.@__optCreateNotes.Name = "__optCreateNotes";
			this.@__optCreateNotes.Size = new System.Drawing.Size(414, 31);
			this.@__optCreateNotes.TabIndex = 5;
			this.@__optCreateNotes.Text = "Downloads tools so you can easily make your own package or distribution";
			// 
			// MainActionPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Name = "MainActionPage";
			this.@__banner.ResumeLayout(false);
			this.@__content.ResumeLayout(false);
			this.@__content.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RadioButton __optInstallRad;
		private System.Windows.Forms.Label __optInstallNotes;
		private System.Windows.Forms.Label __optCreateNotes;
		private System.Windows.Forms.RadioButton __optCreateRad;
		private System.Windows.Forms.Label __optUndoNotes;
		private System.Windows.Forms.RadioButton __optUndo;
	}
}
