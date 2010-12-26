namespace Anolis.Resourcer {
	partial class FindForm {
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
			this.label1 = new System.Windows.Forms.Label();
			this.@__text = new System.Windows.Forms.TextBox();
			this.@__findNext = new System.Windows.Forms.Button();
			this.@__cancel = new System.Windows.Forms.Button();
			this.@__matchCase = new System.Windows.Forms.CheckBox();
			this.@__searchNames = new System.Windows.Forms.CheckBox();
			this.@__searchContent = new System.Windows.Forms.CheckBox();
			this.@__searchVisual = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Find what:";
			// 
			// __text
			// 
			this.@__text.Location = new System.Drawing.Point(74, 6);
			this.@__text.Name = "__text";
			this.@__text.Size = new System.Drawing.Size(254, 20);
			this.@__text.TabIndex = 0;
			// 
			// __findNext
			// 
			this.@__findNext.Location = new System.Drawing.Point(334, 4);
			this.@__findNext.Name = "__findNext";
			this.@__findNext.Size = new System.Drawing.Size(75, 23);
			this.@__findNext.TabIndex = 5;
			this.@__findNext.Text = "Find Next";
			this.@__findNext.UseVisualStyleBackColor = true;
			// 
			// __cancel
			// 
			this.@__cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__cancel.Location = new System.Drawing.Point(334, 33);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(75, 23);
			this.@__cancel.TabIndex = 6;
			this.@__cancel.Text = "Cancel";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// __matchCase
			// 
			this.@__matchCase.AutoSize = true;
			this.@__matchCase.Location = new System.Drawing.Point(15, 39);
			this.@__matchCase.Name = "__matchCase";
			this.@__matchCase.Size = new System.Drawing.Size(82, 17);
			this.@__matchCase.TabIndex = 1;
			this.@__matchCase.Text = "Match case";
			this.@__matchCase.UseVisualStyleBackColor = true;
			// 
			// __searchNames
			// 
			this.@__searchNames.AutoSize = true;
			this.@__searchNames.Checked = true;
			this.@__searchNames.CheckState = System.Windows.Forms.CheckState.Checked;
			this.@__searchNames.Location = new System.Drawing.Point(15, 62);
			this.@__searchNames.Name = "__searchNames";
			this.@__searchNames.Size = new System.Drawing.Size(145, 17);
			this.@__searchNames.TabIndex = 2;
			this.@__searchNames.Text = "Search Resource Names";
			this.@__searchNames.UseVisualStyleBackColor = true;
			// 
			// __searchContent
			// 
			this.@__searchContent.AutoSize = true;
			this.@__searchContent.Location = new System.Drawing.Point(15, 85);
			this.@__searchContent.Name = "__searchContent";
			this.@__searchContent.Size = new System.Drawing.Size(149, 17);
			this.@__searchContent.TabIndex = 3;
			this.@__searchContent.Text = "Search Resource Content";
			this.@__searchContent.UseVisualStyleBackColor = true;
			// 
			// __searchVisual
			// 
			this.@__searchVisual.AutoSize = true;
			this.@__searchVisual.Enabled = false;
			this.@__searchVisual.Location = new System.Drawing.Point(15, 108);
			this.@__searchVisual.Name = "__searchVisual";
			this.@__searchVisual.Size = new System.Drawing.Size(290, 17);
			this.@__searchVisual.TabIndex = 4;
			this.@__searchVisual.Text = "Search content of \'visual\' resources (Not recommended)";
			this.@__searchVisual.UseVisualStyleBackColor = true;
			// 
			// FindForm
			// 
			this.AcceptButton = this.@__findNext;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__cancel;
			this.ClientSize = new System.Drawing.Size(415, 140);
			this.Controls.Add(this.@__searchVisual);
			this.Controls.Add(this.@__searchContent);
			this.Controls.Add(this.@__searchNames);
			this.Controls.Add(this.@__matchCase);
			this.Controls.Add(this.@__cancel);
			this.Controls.Add(this.@__findNext);
			this.Controls.Add(this.@__text);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FindForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox __text;
		private System.Windows.Forms.Button __findNext;
		private System.Windows.Forms.Button __cancel;
		private System.Windows.Forms.CheckBox __matchCase;
		private System.Windows.Forms.CheckBox __searchNames;
		private System.Windows.Forms.CheckBox __searchContent;
		private System.Windows.Forms.CheckBox __searchVisual;
	}
}