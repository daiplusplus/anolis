namespace Anolis.Resourcer {
	partial class FactoryOptionsForm {
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
			this.@__ok = new System.Windows.Forms.Button();
			this.@__grid = new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.@__ok.Location = new System.Drawing.Point(299, 283);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 0;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// __grid
			// 
			this.@__grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__grid.Location = new System.Drawing.Point(9, 9);
			this.@__grid.Margin = new System.Windows.Forms.Padding(0);
			this.@__grid.Name = "__grid";
			this.@__grid.Size = new System.Drawing.Size(368, 268);
			this.@__grid.TabIndex = 1;
			// 
			// FactoryOptionsForm
			// 
			this.AcceptButton = this.@__ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(386, 318);
			this.ControlBox = false;
			this.Controls.Add(this.@__grid);
			this.Controls.Add(this.@__ok);
			this.Name = "FactoryOptionsForm";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Factory Options";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button __ok;
		private System.Windows.Forms.PropertyGrid __grid;
	}
}