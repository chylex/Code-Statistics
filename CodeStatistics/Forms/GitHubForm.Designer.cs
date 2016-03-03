namespace CodeStatistics.Forms {
    partial class GitHubForm {
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
            this.textBoxRepository = new System.Windows.Forms.TextBox();
            this.labelRepository = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labelBranch = new System.Windows.Forms.Label();
            this.listBranches = new System.Windows.Forms.ComboBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.tablePanel.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxRepository
            // 
            this.textBoxRepository.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRepository.Location = new System.Drawing.Point(0, 16);
            this.textBoxRepository.Name = "textBoxRepository";
            this.textBoxRepository.Size = new System.Drawing.Size(175, 20);
            this.textBoxRepository.TabIndex = 0;
            this.textBoxRepository.TextChanged += new System.EventHandler(this.textBoxRepository_TextChanged);
            // 
            // labelRepository
            // 
            this.labelRepository.AutoSize = true;
            this.labelRepository.BackColor = System.Drawing.Color.Transparent;
            this.labelRepository.Location = new System.Drawing.Point(-3, 0);
            this.labelRepository.Margin = new System.Windows.Forms.Padding(0);
            this.labelRepository.Name = "labelRepository";
            this.labelRepository.Size = new System.Drawing.Size(0, 13);
            this.labelRepository.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(299, 164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelBranch
            // 
            this.labelBranch.AutoSize = true;
            this.labelBranch.BackColor = System.Drawing.Color.Transparent;
            this.labelBranch.Location = new System.Drawing.Point(-3, 0);
            this.labelBranch.Margin = new System.Windows.Forms.Padding(0);
            this.labelBranch.Name = "labelBranch";
            this.labelBranch.Size = new System.Drawing.Size(0, 13);
            this.labelBranch.TabIndex = 4;
            // 
            // listBranches
            // 
            this.listBranches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.listBranches.FormattingEnabled = true;
            this.listBranches.IntegralHeight = false;
            this.listBranches.Location = new System.Drawing.Point(0, 16);
            this.listBranches.Name = "listBranches";
            this.listBranches.Size = new System.Drawing.Size(175, 130);
            this.listBranches.TabIndex = 0;
            this.listBranches.SelectedValueChanged += new System.EventHandler(this.listBranches_SelectedValueChanged);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(218, 164);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // tablePanel
            // 
            this.tablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablePanel.ColumnCount = 2;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.Controls.Add(this.panelLeft, 0, 0);
            this.tablePanel.Controls.Add(this.panelRight, 1, 0);
            this.tablePanel.Location = new System.Drawing.Point(12, 12);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 1;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.Size = new System.Drawing.Size(362, 146);
            this.tablePanel.TabIndex = 8;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.textBoxRepository);
            this.panelLeft.Controls.Add(this.labelRepository);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(175, 146);
            this.panelLeft.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.listBranches);
            this.panelRight.Controls.Add(this.labelBranch);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(187, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(175, 146);
            this.panelRight.TabIndex = 1;
            // 
            // GitHubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(386, 199);
            this.Controls.Add(this.tablePanel);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GitHubForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.OnLoad);
            this.tablePanel.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRepository;
        private System.Windows.Forms.Label labelRepository;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labelBranch;
        private System.Windows.Forms.ComboBox listBranches;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
    }
}