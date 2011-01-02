namespace Anolis.Tools.PEInfo {
	partial class PEView {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.@__sections = new Anolis.Tools.PEInfo.SectionsChart();
			this.SuspendLayout();
			// 
			// __sections
			// 
			this.@__sections.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__sections.ImageLength = ((ulong)(0ul));
			this.@__sections.Location = new System.Drawing.Point(0, 0);
			this.@__sections.Name = "__sections";
			this.@__sections.Size = new System.Drawing.Size(586, 493);
			this.@__sections.TabIndex = 0;
			// 
			// PEView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__sections);
			this.Name = "PEView";
			this.Size = new System.Drawing.Size(586, 493);
			this.ResumeLayout(false);

		}

		#endregion

		private SectionsChart __sections;
	}
}
