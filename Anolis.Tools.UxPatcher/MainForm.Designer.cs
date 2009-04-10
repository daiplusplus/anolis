namespace Anolis.Tools.UxPatcher {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.@__patch = new System.Windows.Forms.Button();
			this.@__infoLbl = new System.Windows.Forms.Label();
			this.@__aboutAnolis = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.@__linkAnolis = new System.Windows.Forms.LinkLabel();
			this.@__linkRafael = new System.Windows.Forms.LinkLabel();
			this.@__systemRestore = new System.Windows.Forms.CheckBox();
			this.@__links = new System.Windows.Forms.Label();
			this.@__tabs = new System.Windows.Forms.TabControl();
			this.@__tabAbout = new System.Windows.Forms.TabPage();
			this.@__tabPatch = new System.Windows.Forms.TabPage();
			this.@__restart = new System.Windows.Forms.Button();
			this.@__status = new System.Windows.Forms.Label();
			this.@__statusLbl = new System.Windows.Forms.Label();
			this.@__patchMatch = new System.Windows.Forms.TextBox();
			this.@__patchMatchLbl = new System.Windows.Forms.Label();
			this.@__system = new System.Windows.Forms.TextBox();
			this.@__systemLbl = new System.Windows.Forms.Label();
			this.@__bw = new System.ComponentModel.BackgroundWorker();
			this.@__tabs.SuspendLayout();
			this.@__tabAbout.SuspendLayout();
			this.@__tabPatch.SuspendLayout();
			this.SuspendLayout();
			// 
			// __patch
			// 
			this.@__patch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__patch.Location = new System.Drawing.Point(247, 111);
			this.@__patch.Name = "__patch";
			this.@__patch.Size = new System.Drawing.Size(75, 23);
			this.@__patch.TabIndex = 1;
			this.@__patch.Text = "Patch";
			this.@__patch.UseVisualStyleBackColor = true;
			// 
			// __infoLbl
			// 
			this.@__infoLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__infoLbl.Location = new System.Drawing.Point(3, 3);
			this.@__infoLbl.Name = "__infoLbl";
			this.@__infoLbl.Size = new System.Drawing.Size(327, 187);
			this.@__infoLbl.TabIndex = 2;
			this.@__infoLbl.Text = resources.GetString("__infoLbl.Text");
			// 
			// __aboutAnolis
			// 
			this.@__aboutAnolis.AutoSize = true;
			this.@__aboutAnolis.Dock = System.Windows.Forms.DockStyle.Top;
			this.@__aboutAnolis.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
			this.@__aboutAnolis.Location = new System.Drawing.Point(3, 3);
			this.@__aboutAnolis.Name = "__aboutAnolis";
			this.@__aboutAnolis.Padding = new System.Windows.Forms.Padding(3);
			this.@__aboutAnolis.Size = new System.Drawing.Size(248, 29);
			this.@__aboutAnolis.TabIndex = 20;
			this.@__aboutAnolis.Text = "Anolis UxTheme Patcher";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 215);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 26);
			this.label1.TabIndex = 21;
			this.label1.Text = "Anolis\r\nRafael Rivera";
			// 
			// __linkAnolis
			// 
			this.@__linkAnolis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__linkAnolis.AutoSize = true;
			this.@__linkAnolis.Location = new System.Drawing.Point(260, 215);
			this.@__linkAnolis.Name = "__linkAnolis";
			this.@__linkAnolis.Size = new System.Drawing.Size(70, 13);
			this.@__linkAnolis.TabIndex = 22;
			this.@__linkAnolis.TabStop = true;
			this.@__linkAnolis.Text = "http://anol.is";
			// 
			// __linkRafael
			// 
			this.@__linkRafael.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__linkRafael.AutoSize = true;
			this.@__linkRafael.Location = new System.Drawing.Point(171, 228);
			this.@__linkRafael.Name = "__linkRafael";
			this.@__linkRafael.Size = new System.Drawing.Size(159, 13);
			this.@__linkRafael.TabIndex = 23;
			this.@__linkRafael.TabStop = true;
			this.@__linkRafael.Text = "http://www.withinwindows.com";
			// 
			// __systemRestore
			// 
			this.@__systemRestore.AutoSize = true;
			this.@__systemRestore.Checked = true;
			this.@__systemRestore.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__systemRestore.Location = new System.Drawing.Point(16, 75);
			this.@__systemRestore.Name = "__systemRestore";
			this.@__systemRestore.Size = new System.Drawing.Size(246, 17);
			this.@__systemRestore.TabIndex = 24;
			this.@__systemRestore.Text = "Create System Restore Point (Recommended)";
			this.@__systemRestore.UseVisualStyleBackColor = true;
			// 
			// __links
			// 
			this.@__links.AutoSize = true;
			this.@__links.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__links.Location = new System.Drawing.Point(3, 197);
			this.@__links.Name = "__links";
			this.@__links.Size = new System.Drawing.Size(36, 13);
			this.@__links.TabIndex = 25;
			this.@__links.Text = "Links";
			// 
			// __tabs
			// 
			this.@__tabs.Controls.Add(this.@__tabAbout);
			this.@__tabs.Controls.Add(this.@__tabPatch);
			this.@__tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__tabs.Location = new System.Drawing.Point(3, 32);
			this.@__tabs.Name = "__tabs";
			this.@__tabs.SelectedIndex = 0;
			this.@__tabs.Size = new System.Drawing.Size(344, 280);
			this.@__tabs.TabIndex = 26;
			// 
			// __tabAbout
			// 
			this.@__tabAbout.AutoScroll = true;
			this.@__tabAbout.Controls.Add(this.@__infoLbl);
			this.@__tabAbout.Controls.Add(this.@__links);
			this.@__tabAbout.Controls.Add(this.label1);
			this.@__tabAbout.Controls.Add(this.@__linkAnolis);
			this.@__tabAbout.Controls.Add(this.@__linkRafael);
			this.@__tabAbout.Location = new System.Drawing.Point(4, 22);
			this.@__tabAbout.Name = "__tabAbout";
			this.@__tabAbout.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabAbout.Size = new System.Drawing.Size(336, 254);
			this.@__tabAbout.TabIndex = 0;
			this.@__tabAbout.Text = "About";
			this.@__tabAbout.UseVisualStyleBackColor = true;
			// 
			// __tabPatch
			// 
			this.@__tabPatch.Controls.Add(this.@__restart);
			this.@__tabPatch.Controls.Add(this.@__status);
			this.@__tabPatch.Controls.Add(this.@__statusLbl);
			this.@__tabPatch.Controls.Add(this.@__patchMatch);
			this.@__tabPatch.Controls.Add(this.@__patchMatchLbl);
			this.@__tabPatch.Controls.Add(this.@__system);
			this.@__tabPatch.Controls.Add(this.@__systemLbl);
			this.@__tabPatch.Controls.Add(this.@__systemRestore);
			this.@__tabPatch.Controls.Add(this.@__patch);
			this.@__tabPatch.Location = new System.Drawing.Point(4, 22);
			this.@__tabPatch.Name = "__tabPatch";
			this.@__tabPatch.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabPatch.Size = new System.Drawing.Size(336, 254);
			this.@__tabPatch.TabIndex = 1;
			this.@__tabPatch.Text = "Patch";
			this.@__tabPatch.UseVisualStyleBackColor = true;
			// 
			// __restart
			// 
			this.@__restart.Enabled = false;
			this.@__restart.Location = new System.Drawing.Point(203, 140);
			this.@__restart.Name = "__restart";
			this.@__restart.Size = new System.Drawing.Size(119, 23);
			this.@__restart.TabIndex = 31;
			this.@__restart.Text = "Restart Computer";
			this.@__restart.UseVisualStyleBackColor = true;
			// 
			// __status
			// 
			this.@__status.AutoSize = true;
			this.@__status.Location = new System.Drawing.Point(61, 116);
			this.@__status.Name = "__status";
			this.@__status.Size = new System.Drawing.Size(38, 13);
			this.@__status.TabIndex = 30;
			this.@__status.Text = "Ready";
			// 
			// __statusLbl
			// 
			this.@__statusLbl.AutoSize = true;
			this.@__statusLbl.Location = new System.Drawing.Point(13, 116);
			this.@__statusLbl.Name = "__statusLbl";
			this.@__statusLbl.Size = new System.Drawing.Size(42, 13);
			this.@__statusLbl.TabIndex = 29;
			this.@__statusLbl.Text = "Status:";
			// 
			// __patchMatch
			// 
			this.@__patchMatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__patchMatch.Location = new System.Drawing.Point(99, 39);
			this.@__patchMatch.Name = "__patchMatch";
			this.@__patchMatch.ReadOnly = true;
			this.@__patchMatch.Size = new System.Drawing.Size(223, 21);
			this.@__patchMatch.TabIndex = 28;
			// 
			// __patchMatchLbl
			// 
			this.@__patchMatchLbl.AutoSize = true;
			this.@__patchMatchLbl.Location = new System.Drawing.Point(13, 42);
			this.@__patchMatchLbl.Name = "__patchMatchLbl";
			this.@__patchMatchLbl.Size = new System.Drawing.Size(80, 13);
			this.@__patchMatchLbl.TabIndex = 27;
			this.@__patchMatchLbl.Text = "Matching Patch";
			// 
			// __system
			// 
			this.@__system.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__system.Location = new System.Drawing.Point(99, 12);
			this.@__system.Name = "__system";
			this.@__system.ReadOnly = true;
			this.@__system.Size = new System.Drawing.Size(223, 21);
			this.@__system.TabIndex = 26;
			// 
			// __systemLbl
			// 
			this.@__systemLbl.AutoSize = true;
			this.@__systemLbl.Location = new System.Drawing.Point(26, 15);
			this.@__systemLbl.Name = "__systemLbl";
			this.@__systemLbl.Size = new System.Drawing.Size(67, 13);
			this.@__systemLbl.TabIndex = 25;
			this.@__systemLbl.Text = "Your System";
			// 
			// MainForm
			// 
			this.AcceptButton = this.@__patch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(350, 315);
			this.Controls.Add(this.@__tabs);
			this.Controls.Add(this.@__aboutAnolis);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.ShowIcon = false;
			this.Text = "Anolis NT5.x UxTheme Patcher";
			this.@__tabs.ResumeLayout(false);
			this.@__tabAbout.ResumeLayout(false);
			this.@__tabAbout.PerformLayout();
			this.@__tabPatch.ResumeLayout(false);
			this.@__tabPatch.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button __patch;
		private System.Windows.Forms.Label __infoLbl;
		private System.Windows.Forms.Label __aboutAnolis;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel __linkAnolis;
		private System.Windows.Forms.LinkLabel __linkRafael;
		private System.Windows.Forms.CheckBox __systemRestore;
		private System.Windows.Forms.Label __links;
		private System.Windows.Forms.TabControl __tabs;
		private System.Windows.Forms.TabPage __tabAbout;
		private System.Windows.Forms.TabPage __tabPatch;
		private System.Windows.Forms.TextBox __patchMatch;
		private System.Windows.Forms.Label __patchMatchLbl;
		private System.Windows.Forms.TextBox __system;
		private System.Windows.Forms.Label __systemLbl;
		private System.Windows.Forms.Label __status;
		private System.Windows.Forms.Label __statusLbl;
		private System.Windows.Forms.Button __restart;
		private System.ComponentModel.BackgroundWorker __bw;
	}
}

