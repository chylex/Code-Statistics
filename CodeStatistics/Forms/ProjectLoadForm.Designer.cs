﻿namespace CodeStatistics.Forms {
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
            this.SuspendLayout();
            // 
            // progressBarLoad
            // 
            this.progressBarLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarLoad.Location = new System.Drawing.Point(12, 45);
            this.progressBarLoad.MarqueeAnimationSpeed = 10;
            this.progressBarLoad.Name = "progressBarLoad";
            this.progressBarLoad.Size = new System.Drawing.Size(307, 23);
            this.progressBarLoad.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarLoad.TabIndex = 0;
            this.progressBarLoad.Value = 100;
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
            this.labelLoadInfo.Text = "...";
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
            this.labelLoadData.Text = "...";
            this.labelLoadData.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(244, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ProjectLoadForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(331, 111);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelLoadData);
            this.Controls.Add(this.labelLoadInfo);
            this.Controls.Add(this.progressBarLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProjectLoadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Statistics - Loading Project";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarLoad;
        private System.Windows.Forms.Label labelLoadInfo;
        private System.Windows.Forms.Label labelLoadData;
        private System.Windows.Forms.Button btnCancel;
    }
}