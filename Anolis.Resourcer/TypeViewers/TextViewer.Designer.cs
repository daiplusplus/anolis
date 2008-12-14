namespace Anolis.Resourcer.TypeViewers {
	partial class TextViewer {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextViewer));
			this.@__tools = new System.Windows.Forms.ToolStrip();
			this.@__toolsEncodingLbl = new System.Windows.Forms.ToolStripLabel();
			this.@__toolsEncoding = new System.Windows.Forms.ToolStripDropDownButton();
			this.aSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uTF7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uTF8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uTF16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uTF32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsBom = new System.Windows.Forms.ToolStripButton();
			this.@__toolsEndian = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.@__toolsFont = new System.Windows.Forms.ToolStripButton();
			this.@__text = new System.Windows.Forms.TextBox();
			this.@__fdlg = new System.Windows.Forms.FontDialog();
			this.@__tools.SuspendLayout();
			this.SuspendLayout();
			// 
			// __tools
			// 
			this.@__tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__toolsEncodingLbl,
            this.@__toolsEncoding,
            this.@__toolsBom,
            this.@__toolsEndian,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.@__toolsFont});
			this.@__tools.Location = new System.Drawing.Point(0, 0);
			this.@__tools.Name = "__tools";
			this.@__tools.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
			this.@__tools.Size = new System.Drawing.Size(644, 25);
			this.@__tools.TabIndex = 2;
			this.@__tools.Text = "toolStrip1";
			// 
			// __toolsEncodingLbl
			// 
			this.@__toolsEncodingLbl.Name = "__toolsEncodingLbl";
			this.@__toolsEncodingLbl.Size = new System.Drawing.Size(54, 22);
			this.@__toolsEncodingLbl.Text = "Encoding:";
			// 
			// __toolsEncoding
			// 
			this.@__toolsEncoding.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.@__toolsEncoding.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aSCIIToolStripMenuItem,
            this.uTF7ToolStripMenuItem,
            this.uTF8ToolStripMenuItem,
            this.uTF16ToolStripMenuItem,
            this.uTF32ToolStripMenuItem});
			this.@__toolsEncoding.Image = ((System.Drawing.Image)(resources.GetObject("__toolsEncoding.Image")));
			this.@__toolsEncoding.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsEncoding.Name = "__toolsEncoding";
			this.@__toolsEncoding.Size = new System.Drawing.Size(49, 22);
			this.@__toolsEncoding.Text = "UTF-8";
			// 
			// aSCIIToolStripMenuItem
			// 
			this.aSCIIToolStripMenuItem.Name = "aSCIIToolStripMenuItem";
			this.aSCIIToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.aSCIIToolStripMenuItem.Text = "ASCII";
			// 
			// uTF7ToolStripMenuItem
			// 
			this.uTF7ToolStripMenuItem.Name = "uTF7ToolStripMenuItem";
			this.uTF7ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.uTF7ToolStripMenuItem.Text = "UTF-7";
			// 
			// uTF8ToolStripMenuItem
			// 
			this.uTF8ToolStripMenuItem.Name = "uTF8ToolStripMenuItem";
			this.uTF8ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.uTF8ToolStripMenuItem.Text = "UTF-8";
			// 
			// uTF16ToolStripMenuItem
			// 
			this.uTF16ToolStripMenuItem.Name = "uTF16ToolStripMenuItem";
			this.uTF16ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.uTF16ToolStripMenuItem.Text = "UTF-16";
			// 
			// uTF32ToolStripMenuItem
			// 
			this.uTF32ToolStripMenuItem.Name = "uTF32ToolStripMenuItem";
			this.uTF32ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.uTF32ToolStripMenuItem.Text = "UTF-32";
			// 
			// __toolsBom
			// 
			this.@__toolsBom.CheckOnClick = true;
			this.@__toolsBom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__toolsBom.Image = ((System.Drawing.Image)(resources.GetObject("__toolsBom.Image")));
			this.@__toolsBom.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsBom.Name = "__toolsBom";
			this.@__toolsBom.Size = new System.Drawing.Size(23, 22);
			this.@__toolsBom.Text = "Byte Order Mark";
			this.@__toolsBom.ToolTipText = "Use Byte Order Mark (BOM) if present";
			// 
			// __toolsEndian
			// 
			this.@__toolsEndian.CheckOnClick = true;
			this.@__toolsEndian.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__toolsEndian.Image = ((System.Drawing.Image)(resources.GetObject("__toolsEndian.Image")));
			this.@__toolsEndian.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsEndian.Name = "__toolsEndian";
			this.@__toolsEndian.Size = new System.Drawing.Size(23, 22);
			this.@__toolsEndian.Text = "Unicode Big-Endian";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(82, 22);
			this.toolStripButton1.Text = "Word Wrap";
			// 
			// __toolsFont
			// 
			this.@__toolsFont.Image = ((System.Drawing.Image)(resources.GetObject("__toolsFont.Image")));
			this.@__toolsFont.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsFont.Name = "__toolsFont";
			this.@__toolsFont.Size = new System.Drawing.Size(49, 22);
			this.@__toolsFont.Text = "Font";
			this.@__toolsFont.Click += new System.EventHandler(this.@__toolsFont_Click);
			// 
			// __text
			// 
			this.@__text.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__text.Location = new System.Drawing.Point(0, 25);
			this.@__text.Multiline = true;
			this.@__text.Name = "__text";
			this.@__text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.@__text.Size = new System.Drawing.Size(644, 422);
			this.@__text.TabIndex = 3;
			// 
			// __fdlg
			// 
			this.@__fdlg.FontMustExist = true;
			this.@__fdlg.ShowApply = true;
			this.@__fdlg.ShowEffects = false;
			// 
			// TextViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@__text);
			this.Controls.Add(this.@__tools);
			this.Name = "TextViewer";
			this.Size = new System.Drawing.Size(644, 447);
			this.@__tools.ResumeLayout(false);
			this.@__tools.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip __tools;
		private System.Windows.Forms.ToolStripLabel __toolsEncodingLbl;
		private System.Windows.Forms.ToolStripDropDownButton __toolsEncoding;
		private System.Windows.Forms.ToolStripMenuItem aSCIIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uTF7ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uTF8ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uTF16ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uTF32ToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton __toolsBom;
		private System.Windows.Forms.ToolStripButton __toolsEndian;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.TextBox __text;
		private System.Windows.Forms.ToolStripButton __toolsFont;
		private System.Windows.Forms.FontDialog __fdlg;
	}
}
