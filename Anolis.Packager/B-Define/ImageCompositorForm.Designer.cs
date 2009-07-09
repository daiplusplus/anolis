namespace Anolis.Packager {
	partial class ImageCompositorForm {
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
			this.@__image = new System.Windows.Forms.PictureBox();
			this.@__layers = new System.Windows.Forms.ListBox();
			this.@__layerAdd = new System.Windows.Forms.Button();
			this.@__layerRemove = new System.Windows.Forms.Button();
			this.@__layersMoveUp = new System.Windows.Forms.Button();
			this.@__layersMoveDown = new System.Windows.Forms.Button();
			this.@__layFileNameLbl = new System.Windows.Forms.Label();
			this.@__layFileName = new System.Windows.Forms.TextBox();
			this.@__layBrowse = new System.Windows.Forms.Button();
			this.@__layCoordsLbl = new System.Windows.Forms.Label();
			this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
			this.@__layUpdate = new System.Windows.Forms.Button();
			this.@__ofd = new System.Windows.Forms.OpenFileDialog();
			this.@__layerGrp = new System.Windows.Forms.GroupBox();
			this.@__layCancel = new System.Windows.Forms.Button();
			this.@__layersGrp = new System.Windows.Forms.GroupBox();
			this.@__compLbl = new System.Windows.Forms.Label();
			this.@__comp = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.@__image)).BeginInit();
			this.@__layerGrp.SuspendLayout();
			this.@__layersGrp.SuspendLayout();
			this.SuspendLayout();
			// 
			// __image
			// 
			this.@__image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__image.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.@__image.Location = new System.Drawing.Point(12, 12);
			this.@__image.Name = "__image";
			this.@__image.Size = new System.Drawing.Size(429, 418);
			this.@__image.TabIndex = 0;
			this.@__image.TabStop = false;
			// 
			// __layers
			// 
			this.@__layers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__layers.FormattingEnabled = true;
			this.@__layers.IntegralHeight = false;
			this.@__layers.Location = new System.Drawing.Point(11, 19);
			this.@__layers.Name = "__layers";
			this.@__layers.Size = new System.Drawing.Size(160, 251);
			this.@__layers.TabIndex = 1;
			// 
			// __layerAdd
			// 
			this.@__layerAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__layerAdd.Location = new System.Drawing.Point(11, 276);
			this.@__layerAdd.Name = "__layerAdd";
			this.@__layerAdd.Size = new System.Drawing.Size(91, 23);
			this.@__layerAdd.TabIndex = 3;
			this.@__layerAdd.Text = "Add Layer";
			this.@__layerAdd.UseVisualStyleBackColor = true;
			// 
			// __layerRemove
			// 
			this.@__layerRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__layerRemove.Location = new System.Drawing.Point(108, 276);
			this.@__layerRemove.Name = "__layerRemove";
			this.@__layerRemove.Size = new System.Drawing.Size(79, 23);
			this.@__layerRemove.TabIndex = 4;
			this.@__layerRemove.Text = "Remove";
			this.@__layerRemove.UseVisualStyleBackColor = true;
			// 
			// __layersMoveUp
			// 
			this.@__layersMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__layersMoveUp.Font = new System.Drawing.Font("Marlett", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.@__layersMoveUp.Location = new System.Drawing.Point(177, 114);
			this.@__layersMoveUp.Name = "__layersMoveUp";
			this.@__layersMoveUp.Size = new System.Drawing.Size(21, 23);
			this.@__layersMoveUp.TabIndex = 5;
			this.@__layersMoveUp.Text = "t";
			this.@__layersMoveUp.UseVisualStyleBackColor = true;
			// 
			// __layersMoveDown
			// 
			this.@__layersMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__layersMoveDown.Font = new System.Drawing.Font("Marlett", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.@__layersMoveDown.Location = new System.Drawing.Point(177, 143);
			this.@__layersMoveDown.Name = "__layersMoveDown";
			this.@__layersMoveDown.Size = new System.Drawing.Size(21, 23);
			this.@__layersMoveDown.TabIndex = 6;
			this.@__layersMoveDown.Text = "u";
			this.@__layersMoveDown.UseVisualStyleBackColor = true;
			// 
			// __layFileNameLbl
			// 
			this.@__layFileNameLbl.Location = new System.Drawing.Point(8, 24);
			this.@__layFileNameLbl.Name = "__layFileNameLbl";
			this.@__layFileNameLbl.Size = new System.Drawing.Size(63, 23);
			this.@__layFileNameLbl.TabIndex = 7;
			this.@__layFileNameLbl.Text = "Filename";
			this.@__layFileNameLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// __layFileName
			// 
			this.@__layFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.@__layFileName.Location = new System.Drawing.Point(77, 21);
			this.@__layFileName.Name = "__layFileName";
			this.@__layFileName.Size = new System.Drawing.Size(80, 20);
			this.@__layFileName.TabIndex = 8;
			// 
			// __layBrowse
			// 
			this.@__layBrowse.Location = new System.Drawing.Point(163, 19);
			this.@__layBrowse.Name = "__layBrowse";
			this.@__layBrowse.Size = new System.Drawing.Size(28, 23);
			this.@__layBrowse.TabIndex = 9;
			this.@__layBrowse.Text = "...";
			this.@__layBrowse.UseVisualStyleBackColor = true;
			// 
			// __layCoordsLbl
			// 
			this.@__layCoordsLbl.Location = new System.Drawing.Point(8, 51);
			this.@__layCoordsLbl.Name = "__layCoordsLbl";
			this.@__layCoordsLbl.Size = new System.Drawing.Size(63, 23);
			this.@__layCoordsLbl.TabIndex = 10;
			this.@__layCoordsLbl.Text = "Coordinates";
			this.@__layCoordsLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// maskedTextBox1
			// 
			this.maskedTextBox1.Location = new System.Drawing.Point(77, 48);
			this.maskedTextBox1.Mask = "0999\\,0999";
			this.maskedTextBox1.Name = "maskedTextBox1";
			this.maskedTextBox1.Size = new System.Drawing.Size(114, 20);
			this.maskedTextBox1.TabIndex = 11;
			// 
			// __layUpdate
			// 
			this.@__layUpdate.Location = new System.Drawing.Point(35, 74);
			this.@__layUpdate.Name = "__layUpdate";
			this.@__layUpdate.Size = new System.Drawing.Size(75, 23);
			this.@__layUpdate.TabIndex = 12;
			this.@__layUpdate.Text = "Update";
			this.@__layUpdate.UseVisualStyleBackColor = true;
			// 
			// __ofd
			// 
			this.@__ofd.Filter = "Bitmap Image (*.bmp)|*.bmp";
			// 
			// __layerGrp
			// 
			this.@__layerGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__layerGrp.Controls.Add(this.@__layCancel);
			this.@__layerGrp.Controls.Add(this.@__layFileNameLbl);
			this.@__layerGrp.Controls.Add(this.@__layUpdate);
			this.@__layerGrp.Controls.Add(this.@__layFileName);
			this.@__layerGrp.Controls.Add(this.maskedTextBox1);
			this.@__layerGrp.Controls.Add(this.@__layBrowse);
			this.@__layerGrp.Controls.Add(this.@__layCoordsLbl);
			this.@__layerGrp.Location = new System.Drawing.Point(447, 328);
			this.@__layerGrp.Name = "__layerGrp";
			this.@__layerGrp.Size = new System.Drawing.Size(204, 102);
			this.@__layerGrp.TabIndex = 13;
			this.@__layerGrp.TabStop = false;
			this.@__layerGrp.Text = "Layer Properties";
			// 
			// __layCancel
			// 
			this.@__layCancel.Location = new System.Drawing.Point(116, 74);
			this.@__layCancel.Name = "__layCancel";
			this.@__layCancel.Size = new System.Drawing.Size(75, 23);
			this.@__layCancel.TabIndex = 13;
			this.@__layCancel.Text = "Cancel";
			this.@__layCancel.UseVisualStyleBackColor = true;
			// 
			// __layersGrp
			// 
			this.@__layersGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__layersGrp.Controls.Add(this.@__layers);
			this.@__layersGrp.Controls.Add(this.@__layerAdd);
			this.@__layersGrp.Controls.Add(this.@__layersMoveDown);
			this.@__layersGrp.Controls.Add(this.@__layerRemove);
			this.@__layersGrp.Controls.Add(this.@__layersMoveUp);
			this.@__layersGrp.Location = new System.Drawing.Point(447, 12);
			this.@__layersGrp.Name = "__layersGrp";
			this.@__layersGrp.Size = new System.Drawing.Size(204, 310);
			this.@__layersGrp.TabIndex = 14;
			this.@__layersGrp.TabStop = false;
			this.@__layersGrp.Text = "Layers";
			// 
			// __compLbl
			// 
			this.@__compLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__compLbl.AutoSize = true;
			this.@__compLbl.Location = new System.Drawing.Point(12, 443);
			this.@__compLbl.Name = "__compLbl";
			this.@__compLbl.Size = new System.Drawing.Size(118, 13);
			this.@__compLbl.TabIndex = 15;
			this.@__compLbl.Text = "Composition Expression";
			// 
			// __comp
			// 
			this.@__comp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__comp.BackColor = System.Drawing.SystemColors.Window;
			this.@__comp.Location = new System.Drawing.Point(136, 440);
			this.@__comp.Name = "__comp";
			this.@__comp.ReadOnly = true;
			this.@__comp.Size = new System.Drawing.Size(515, 20);
			this.@__comp.TabIndex = 16;
			// 
			// ImageCompositorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(662, 472);
			this.Controls.Add(this.@__comp);
			this.Controls.Add(this.@__compLbl);
			this.Controls.Add(this.@__layersGrp);
			this.Controls.Add(this.@__layerGrp);
			this.Controls.Add(this.@__image);
			this.Name = "ImageCompositorForm";
			this.Text = "Image Compositor";
			((System.ComponentModel.ISupportInitialize)(this.@__image)).EndInit();
			this.@__layerGrp.ResumeLayout(false);
			this.@__layerGrp.PerformLayout();
			this.@__layersGrp.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox __image;
		private System.Windows.Forms.ListBox __layers;
		private System.Windows.Forms.Button __layerAdd;
		private System.Windows.Forms.Button __layerRemove;
		private System.Windows.Forms.Button __layersMoveUp;
		private System.Windows.Forms.Button __layersMoveDown;
		private System.Windows.Forms.Label __layFileNameLbl;
		private System.Windows.Forms.TextBox __layFileName;
		private System.Windows.Forms.Button __layBrowse;
		private System.Windows.Forms.Label __layCoordsLbl;
		private System.Windows.Forms.MaskedTextBox maskedTextBox1;
		private System.Windows.Forms.Button __layUpdate;
		private System.Windows.Forms.OpenFileDialog __ofd;
		private System.Windows.Forms.GroupBox __layerGrp;
		private System.Windows.Forms.Button __layCancel;
		private System.Windows.Forms.GroupBox __layersGrp;
		private System.Windows.Forms.Label __compLbl;
		private System.Windows.Forms.TextBox __comp;
	}
}