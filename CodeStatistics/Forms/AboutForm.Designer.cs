namespace CodeStatistics.Forms {
    partial class AboutForm {
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
            this.textContents = new System.Windows.Forms.RichTextBox();
            this.btnReadme = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textContents
            // 
            this.textContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textContents.BackColor = System.Drawing.Color.White;
            this.textContents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textContents.Cursor = System.Windows.Forms.Cursors.Default;
            this.textContents.DetectUrls = false;
            this.textContents.Location = new System.Drawing.Point(12, 12);
            this.textContents.Name = "textContents";
            this.textContents.ReadOnly = true;
            this.textContents.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.textContents.ShortcutsEnabled = false;
            this.textContents.Size = new System.Drawing.Size(330, 256);
            this.textContents.TabIndex = 0;
            this.textContents.TabStop = false;
            this.textContents.Text = "";
            // 
            // btnReadme
            // 
            this.btnReadme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadme.AutoSize = true;
            this.btnReadme.BackColor = System.Drawing.Color.Gainsboro;
            this.btnReadme.FlatAppearance.BorderSize = 0;
            this.btnReadme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnReadme.Location = new System.Drawing.Point(292, 274);
            this.btnReadme.Name = "btnReadme";
            this.btnReadme.Size = new System.Drawing.Size(50, 23);
            this.btnReadme.TabIndex = 1;
            this.btnReadme.UseVisualStyleBackColor = false;
            this.btnReadme.Click += new System.EventHandler(btnReadme_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(354, 309);
            this.Controls.Add(this.btnReadme);
            this.Controls.Add(this.textContents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox textContents;
        private System.Windows.Forms.Button btnReadme;
    }
}