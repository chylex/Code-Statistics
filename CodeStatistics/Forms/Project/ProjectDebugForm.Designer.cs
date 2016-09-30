namespace CodeStatistics.Forms.Project {
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
            this.textBoxFilterFiles = new System.Windows.Forms.TextBox();
            this.tableBottomPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnReprocess = new System.Windows.Forms.Button();
            this.btnLoadOriginal = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.panelSplitContainerInner = new System.Windows.Forms.SplitContainer();
            this.treeViewData = new System.Windows.Forms.TreeView();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelSplitContainer)).BeginInit();
            this.panelSplitContainer.Panel1.SuspendLayout();
            this.panelSplitContainer.Panel2.SuspendLayout();
            this.panelSplitContainer.SuspendLayout();
            this.tableBottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSplitContainerInner)).BeginInit();
            this.panelSplitContainerInner.Panel1.SuspendLayout();
            this.panelSplitContainerInner.Panel2.SuspendLayout();
            this.panelSplitContainerInner.SuspendLayout();
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
            this.panelSplitContainer.Panel1.Controls.Add(this.textBoxFilterFiles);
            this.panelSplitContainer.Panel1.Controls.Add(this.tableBottomPanel);
            this.panelSplitContainer.Panel1.Controls.Add(this.listBoxFiles);
            // 
            // panelSplitContainer.Panel2
            // 
            this.panelSplitContainer.Panel2.Controls.Add(this.panelSplitContainerInner);
            this.panelSplitContainer.Size = new System.Drawing.Size(744, 625);
            this.panelSplitContainer.SplitterDistance = 220;
            this.panelSplitContainer.TabIndex = 0;
            // 
            // textBoxFilterFiles
            // 
            this.textBoxFilterFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilterFiles.Location = new System.Drawing.Point(13, 565);
            this.textBoxFilterFiles.Name = "textBoxFilterFiles";
            this.textBoxFilterFiles.Size = new System.Drawing.Size(205, 20);
            this.textBoxFilterFiles.TabIndex = 4;
            this.textBoxFilterFiles.TextChanged += new System.EventHandler(this.textBoxFilterFiles_TextChanged);
            // 
            // tableBottomPanel
            // 
            this.tableBottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableBottomPanel.ColumnCount = 3;
            this.tableBottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33111F));
            this.tableBottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33778F));
            this.tableBottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33112F));
            this.tableBottomPanel.Controls.Add(this.btnReprocess, 0, 0);
            this.tableBottomPanel.Controls.Add(this.btnLoadOriginal, 0, 0);
            this.tableBottomPanel.Controls.Add(this.btnDebug, 1, 0);
            this.tableBottomPanel.Location = new System.Drawing.Point(13, 588);
            this.tableBottomPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableBottomPanel.Name = "tableBottomPanel";
            this.tableBottomPanel.RowCount = 1;
            this.tableBottomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableBottomPanel.Size = new System.Drawing.Size(204, 26);
            this.tableBottomPanel.TabIndex = 3;
            // 
            // btnReprocess
            // 
            this.btnReprocess.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnReprocess.Location = new System.Drawing.Point(70, 3);
            this.btnReprocess.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnReprocess.Name = "btnReprocess";
            this.btnReprocess.Size = new System.Drawing.Size(65, 23);
            this.btnReprocess.TabIndex = 3;
            this.btnReprocess.UseVisualStyleBackColor = true;
            this.btnReprocess.Click += new System.EventHandler(this.btnReprocess_Click);
            // 
            // btnLoadOriginal
            // 
            this.btnLoadOriginal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLoadOriginal.Location = new System.Drawing.Point(0, 3);
            this.btnLoadOriginal.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnLoadOriginal.Name = "btnLoadOriginal";
            this.btnLoadOriginal.Size = new System.Drawing.Size(64, 23);
            this.btnLoadOriginal.TabIndex = 2;
            this.btnLoadOriginal.UseVisualStyleBackColor = true;
            this.btnLoadOriginal.Click += new System.EventHandler(this.btnLoadOriginal_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDebug.Location = new System.Drawing.Point(138, 3);
            this.btnDebug.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(66, 23);
            this.btnDebug.TabIndex = 1;
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
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
            this.listBoxFiles.Size = new System.Drawing.Size(205, 547);
            this.listBoxFiles.TabIndex = 0;
            this.listBoxFiles.SelectedValueChanged += new System.EventHandler(this.listBoxFiles_SelectedValueChange);
            // 
            // panelSplitContainerInner
            // 
            this.panelSplitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSplitContainerInner.Location = new System.Drawing.Point(0, 0);
            this.panelSplitContainerInner.Name = "panelSplitContainerInner";
            // 
            // panelSplitContainerInner.Panel1
            // 
            this.panelSplitContainerInner.Panel1.Controls.Add(this.treeViewData);
            // 
            // panelSplitContainerInner.Panel2
            // 
            this.panelSplitContainerInner.Panel2.Controls.Add(this.textBoxCode);
            this.panelSplitContainerInner.Size = new System.Drawing.Size(520, 625);
            this.panelSplitContainerInner.SplitterDistance = 172;
            this.panelSplitContainerInner.TabIndex = 1;
            // 
            // treeViewData
            // 
            this.treeViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewData.Location = new System.Drawing.Point(3, 12);
            this.treeViewData.Name = "treeViewData";
            this.treeViewData.Size = new System.Drawing.Size(166, 601);
            this.treeViewData.TabIndex = 0;
            // 
            // textBoxCode
            // 
            this.textBoxCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCode.BackColor = System.Drawing.Color.White;
            this.textBoxCode.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxCode.Location = new System.Drawing.Point(3, 12);
            this.textBoxCode.Multiline = true;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCode.Size = new System.Drawing.Size(329, 601);
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
            this.panelSplitContainer.Panel1.ResumeLayout(false);
            this.panelSplitContainer.Panel1.PerformLayout();
            this.panelSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelSplitContainer)).EndInit();
            this.panelSplitContainer.ResumeLayout(false);
            this.tableBottomPanel.ResumeLayout(false);
            this.panelSplitContainerInner.Panel1.ResumeLayout(false);
            this.panelSplitContainerInner.Panel2.ResumeLayout(false);
            this.panelSplitContainerInner.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSplitContainerInner)).EndInit();
            this.panelSplitContainerInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer panelSplitContainer;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Button btnLoadOriginal;
        private System.Windows.Forms.TableLayoutPanel tableBottomPanel;
        private System.Windows.Forms.Button btnReprocess;
        private System.Windows.Forms.SplitContainer panelSplitContainerInner;
        private System.Windows.Forms.TreeView treeViewData;
        private System.Windows.Forms.TextBox textBoxFilterFiles;
    }
}