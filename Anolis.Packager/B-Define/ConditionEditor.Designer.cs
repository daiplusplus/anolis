namespace Anolis.Packager {
	partial class ConditionEditor {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.@__expression = new System.Windows.Forms.TextBox();
			this.@__expressionLbl = new System.Windows.Forms.Label();
			this.@__resultLbl = new System.Windows.Forms.Label();
			this.@__symbolsLbl = new System.Windows.Forms.Label();
			this.@__symbols = new System.Windows.Forms.DataGridView();
			this.@__split = new System.Windows.Forms.SplitContainer();
			this.@__sColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.@__sColVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.@__result = new System.Windows.Forms.TextBox();
			this.@__eval = new System.Windows.Forms.Button();
			this.@__cancel = new System.Windows.Forms.Button();
			this.@__ok = new System.Windows.Forms.Button();
			this.@__tok = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.@__symbols)).BeginInit();
			this.@__split.Panel1.SuspendLayout();
			this.@__split.Panel2.SuspendLayout();
			this.@__split.SuspendLayout();
			this.SuspendLayout();
			// 
			// __expression
			// 
			this.@__expression.AcceptsReturn = true;
			this.@__expression.AcceptsTab = true;
			this.@__expression.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__expression.Location = new System.Drawing.Point(0, 16);
			this.@__expression.Multiline = true;
			this.@__expression.Name = "__expression";
			this.@__expression.Size = new System.Drawing.Size(210, 210);
			this.@__expression.TabIndex = 0;
			// 
			// __expressionLbl
			// 
			this.@__expressionLbl.AutoSize = true;
			this.@__expressionLbl.Location = new System.Drawing.Point(-3, 0);
			this.@__expressionLbl.Name = "__expressionLbl";
			this.@__expressionLbl.Size = new System.Drawing.Size(85, 13);
			this.@__expressionLbl.TabIndex = 1;
			this.@__expressionLbl.Text = "Input Expression";
			// 
			// __resultLbl
			// 
			this.@__resultLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__resultLbl.AutoSize = true;
			this.@__resultLbl.Location = new System.Drawing.Point(6, 238);
			this.@__resultLbl.Name = "__resultLbl";
			this.@__resultLbl.Size = new System.Drawing.Size(37, 13);
			this.@__resultLbl.TabIndex = 2;
			this.@__resultLbl.Text = "Result";
			// 
			// __symbolsLbl
			// 
			this.@__symbolsLbl.AutoSize = true;
			this.@__symbolsLbl.Location = new System.Drawing.Point(3, 0);
			this.@__symbolsLbl.Name = "__symbolsLbl";
			this.@__symbolsLbl.Size = new System.Drawing.Size(46, 13);
			this.@__symbolsLbl.TabIndex = 3;
			this.@__symbolsLbl.Text = "Symbols";
			// 
			// __symbols
			// 
			this.@__symbols.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__symbols.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.@__symbols.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.@__symbols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.@__symbols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.@__sColName,
            this.@__sColVal});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.@__symbols.DefaultCellStyle = dataGridViewCellStyle2;
			this.@__symbols.Location = new System.Drawing.Point(6, 16);
			this.@__symbols.Name = "__symbols";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.@__symbols.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.@__symbols.Size = new System.Drawing.Size(225, 210);
			this.@__symbols.TabIndex = 4;
			// 
			// __split
			// 
			this.@__split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__split.Location = new System.Drawing.Point(9, 9);
			this.@__split.Margin = new System.Windows.Forms.Padding(0);
			this.@__split.Name = "__split";
			// 
			// __split.Panel1
			// 
			this.@__split.Panel1.Controls.Add(this.@__expression);
			this.@__split.Panel1.Controls.Add(this.@__expressionLbl);
			// 
			// __split.Panel2
			// 
			this.@__split.Panel2.Controls.Add(this.@__symbols);
			this.@__split.Panel2.Controls.Add(this.@__symbolsLbl);
			this.@__split.Size = new System.Drawing.Size(451, 229);
			this.@__split.SplitterDistance = 213;
			this.@__split.TabIndex = 5;
			// 
			// __sColName
			// 
			this.@__sColName.HeaderText = "Name";
			this.@__sColName.Name = "__sColName";
			// 
			// __sColVal
			// 
			this.@__sColVal.HeaderText = "Value";
			this.@__sColVal.Name = "__sColVal";
			this.@__sColVal.Width = 75;
			// 
			// __result
			// 
			this.@__result.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__result.BackColor = System.Drawing.SystemColors.Window;
			this.@__result.Location = new System.Drawing.Point(9, 254);
			this.@__result.Name = "__result";
			this.@__result.ReadOnly = true;
			this.@__result.Size = new System.Drawing.Size(451, 20);
			this.@__result.TabIndex = 6;
			// 
			// __eval
			// 
			this.@__eval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__eval.Location = new System.Drawing.Point(9, 280);
			this.@__eval.Name = "__eval";
			this.@__eval.Size = new System.Drawing.Size(75, 23);
			this.@__eval.TabIndex = 7;
			this.@__eval.Text = "Evaluate";
			this.@__eval.UseVisualStyleBackColor = true;
			// 
			// __cancel
			// 
			this.@__cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__cancel.Location = new System.Drawing.Point(385, 280);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(75, 23);
			this.@__cancel.TabIndex = 8;
			this.@__cancel.Text = "Cancel";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.Location = new System.Drawing.Point(304, 280);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 9;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// __tok
			// 
			this.@__tok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__tok.Location = new System.Drawing.Point(90, 280);
			this.@__tok.Name = "__tok";
			this.@__tok.Size = new System.Drawing.Size(75, 23);
			this.@__tok.TabIndex = 10;
			this.@__tok.Text = "Tokenize";
			this.@__tok.UseVisualStyleBackColor = true;
			// 
			// ConditionEditor
			// 
			this.AcceptButton = this.@__ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__cancel;
			this.ClientSize = new System.Drawing.Size(469, 311);
			this.Controls.Add(this.@__tok);
			this.Controls.Add(this.@__ok);
			this.Controls.Add(this.@__cancel);
			this.Controls.Add(this.@__eval);
			this.Controls.Add(this.@__result);
			this.Controls.Add(this.@__split);
			this.Controls.Add(this.@__resultLbl);
			this.Name = "ConditionEditor";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Package Item Condition Editor";
			((System.ComponentModel.ISupportInitialize)(this.@__symbols)).EndInit();
			this.@__split.Panel1.ResumeLayout(false);
			this.@__split.Panel1.PerformLayout();
			this.@__split.Panel2.ResumeLayout(false);
			this.@__split.Panel2.PerformLayout();
			this.@__split.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox __expression;
		private System.Windows.Forms.Label __expressionLbl;
		private System.Windows.Forms.Label __resultLbl;
		private System.Windows.Forms.Label __symbolsLbl;
		private System.Windows.Forms.DataGridView __symbols;
		private System.Windows.Forms.SplitContainer __split;
		private System.Windows.Forms.DataGridViewTextBoxColumn __sColName;
		private System.Windows.Forms.DataGridViewTextBoxColumn __sColVal;
		private System.Windows.Forms.TextBox __result;
		private System.Windows.Forms.Button __eval;
		private System.Windows.Forms.Button __cancel;
		private System.Windows.Forms.Button __ok;
		private System.Windows.Forms.Button __tok;
	}
}