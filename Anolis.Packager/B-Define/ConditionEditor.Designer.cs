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
			this.@__resultLbl = new System.Windows.Forms.Label();
			this.@__symbols = new System.Windows.Forms.DataGridView();
			this.@__sColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.@__sColVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.@__result = new System.Windows.Forms.TextBox();
			this.@__eval = new System.Windows.Forms.Button();
			this.@__cancel = new System.Windows.Forms.Button();
			this.@__ok = new System.Windows.Forms.Button();
			this.@__tok = new System.Windows.Forms.Button();
			this.@__layout = new System.Windows.Forms.TableLayoutPanel();
			this.@__debugLayout = new System.Windows.Forms.TableLayoutPanel();
			this.@__dbgStackValLbl = new System.Windows.Forms.Label();
			this.@__dbgStackOpLbl = new System.Windows.Forms.Label();
			this.@__dbgValStack = new System.Windows.Forms.ListBox();
			this.@__dbgOpStack = new System.Windows.Forms.ListBox();
			this.@__dbgValLbl = new System.Windows.Forms.Label();
			this.@__dbgVal = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.@__dbgBack = new System.Windows.Forms.Button();
			this.@__dbgNext = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.@__symbols)).BeginInit();
			this.@__layout.SuspendLayout();
			this.@__debugLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// __expression
			// 
			this.@__expression.AcceptsReturn = true;
			this.@__expression.AcceptsTab = true;
			this.@__expression.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__expression.Location = new System.Drawing.Point(3, 28);
			this.@__expression.Multiline = true;
			this.@__expression.Name = "__expression";
			this.@__expression.Size = new System.Drawing.Size(287, 255);
			this.@__expression.TabIndex = 0;
			// 
			// __resultLbl
			// 
			this.@__resultLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__resultLbl.AutoSize = true;
			this.@__resultLbl.Location = new System.Drawing.Point(6, 301);
			this.@__resultLbl.Name = "__resultLbl";
			this.@__resultLbl.Size = new System.Drawing.Size(37, 13);
			this.@__resultLbl.TabIndex = 2;
			this.@__resultLbl.Text = "Result";
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
			this.@__symbols.Location = new System.Drawing.Point(296, 28);
			this.@__symbols.Name = "__symbols";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.@__symbols.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.@__symbols.Size = new System.Drawing.Size(295, 255);
			this.@__symbols.TabIndex = 4;
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
			this.@__result.Location = new System.Drawing.Point(9, 317);
			this.@__result.Name = "__result";
			this.@__result.ReadOnly = true;
			this.@__result.Size = new System.Drawing.Size(891, 20);
			this.@__result.TabIndex = 6;
			// 
			// __eval
			// 
			this.@__eval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__eval.Location = new System.Drawing.Point(9, 343);
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
			this.@__cancel.Location = new System.Drawing.Point(825, 343);
			this.@__cancel.Name = "__cancel";
			this.@__cancel.Size = new System.Drawing.Size(75, 23);
			this.@__cancel.TabIndex = 8;
			this.@__cancel.Text = "Cancel";
			this.@__cancel.UseVisualStyleBackColor = true;
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.Location = new System.Drawing.Point(744, 343);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 9;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// __tok
			// 
			this.@__tok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__tok.Location = new System.Drawing.Point(90, 343);
			this.@__tok.Name = "__tok";
			this.@__tok.Size = new System.Drawing.Size(75, 23);
			this.@__tok.TabIndex = 10;
			this.@__tok.Text = "Tokenize";
			this.@__tok.UseVisualStyleBackColor = true;
			// 
			// __layout
			// 
			this.@__layout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__layout.ColumnCount = 3;
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
			this.@__layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.@__layout.Controls.Add(this.@__debugLayout, 2, 1);
			this.@__layout.Controls.Add(this.@__symbols, 1, 1);
			this.@__layout.Controls.Add(this.@__expression, 0, 1);
			this.@__layout.Controls.Add(this.label2, 0, 0);
			this.@__layout.Controls.Add(this.label3, 1, 0);
			this.@__layout.Controls.Add(this.label4, 2, 0);
			this.@__layout.Location = new System.Drawing.Point(9, 12);
			this.@__layout.Name = "__layout";
			this.@__layout.RowCount = 2;
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.@__layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.@__layout.Size = new System.Drawing.Size(888, 286);
			this.@__layout.TabIndex = 12;
			// 
			// __debugLayout
			// 
			this.@__debugLayout.ColumnCount = 2;
			this.@__debugLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.@__debugLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.@__debugLayout.Controls.Add(this.@__dbgStackValLbl, 0, 0);
			this.@__debugLayout.Controls.Add(this.@__dbgStackOpLbl, 1, 0);
			this.@__debugLayout.Controls.Add(this.@__dbgValStack, 0, 1);
			this.@__debugLayout.Controls.Add(this.@__dbgOpStack, 1, 1);
			this.@__debugLayout.Controls.Add(this.@__dbgValLbl, 0, 2);
			this.@__debugLayout.Controls.Add(this.@__dbgVal, 1, 2);
			this.@__debugLayout.Controls.Add(this.@__dbgBack, 0, 3);
			this.@__debugLayout.Controls.Add(this.@__dbgNext, 1, 3);
			this.@__debugLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__debugLayout.Location = new System.Drawing.Point(597, 28);
			this.@__debugLayout.Name = "__debugLayout";
			this.@__debugLayout.RowCount = 4;
			this.@__debugLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.@__debugLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.@__debugLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.@__debugLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.@__debugLayout.Size = new System.Drawing.Size(288, 255);
			this.@__debugLayout.TabIndex = 5;
			// 
			// __dbgStackValLbl
			// 
			this.@__dbgStackValLbl.AutoSize = true;
			this.@__dbgStackValLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__dbgStackValLbl.Location = new System.Drawing.Point(3, 0);
			this.@__dbgStackValLbl.Name = "__dbgStackValLbl";
			this.@__dbgStackValLbl.Size = new System.Drawing.Size(138, 20);
			this.@__dbgStackValLbl.TabIndex = 0;
			this.@__dbgStackValLbl.Text = "Value Stack";
			this.@__dbgStackValLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// __dbgStackOpLbl
			// 
			this.@__dbgStackOpLbl.AutoSize = true;
			this.@__dbgStackOpLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__dbgStackOpLbl.Location = new System.Drawing.Point(147, 0);
			this.@__dbgStackOpLbl.Name = "__dbgStackOpLbl";
			this.@__dbgStackOpLbl.Size = new System.Drawing.Size(138, 20);
			this.@__dbgStackOpLbl.TabIndex = 1;
			this.@__dbgStackOpLbl.Text = "Operator Stack";
			this.@__dbgStackOpLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// __dbgValStack
			// 
			this.@__dbgValStack.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__dbgValStack.FormattingEnabled = true;
			this.@__dbgValStack.IntegralHeight = false;
			this.@__dbgValStack.Location = new System.Drawing.Point(3, 23);
			this.@__dbgValStack.Name = "__dbgValStack";
			this.@__dbgValStack.Size = new System.Drawing.Size(138, 174);
			this.@__dbgValStack.TabIndex = 2;
			// 
			// __dbgOpStack
			// 
			this.@__dbgOpStack.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__dbgOpStack.FormattingEnabled = true;
			this.@__dbgOpStack.IntegralHeight = false;
			this.@__dbgOpStack.Location = new System.Drawing.Point(147, 23);
			this.@__dbgOpStack.Name = "__dbgOpStack";
			this.@__dbgOpStack.Size = new System.Drawing.Size(138, 174);
			this.@__dbgOpStack.TabIndex = 3;
			// 
			// __dbgValLbl
			// 
			this.@__dbgValLbl.AutoSize = true;
			this.@__dbgValLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__dbgValLbl.Location = new System.Drawing.Point(3, 200);
			this.@__dbgValLbl.Name = "__dbgValLbl";
			this.@__dbgValLbl.Size = new System.Drawing.Size(138, 25);
			this.@__dbgValLbl.TabIndex = 4;
			this.@__dbgValLbl.Text = "Current Value";
			this.@__dbgValLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// __dbgVal
			// 
			this.@__dbgVal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__dbgVal.Location = new System.Drawing.Point(147, 203);
			this.@__dbgVal.Name = "__dbgVal";
			this.@__dbgVal.ReadOnly = true;
			this.@__dbgVal.Size = new System.Drawing.Size(138, 20);
			this.@__dbgVal.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(287, 25);
			this.label2.TabIndex = 0;
			this.label2.Text = "Input Expression";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(296, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(295, 25);
			this.label3.TabIndex = 1;
			this.label3.Text = "Symbols";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(597, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(288, 25);
			this.label4.TabIndex = 2;
			this.label4.Text = "Evaluation Stacks";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// __dbgBack
			// 
			this.@__dbgBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.@__dbgBack.Location = new System.Drawing.Point(66, 228);
			this.@__dbgBack.Name = "__dbgBack";
			this.@__dbgBack.Size = new System.Drawing.Size(75, 23);
			this.@__dbgBack.TabIndex = 6;
			this.@__dbgBack.Text = "Back";
			this.@__dbgBack.UseVisualStyleBackColor = true;
			// 
			// __dbgNext
			// 
			this.@__dbgNext.Location = new System.Drawing.Point(147, 228);
			this.@__dbgNext.Name = "__dbgNext";
			this.@__dbgNext.Size = new System.Drawing.Size(75, 23);
			this.@__dbgNext.TabIndex = 7;
			this.@__dbgNext.Text = "Next";
			this.@__dbgNext.UseVisualStyleBackColor = true;
			// 
			// ConditionEditor
			// 
			this.AcceptButton = this.@__ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__cancel;
			this.ClientSize = new System.Drawing.Size(909, 374);
			this.Controls.Add(this.@__layout);
			this.Controls.Add(this.@__tok);
			this.Controls.Add(this.@__ok);
			this.Controls.Add(this.@__cancel);
			this.Controls.Add(this.@__eval);
			this.Controls.Add(this.@__result);
			this.Controls.Add(this.@__resultLbl);
			this.DoubleBuffered = true;
			this.Name = "ConditionEditor";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Package Item Condition Editor";
			((System.ComponentModel.ISupportInitialize)(this.@__symbols)).EndInit();
			this.@__layout.ResumeLayout(false);
			this.@__layout.PerformLayout();
			this.@__debugLayout.ResumeLayout(false);
			this.@__debugLayout.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox __expression;
		private System.Windows.Forms.Label __resultLbl;
		private System.Windows.Forms.DataGridView __symbols;
		private System.Windows.Forms.DataGridViewTextBoxColumn __sColName;
		private System.Windows.Forms.DataGridViewTextBoxColumn __sColVal;
		private System.Windows.Forms.TextBox __result;
		private System.Windows.Forms.Button __eval;
		private System.Windows.Forms.Button __cancel;
		private System.Windows.Forms.Button __ok;
		private System.Windows.Forms.Button __tok;
		private System.Windows.Forms.TableLayoutPanel __layout;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TableLayoutPanel __debugLayout;
		private System.Windows.Forms.Label __dbgStackValLbl;
		private System.Windows.Forms.Label __dbgStackOpLbl;
		private System.Windows.Forms.ListBox __dbgValStack;
		private System.Windows.Forms.ListBox __dbgOpStack;
		private System.Windows.Forms.Label __dbgValLbl;
		private System.Windows.Forms.TextBox __dbgVal;
		private System.Windows.Forms.Button __dbgBack;
		private System.Windows.Forms.Button __dbgNext;
	}
}