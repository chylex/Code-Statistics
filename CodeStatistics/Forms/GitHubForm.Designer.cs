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
            this.btnListBranches = new System.Windows.Forms.Button();
            this.labelBranch = new System.Windows.Forms.Label();
            this.listBranches = new System.Windows.Forms.ComboBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxRepository
            // 
            this.textBoxRepository.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRepository.Location = new System.Drawing.Point(15, 25);
            this.textBoxRepository.Name = "textBoxRepository";
            this.textBoxRepository.Size = new System.Drawing.Size(298, 20);
            this.textBoxRepository.TabIndex = 0;
            // 
            // labelRepository
            // 
            this.labelRepository.AutoSize = true;
            this.labelRepository.BackColor = System.Drawing.Color.Transparent;
            this.labelRepository.Location = new System.Drawing.Point(12, 9);
            this.labelRepository.Name = "labelRepository";
            this.labelRepository.Size = new System.Drawing.Size(91, 13);
            this.labelRepository.TabIndex = 1;
            this.labelRepository.Text = "Repository Name:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(238, 113);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnListBranches
            // 
            this.btnListBranches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnListBranches.Location = new System.Drawing.Point(139, 113);
            this.btnListBranches.Name = "btnListBranches";
            this.btnListBranches.Size = new System.Drawing.Size(93, 23);
            this.btnListBranches.TabIndex = 3;
            this.btnListBranches.Text = "List Branches";
            this.btnListBranches.UseVisualStyleBackColor = true;
            this.btnListBranches.Click += new System.EventHandler(this.btnListBranches_Click);
            // 
            // labelBranch
            // 
            this.labelBranch.AutoSize = true;
            this.labelBranch.BackColor = System.Drawing.Color.Transparent;
            this.labelBranch.Location = new System.Drawing.Point(12, 57);
            this.labelBranch.Name = "labelBranch";
            this.labelBranch.Size = new System.Drawing.Size(44, 13);
            this.labelBranch.TabIndex = 4;
            this.labelBranch.Text = "Branch:";
            // 
            // listBranches
            // 
            this.listBranches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBranches.FormattingEnabled = true;
            this.listBranches.Location = new System.Drawing.Point(15, 73);
            this.listBranches.Name = "listBranches";
            this.listBranches.Size = new System.Drawing.Size(298, 21);
            this.listBranches.TabIndex = 6;
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(58, 113);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 7;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // GitHubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(325, 148);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.listBranches);
            this.Controls.Add(this.labelBranch);
            this.Controls.Add(this.btnListBranches);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelRepository);
            this.Controls.Add(this.textBoxRepository);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GitHubForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRepository;
        private System.Windows.Forms.Label labelRepository;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnListBranches;
        private System.Windows.Forms.Label labelBranch;
        private System.Windows.Forms.ComboBox listBranches;
        private System.Windows.Forms.Button btnDownload;
    }
}