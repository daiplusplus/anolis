namespace Anolis.Installer.Pages {
	partial class InstallationOptionsForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallationOptionsForm));
			this.@__restore = new System.Windows.Forms.CheckBox();
			this.@__restoreDesc = new System.Windows.Forms.Label();
			this.@__lite = new System.Windows.Forms.CheckBox();
			this.@__liteDesc = new System.Windows.Forms.Label();
			this.@__ok = new System.Windows.Forms.Button();
			this.@__feedback = new System.Windows.Forms.CheckBox();
			this.@__feedbackDesc = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// __restore
			// 
			this.@__restore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__restore.Checked = true;
			this.@__restore.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__restore.Location = new System.Drawing.Point(12, 12);
			this.@__restore.Name = "__restore";
			this.@__restore.Size = new System.Drawing.Size(340, 17);
			this.@__restore.TabIndex = 0;
			this.@__restore.Text = "Create System Restore Point (Recommended)";
			this.@__restore.UseVisualStyleBackColor = true;
			// 
			// __restoreDesc
			// 
			this.@__restoreDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__restoreDesc.Location = new System.Drawing.Point(37, 32);
			this.@__restoreDesc.Name = "__restoreDesc";
			this.@__restoreDesc.Size = new System.Drawing.Size(315, 58);
			this.@__restoreDesc.TabIndex = 1;
			this.@__restoreDesc.Text = "System Restore provides an added layer of protection to the uninstaller. Note thi" +
				"s does not always protect against modifications made to non-Windows files under " +
				"Program Files";
			// 
			// __lite
			// 
			this.@__lite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__lite.Location = new System.Drawing.Point(12, 93);
			this.@__lite.Name = "__lite";
			this.@__lite.Size = new System.Drawing.Size(340, 17);
			this.@__lite.TabIndex = 2;
			this.@__lite.Text = "\"Lite\" installation for non-English language Windows";
			this.@__lite.UseVisualStyleBackColor = true;
			// 
			// __liteDesc
			// 
			this.@__liteDesc.Location = new System.Drawing.Point(37, 113);
			this.@__liteDesc.Name = "__liteDesc";
			this.@__liteDesc.Size = new System.Drawing.Size(315, 78);
			this.@__liteDesc.TabIndex = 3;
			this.@__liteDesc.Text = resources.GetString("__liteDesc.Text");
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.Location = new System.Drawing.Point(277, 273);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 5;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// __feedback
			// 
			this.@__feedback.Location = new System.Drawing.Point(12, 194);
			this.@__feedback.Name = "__feedback";
			this.@__feedback.Size = new System.Drawing.Size(340, 17);
			this.@__feedback.TabIndex = 6;
			this.@__feedback.Text = "Send Installation Feedback";
			this.@__feedback.UseVisualStyleBackColor = true;
			// 
			// __feedbackDesc
			// 
			this.@__feedbackDesc.Location = new System.Drawing.Point(37, 214);
			this.@__feedbackDesc.Name = "__feedbackDesc";
			this.@__feedbackDesc.Size = new System.Drawing.Size(315, 53);
			this.@__feedbackDesc.TabIndex = 7;
			this.@__feedbackDesc.Text = "Anonymous details of your setup experience (such as your operating system, langua" +
				"ge, and any errors) will be sent to the package\'s developers to help improve the" +
				" package in future.";
			// 
			// InstallationOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 308);
			this.ControlBox = false;
			this.Controls.Add(this.@__restore);
			this.Controls.Add(this.@__restoreDesc);
			this.Controls.Add(this.@__lite);
			this.Controls.Add(this.@__liteDesc);
			this.Controls.Add(this.@__feedback);
			this.Controls.Add(this.@__feedbackDesc);
			this.Controls.Add(this.@__ok);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "InstallationOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Advanced Installation Options";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox __restore;
		private System.Windows.Forms.Label __restoreDesc;
		private System.Windows.Forms.CheckBox __lite;
		private System.Windows.Forms.Label __liteDesc;
		private System.Windows.Forms.Button __ok;
		private System.Windows.Forms.CheckBox __feedback;
		private System.Windows.Forms.Label __feedbackDesc;
	}
}