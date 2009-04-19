namespace Anolis.Resourcer {
	partial class PendingOperationsForm {
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
			this.@__listOperations = new System.Windows.Forms.ListView();
			this.@__colAction = new System.Windows.Forms.ColumnHeader();
			this.@__colPath = new System.Windows.Forms.ColumnHeader();
			this.@__ok = new System.Windows.Forms.Button();
			this.@__listCancelOp = new System.Windows.Forms.Button();
			this.@__tabs = new System.Windows.Forms.TabControl();
			this.@__tabList = new System.Windows.Forms.TabPage();
			this.@__tabPackage = new System.Windows.Forms.TabPage();
			this.@__tabRc = new System.Windows.Forms.TabPage();
			this.@__xmlText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.@__tabs.SuspendLayout();
			this.@__tabList.SuspendLayout();
			this.@__tabPackage.SuspendLayout();
			this.@__tabRc.SuspendLayout();
			this.SuspendLayout();
			// 
			// __listOperations
			// 
			this.@__listOperations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__listOperations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.@__colAction,
            this.@__colPath});
			this.@__listOperations.Location = new System.Drawing.Point(3, 3);
			this.@__listOperations.Name = "__listOperations";
			this.@__listOperations.Size = new System.Drawing.Size(349, 306);
			this.@__listOperations.TabIndex = 0;
			this.@__listOperations.UseCompatibleStateImageBehavior = false;
			this.@__listOperations.View = System.Windows.Forms.View.Details;
			// 
			// __colAction
			// 
			this.@__colAction.Text = "Action";
			// 
			// __colPath
			// 
			this.@__colPath.Text = "Path";
			this.@__colPath.Width = 240;
			// 
			// __ok
			// 
			this.@__ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.@__ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.@__ok.Location = new System.Drawing.Point(290, 380);
			this.@__ok.Name = "__ok";
			this.@__ok.Size = new System.Drawing.Size(75, 23);
			this.@__ok.TabIndex = 1;
			this.@__ok.Text = "OK";
			this.@__ok.UseVisualStyleBackColor = true;
			// 
			// __listCancelOp
			// 
			this.@__listCancelOp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.@__listCancelOp.Enabled = false;
			this.@__listCancelOp.Location = new System.Drawing.Point(3, 315);
			this.@__listCancelOp.Name = "__listCancelOp";
			this.@__listCancelOp.Size = new System.Drawing.Size(123, 23);
			this.@__listCancelOp.TabIndex = 2;
			this.@__listCancelOp.Text = "Cancel Operation";
			this.@__listCancelOp.UseVisualStyleBackColor = true;
			// 
			// __tabs
			// 
			this.@__tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__tabs.Controls.Add(this.@__tabList);
			this.@__tabs.Controls.Add(this.@__tabPackage);
			this.@__tabs.Controls.Add(this.@__tabRc);
			this.@__tabs.Location = new System.Drawing.Point(6, 7);
			this.@__tabs.Margin = new System.Windows.Forms.Padding(0);
			this.@__tabs.Name = "__tabs";
			this.@__tabs.SelectedIndex = 0;
			this.@__tabs.Size = new System.Drawing.Size(363, 370);
			this.@__tabs.TabIndex = 3;
			// 
			// __tabList
			// 
			this.@__tabList.Controls.Add(this.@__listCancelOp);
			this.@__tabList.Controls.Add(this.@__listOperations);
			this.@__tabList.Location = new System.Drawing.Point(4, 22);
			this.@__tabList.Name = "__tabList";
			this.@__tabList.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabList.Size = new System.Drawing.Size(355, 344);
			this.@__tabList.TabIndex = 0;
			this.@__tabList.Text = "List";
			this.@__tabList.UseVisualStyleBackColor = true;
			// 
			// __tabPackage
			// 
			this.@__tabPackage.Controls.Add(this.@__xmlText);
			this.@__tabPackage.Location = new System.Drawing.Point(4, 22);
			this.@__tabPackage.Name = "__tabPackage";
			this.@__tabPackage.Padding = new System.Windows.Forms.Padding(3);
			this.@__tabPackage.Size = new System.Drawing.Size(355, 344);
			this.@__tabPackage.TabIndex = 1;
			this.@__tabPackage.Text = "Package XML";
			this.@__tabPackage.UseVisualStyleBackColor = true;
			// 
			// __tabRc
			// 
			this.@__tabRc.Controls.Add(this.label1);
			this.@__tabRc.Location = new System.Drawing.Point(4, 22);
			this.@__tabRc.Name = "__tabRc";
			this.@__tabRc.Size = new System.Drawing.Size(355, 344);
			this.@__tabRc.TabIndex = 2;
			this.@__tabRc.Text = "RC Script";
			this.@__tabRc.UseVisualStyleBackColor = true;
			// 
			// __xmlText
			// 
			this.@__xmlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.@__xmlText.BackColor = System.Drawing.SystemColors.Window;
			this.@__xmlText.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.@__xmlText.Location = new System.Drawing.Point(6, 6);
			this.@__xmlText.Multiline = true;
			this.@__xmlText.Name = "__xmlText";
			this.@__xmlText.ReadOnly = true;
			this.@__xmlText.Size = new System.Drawing.Size(343, 332);
			this.@__xmlText.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(125, 157);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Not yet implemented";
			// 
			// PendingOperationsForm
			// 
			this.AcceptButton = this.@__ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.@__ok;
			this.ClientSize = new System.Drawing.Size(373, 411);
			this.Controls.Add(this.@__ok);
			this.Controls.Add(this.@__tabs);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PendingOperationsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Pending Operations";
			this.@__tabs.ResumeLayout(false);
			this.@__tabList.ResumeLayout(false);
			this.@__tabPackage.ResumeLayout(false);
			this.@__tabPackage.PerformLayout();
			this.@__tabRc.ResumeLayout(false);
			this.@__tabRc.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView __listOperations;
		private System.Windows.Forms.Button __ok;
		private System.Windows.Forms.ColumnHeader __colAction;
		private System.Windows.Forms.ColumnHeader __colPath;
		private System.Windows.Forms.Button __listCancelOp;
		private System.Windows.Forms.TabControl __tabs;
		private System.Windows.Forms.TabPage __tabList;
		private System.Windows.Forms.TabPage __tabPackage;
		private System.Windows.Forms.TabPage __tabRc;
		private System.Windows.Forms.TextBox __xmlText;
		private System.Windows.Forms.Label label1;
	}
}