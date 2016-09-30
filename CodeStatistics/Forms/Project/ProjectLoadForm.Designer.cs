namespace CodeStatistics.Forms.Project {
    partial class ProjectLoadForm {
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
            this.progressBarLoad = new System.Windows.Forms.ProgressBar();
            this.labelLoadInfo = new System.Windows.Forms.Label();
            this.labelLoadData = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpenOutput = new System.Windows.Forms.Button();
            this.btnDebugProject = new System.Windows.Forms.Button();
            this.btnBreakPoint = new System.Windows.Forms.Button();
            this.flowLayoutButtonsDebug = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutButtonsReady = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutButtonsDebug.SuspendLayout();
            this.flowLayoutButtonsReady.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBarLoad
            // 
            this.progressBarLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarLoad.Location = new System.Drawing.Point(12, 45);
            this.progressBarLoad.MarqueeAnimationSpeed = 10;
            this.progressBarLoad.Maximum = 1000;
            this.progressBarLoad.Name = "progressBarLoad";
            this.progressBarLoad.Size = new System.Drawing.Size(307, 23);
            this.progressBarLoad.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarLoad.TabIndex = 0;
            this.progressBarLoad.Value = 1000;
            // 
            // labelLoadInfo
            // 
            this.labelLoadInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLoadInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLoadInfo.Location = new System.Drawing.Point(12, 9);
            this.labelLoadInfo.Name = "labelLoadInfo";
            this.labelLoadInfo.Size = new System.Drawing.Size(307, 18);
            this.labelLoadInfo.TabIndex = 1;
            this.labelLoadInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelLoadData
            // 
            this.labelLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLoadData.Location = new System.Drawing.Point(12, 27);
            this.labelLoadData.Name = "labelLoadData";
            this.labelLoadData.Size = new System.Drawing.Size(307, 15);
            this.labelLoadData.TabIndex = 2;
            this.labelLoadData.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(269, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnCancel.Size = new System.Drawing.Size(50, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSize = true;
            this.btnClose.Location = new System.Drawing.Point(56, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnClose.Size = new System.Drawing.Size(50, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpenOutput
            // 
            this.btnOpenOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenOutput.AutoSize = true;
            this.btnOpenOutput.Location = new System.Drawing.Point(0, 0);
            this.btnOpenOutput.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnOpenOutput.Name = "btnOpenOutput";
            this.btnOpenOutput.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnOpenOutput.Size = new System.Drawing.Size(50, 23);
            this.btnOpenOutput.TabIndex = 5;
            this.btnOpenOutput.UseVisualStyleBackColor = true;
            this.btnOpenOutput.Click += new System.EventHandler(this.btnOpenOutput_Click);
            // 
            // btnDebugProject
            // 
            this.btnDebugProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebugProject.AutoSize = true;
            this.btnDebugProject.Location = new System.Drawing.Point(56, 0);
            this.btnDebugProject.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnDebugProject.Name = "btnDebugProject";
            this.btnDebugProject.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnDebugProject.Size = new System.Drawing.Size(50, 23);
            this.btnDebugProject.TabIndex = 6;
            this.btnDebugProject.UseVisualStyleBackColor = true;
            this.btnDebugProject.Click += new System.EventHandler(this.btnDebugProject_Click);
            // 
            // btnBreakPoint
            // 
            this.btnBreakPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBreakPoint.AutoSize = true;
            this.btnBreakPoint.Location = new System.Drawing.Point(0, 0);
            this.btnBreakPoint.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnBreakPoint.Name = "btnBreakPoint";
            this.btnBreakPoint.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnBreakPoint.Size = new System.Drawing.Size(50, 23);
            this.btnBreakPoint.TabIndex = 7;
            this.btnBreakPoint.UseVisualStyleBackColor = true;
            this.btnBreakPoint.Click += new System.EventHandler(this.btnBreakPoint_Click);
            // 
            // flowLayoutButtonsDebug
            // 
            this.flowLayoutButtonsDebug.AutoSize = true;
            this.flowLayoutButtonsDebug.Controls.Add(this.btnBreakPoint);
            this.flowLayoutButtonsDebug.Controls.Add(this.btnDebugProject);
            this.flowLayoutButtonsDebug.Location = new System.Drawing.Point(12, 76);
            this.flowLayoutButtonsDebug.Name = "flowLayoutButtonsDebug";
            this.flowLayoutButtonsDebug.Size = new System.Drawing.Size(106, 23);
            this.flowLayoutButtonsDebug.TabIndex = 8;
            this.flowLayoutButtonsDebug.Visible = false;
            this.flowLayoutButtonsDebug.WrapContents = false;
            // 
            // flowLayoutButtonsReady
            // 
            this.flowLayoutButtonsReady.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutButtonsReady.AutoSize = true;
            this.flowLayoutButtonsReady.Controls.Add(this.btnClose);
            this.flowLayoutButtonsReady.Controls.Add(this.btnOpenOutput);
            this.flowLayoutButtonsReady.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutButtonsReady.Location = new System.Drawing.Point(213, 76);
            this.flowLayoutButtonsReady.Name = "flowLayoutButtonsReady";
            this.flowLayoutButtonsReady.Size = new System.Drawing.Size(106, 23);
            this.flowLayoutButtonsReady.TabIndex = 9;
            this.flowLayoutButtonsReady.Visible = false;
            this.flowLayoutButtonsReady.WrapContents = false;
            // 
            // ProjectLoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(331, 111);
            this.Controls.Add(this.flowLayoutButtonsReady);
            this.Controls.Add(this.flowLayoutButtonsDebug);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelLoadData);
            this.Controls.Add(this.labelLoadInfo);
            this.Controls.Add(this.progressBarLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProjectLoadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.OnLoad);
            this.flowLayoutButtonsDebug.ResumeLayout(false);
            this.flowLayoutButtonsDebug.PerformLayout();
            this.flowLayoutButtonsReady.ResumeLayout(false);
            this.flowLayoutButtonsReady.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarLoad;
        private System.Windows.Forms.Label labelLoadInfo;
        private System.Windows.Forms.Label labelLoadData;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpenOutput;
        private System.Windows.Forms.Button btnDebugProject;
        private System.Windows.Forms.Button btnBreakPoint;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutButtonsDebug;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutButtonsReady;
    }
}