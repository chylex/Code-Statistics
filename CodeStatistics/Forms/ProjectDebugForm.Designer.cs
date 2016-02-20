namespace CodeStatistics.Forms {
    partial class ProjectDebugForm {
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
            this.panelSplitContainer = new System.Windows.Forms.SplitContainer();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelSplitContainer)).BeginInit();
            this.panelSplitContainer.Panel1.SuspendLayout();
            this.panelSplitContainer.Panel2.SuspendLayout();
            this.panelSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSplitContainer
            // 
            this.panelSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.panelSplitContainer.Name = "panelSplitContainer";
            // 
            // panelSplitContainer.Panel1
            // 
            this.panelSplitContainer.Panel1.Controls.Add(this.listBoxFiles);
            // 
            // panelSplitContainer.Panel2
            // 
            this.panelSplitContainer.Panel2.Controls.Add(this.textBoxCode);
            this.panelSplitContainer.Size = new System.Drawing.Size(744, 625);
            this.panelSplitContainer.SplitterDistance = 240;
            this.panelSplitContainer.TabIndex = 0;
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.HorizontalScrollbar = true;
            this.listBoxFiles.IntegralHeight = false;
            this.listBoxFiles.Location = new System.Drawing.Point(12, 12);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(225, 602);
            this.listBoxFiles.TabIndex = 0;
            this.listBoxFiles.SelectedValueChanged += new System.EventHandler(this.listBoxFiles_SelectedValueChange);
            // 
            // textBoxCode
            // 
            this.textBoxCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCode.BackColor = System.Drawing.Color.White;
            this.textBoxCode.Location = new System.Drawing.Point(3, 12);
            this.textBoxCode.Multiline = true;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.ReadOnly = true;
            this.textBoxCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCode.Size = new System.Drawing.Size(485, 602);
            this.textBoxCode.TabIndex = 0;
            this.textBoxCode.WordWrap = false;
            // 
            // ProjectDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(744, 625);
            this.Controls.Add(this.panelSplitContainer);
            this.Name = "ProjectDebugForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Code Statistics - Project Debug";
            this.panelSplitContainer.Panel1.ResumeLayout(false);
            this.panelSplitContainer.Panel2.ResumeLayout(false);
            this.panelSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSplitContainer)).EndInit();
            this.panelSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer panelSplitContainer;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.ListBox listBoxFiles;
    }
}