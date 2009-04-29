namespace Anolis.Packager {
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
			this.@__tasksGrp = new System.Windows.Forms.GroupBox();
			this.@__icoed = new System.Windows.Forms.Button();
			this.@__import = new System.Windows.Forms.Button();
			this.@__distro = new System.Windows.Forms.Button();
			this.@__compre = new System.Windows.Forms.Button();
			this.@__define = new System.Windows.Forms.Button();
			this.@__tasksGrp.SuspendLayout();
			this.SuspendLayout();
			// 
			// __tasksGrp
			// 
			this.@__tasksGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__tasksGrp.Controls.Add(this.@__icoed);
			this.@__tasksGrp.Controls.Add(this.@__import);
			this.@__tasksGrp.Controls.Add(this.@__distro);
			this.@__tasksGrp.Controls.Add(this.@__compre);
			this.@__tasksGrp.Controls.Add(this.@__define);
			this.@__tasksGrp.Location = new System.Drawing.Point(12, 12);
			this.@__tasksGrp.Name = "__tasksGrp";
			this.@__tasksGrp.Size = new System.Drawing.Size(425, 120);
			this.@__tasksGrp.TabIndex = 0;
			this.@__tasksGrp.TabStop = false;
			this.@__tasksGrp.Text = "Select a Task";
			// 
			// __icoed
			// 
			this.@__icoed.Image = global::Anolis.Packager.Resources.IconEdit;
			this.@__icoed.Location = new System.Drawing.Point(89, 19);
			this.@__icoed.Name = "__icoed";
			this.@__icoed.Size = new System.Drawing.Size(77, 92);
			this.@__icoed.TabIndex = 4;
			this.@__icoed.Text = "Launch Icon Editor";
			this.@__icoed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__icoed.UseVisualStyleBackColor = true;
			// 
			// __import
			// 
			this.@__import.Image = global::Anolis.Packager.Resources.Import;
			this.@__import.Location = new System.Drawing.Point(6, 19);
			this.@__import.Name = "__import";
			this.@__import.Size = new System.Drawing.Size(77, 92);
			this.@__import.TabIndex = 0;
			this.@__import.Text = "Import NSI/XIS";
			this.@__import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__import.UseVisualStyleBackColor = true;
			// 
			// __distro
			// 
			this.@__distro.Image = global::Anolis.Packager.Resources.Distro;
			this.@__distro.Location = new System.Drawing.Point(338, 19);
			this.@__distro.Name = "__distro";
			this.@__distro.Size = new System.Drawing.Size(77, 92);
			this.@__distro.TabIndex = 3;
			this.@__distro.Text = "Create Distribution";
			this.@__distro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__distro.UseVisualStyleBackColor = true;
			// 
			// __compre
			// 
			this.@__compre.Image = global::Anolis.Packager.Resources.Compress;
			this.@__compre.Location = new System.Drawing.Point(255, 19);
			this.@__compre.Name = "__compre";
			this.@__compre.Size = new System.Drawing.Size(77, 92);
			this.@__compre.TabIndex = 2;
			this.@__compre.Text = "Compress Package";
			this.@__compre.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__compre.UseVisualStyleBackColor = true;
			// 
			// __define
			// 
			this.@__define.Image = global::Anolis.Packager.Resources.Create;
			this.@__define.Location = new System.Drawing.Point(172, 19);
			this.@__define.Name = "__define";
			this.@__define.Size = new System.Drawing.Size(77, 92);
			this.@__define.TabIndex = 1;
			this.@__define.Text = "Define Package";
			this.@__define.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.@__define.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(449, 144);
			this.Controls.Add(this.@__tasksGrp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "Anolis Packager";
			this.@__tasksGrp.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox __tasksGrp;
		private System.Windows.Forms.Button __define;
		private System.Windows.Forms.Button __distro;
		private System.Windows.Forms.Button __compre;
		private System.Windows.Forms.Button __import;
		private System.Windows.Forms.Button __icoed;
	}
}