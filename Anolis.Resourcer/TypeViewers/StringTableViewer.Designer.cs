namespace Anolis.Resourcer.TypeViewers {
	partial class StringTableViewer {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.@__fdlg = new System.Windows.Forms.FontDialog();
			this.@__dgv = new System.Windows.Forms.DataGridView();
			this.@__gvColId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.@__gvColString = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.@__dgv)).BeginInit();
			this.SuspendLayout();
			// 
			// __fdlg
			// 
			this.@__fdlg.FontMustExist = true;
			this.@__fdlg.ShowApply = true;
			this.@__fdlg.ShowEffects = false;
			// 
			// __dgv
			// 
			this.@__dgv.AllowUserToAddRows = false;
			this.@__dgv.AllowUserToDeleteRows = false;
			this.@__dgv.AllowUserToOrderColumns = true;
			this.@__dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.@__dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.@__gvColId,
            this.@__gvColString});
			this.@__dgv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__dgv.Location = new System.Drawing.Point(0, 0);
			this.@__dgv.Name = "__dgv";
			this.@__dgv.ReadOnly = true;
			this.@__dgv.Size = new System.Drawing.Size(644, 447);
			this.@__dgv.TabIndex = 0;
			// 
			// __gvColId
			// 
			this.@__gvColId.HeaderText = "ID";
			this.@__gvColId.Name = "__gvColId";
			this.@__gvColId.ReadOnly = true;
			this.@__gvColId.Width = 75;
			// 
			// __gvColString
			// 
			this.@__gvColString.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.@__gvColString.DefaultCellStyle = dataGridViewCellStyle1;
			this.@__gvColString.HeaderText = "String";
			this.@__gvColString.Name = "__gvColString";
			this.@__gvColString.ReadOnly = true;
			// 
			// StringTableViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.@__dgv);
			this.Name = "StringTableViewer";
			this.Size = new System.Drawing.Size(644, 447);
			((System.ComponentModel.ISupportInitialize)(this.@__dgv)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FontDialog __fdlg;
		private System.Windows.Forms.DataGridView __dgv;
		private System.Windows.Forms.DataGridViewTextBoxColumn __gvColId;
		private System.Windows.Forms.DataGridViewTextBoxColumn __gvColString;
	}
}
