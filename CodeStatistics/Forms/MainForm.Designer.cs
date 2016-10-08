namespace CodeStatistics.Forms {
    partial class MainForm {
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
            this.btnProjectFolder = new System.Windows.Forms.Button();
            this.btnProjectGitHub = new System.Windows.Forms.Button();
            this.btnViewSourceCode = new System.Windows.Forms.Button();
            this.btnViewAbout = new System.Windows.Forms.Button();
            this.tableBottomPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnProjectArchive = new System.Windows.Forms.Button();
            this.btnViewOptions = new System.Windows.Forms.Button();
            this.tableBottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProjectFolder
            // 
            this.btnProjectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProjectFolder.BackColor = System.Drawing.Color.Gainsboro;
            this.btnProjectFolder.FlatAppearance.BorderSize = 0;
            this.btnProjectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProjectFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnProjectFolder.Location = new System.Drawing.Point(12, 12);
            this.btnProjectFolder.Name = "btnProjectFolder";
            this.btnProjectFolder.Size = new System.Drawing.Size(312, 38);
            this.btnProjectFolder.TabIndex = 0;
            this.btnProjectFolder.UseVisualStyleBackColor = false;
            this.btnProjectFolder.Click += new System.EventHandler(this.btnProjectFolder_Click);
            // 
            // btnProjectGitHub
            // 
            this.btnProjectGitHub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProjectGitHub.BackColor = System.Drawing.Color.Gainsboro;
            this.btnProjectGitHub.FlatAppearance.BorderSize = 0;
            this.btnProjectGitHub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProjectGitHub.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnProjectGitHub.Location = new System.Drawing.Point(12, 100);
            this.btnProjectGitHub.Name = "btnProjectGitHub";
            this.btnProjectGitHub.Size = new System.Drawing.Size(312, 38);
            this.btnProjectGitHub.TabIndex = 1;
            this.btnProjectGitHub.UseVisualStyleBackColor = false;
            this.btnProjectGitHub.Click += new System.EventHandler(this.btnProjectGitHub_Click);
            // 
            // btnViewSourceCode
            // 
            this.btnViewSourceCode.BackColor = System.Drawing.Color.Gainsboro;
            this.btnViewSourceCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnViewSourceCode.FlatAppearance.BorderSize = 0;
            this.btnViewSourceCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewSourceCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnViewSourceCode.Location = new System.Drawing.Point(0, 0);
            this.btnViewSourceCode.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnViewSourceCode.Name = "btnViewSourceCode";
            this.btnViewSourceCode.Size = new System.Drawing.Size(153, 38);
            this.btnViewSourceCode.TabIndex = 2;
            this.btnViewSourceCode.UseVisualStyleBackColor = false;
            this.btnViewSourceCode.Click += new System.EventHandler(this.btnViewSourceCode_Click);
            // 
            // btnViewAbout
            // 
            this.btnViewAbout.BackColor = System.Drawing.Color.Gainsboro;
            this.btnViewAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnViewAbout.FlatAppearance.BorderSize = 0;
            this.btnViewAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnViewAbout.Location = new System.Drawing.Point(159, 0);
            this.btnViewAbout.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnViewAbout.Name = "btnViewAbout";
            this.btnViewAbout.Size = new System.Drawing.Size(153, 38);
            this.btnViewAbout.TabIndex = 3;
            this.btnViewAbout.UseVisualStyleBackColor = false;
            this.btnViewAbout.Click += new System.EventHandler(this.btnViewAbout_Click);
            // 
            // tableBottomPanel
            // 
            this.tableBottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableBottomPanel.ColumnCount = 2;
            this.tableBottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBottomPanel.Controls.Add(this.btnViewAbout, 1, 0);
            this.tableBottomPanel.Controls.Add(this.btnViewSourceCode, 0, 0);
            this.tableBottomPanel.Location = new System.Drawing.Point(12, 212);
            this.tableBottomPanel.Name = "tableBottomPanel";
            this.tableBottomPanel.RowCount = 1;
            this.tableBottomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBottomPanel.Size = new System.Drawing.Size(312, 38);
            this.tableBottomPanel.TabIndex = 4;
            // 
            // btnProjectArchive
            // 
            this.btnProjectArchive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProjectArchive.BackColor = System.Drawing.Color.Gainsboro;
            this.btnProjectArchive.FlatAppearance.BorderSize = 0;
            this.btnProjectArchive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProjectArchive.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnProjectArchive.Location = new System.Drawing.Point(12, 56);
            this.btnProjectArchive.Name = "btnProjectArchive";
            this.btnProjectArchive.Size = new System.Drawing.Size(312, 38);
            this.btnProjectArchive.TabIndex = 5;
            this.btnProjectArchive.UseVisualStyleBackColor = false;
            this.btnProjectArchive.Click += new System.EventHandler(this.btnProjectArchive_Click);
            // 
            // btnViewOptions
            // 
            this.btnViewOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewOptions.BackColor = System.Drawing.Color.Gainsboro;
            this.btnViewOptions.FlatAppearance.BorderSize = 0;
            this.btnViewOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnViewOptions.Location = new System.Drawing.Point(12, 168);
            this.btnViewOptions.Name = "btnViewOptions";
            this.btnViewOptions.Size = new System.Drawing.Size(312, 38);
            this.btnViewOptions.TabIndex = 6;
            this.btnViewOptions.UseVisualStyleBackColor = false;
            this.btnViewOptions.Click += new System.EventHandler(this.btnViewOptions_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(336, 262);
            this.Controls.Add(this.btnViewOptions);
            this.Controls.Add(this.btnProjectArchive);
            this.Controls.Add(this.tableBottomPanel);
            this.Controls.Add(this.btnProjectGitHub);
            this.Controls.Add(this.btnProjectFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.tableBottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProjectFolder;
        private System.Windows.Forms.Button btnProjectGitHub;
        private System.Windows.Forms.Button btnViewSourceCode;
        private System.Windows.Forms.Button btnViewAbout;
        private System.Windows.Forms.TableLayoutPanel tableBottomPanel;
        private System.Windows.Forms.Button btnProjectArchive;
        private System.Windows.Forms.Button btnViewOptions;

    }
}