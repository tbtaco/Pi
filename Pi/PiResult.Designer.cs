namespace Pi
{
    partial class PiResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PiResult));
            this.uxTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // uxTextBox
            // 
            this.uxTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxTextBox.Location = new System.Drawing.Point(12, 12);
            this.uxTextBox.Multiline = true;
            this.uxTextBox.Name = "uxTextBox";
            this.uxTextBox.ReadOnly = true;
            this.uxTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uxTextBox.Size = new System.Drawing.Size(950, 835);
            this.uxTextBox.TabIndex = 0;
            this.uxTextBox.TabStop = false;
            this.uxTextBox.Text = "Loading...";
            // 
            // PiResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 861);
            this.Controls.Add(this.uxTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PiResult";
            this.Text = "Pi Calculator Results";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PiResult_FormClosing);
            this.Load += new System.EventHandler(this.PiResult_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox uxTextBox;
    }
}