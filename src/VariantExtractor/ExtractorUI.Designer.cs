namespace MVAR_MPVR_Extraction
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dropPanel = new System.Windows.Forms.Panel();
            this.fileText = new System.Windows.Forms.Label();
            this.dropLabel = new System.Windows.Forms.Label();
            this.dropPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dropPanel
            // 
            this.dropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dropPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dropPanel.Controls.Add(this.dropLabel);
            this.dropPanel.Controls.Add(this.fileText);
            this.dropPanel.Location = new System.Drawing.Point(13, 13);
            this.dropPanel.Name = "dropPanel";
            this.dropPanel.Size = new System.Drawing.Size(426, 194);
            this.dropPanel.TabIndex = 0;
            // 
            // fileText
            // 
            this.fileText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fileText.AutoSize = true;
            this.fileText.Location = new System.Drawing.Point(206, 106);
            this.fileText.Name = "fileText";
            this.fileText.Size = new System.Drawing.Size(10, 13);
            this.fileText.TabIndex = 4;
            this.fileText.Text = " ";
            this.fileText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dropLabel
            // 
            this.dropLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dropLabel.AutoSize = true;
            this.dropLabel.Location = new System.Drawing.Point(123, 89);
            this.dropLabel.Name = "dropLabel";
            this.dropLabel.Size = new System.Drawing.Size(176, 13);
            this.dropLabel.TabIndex = 5;
            this.dropLabel.Text = "Drop your MVAR or MPVR file here.";
            this.dropLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 219);
            this.Controls.Add(this.dropPanel);
            this.MinimumSize = new System.Drawing.Size(300, 135);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "MVAR & MPVR Extractor";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainForm_DragOver);
            this.dropPanel.ResumeLayout(false);
            this.dropPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel dropPanel;
        private System.Windows.Forms.Label fileText;
        private System.Windows.Forms.Label dropLabel;

    }
}

