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
            this.textContents.Size = new System.Drawing.Size(330, 285);
            this.textContents.TabIndex = 0;
            this.textContents.TabStop = false;
            this.textContents.Text = "";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(354, 309);
            this.Controls.Add(this.textContents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.OnHelpButtonClicked);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox textContents;
    }
}