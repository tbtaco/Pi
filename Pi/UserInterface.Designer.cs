namespace Pi
{
    partial class UserInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.uxButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.uxPrecision = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.uxIterations = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.uxPrecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // uxButton
            // 
            this.uxButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxButton.Location = new System.Drawing.Point(12, 140);
            this.uxButton.Name = "uxButton";
            this.uxButton.Size = new System.Drawing.Size(500, 61);
            this.uxButton.TabIndex = 0;
            this.uxButton.Text = "Calculate Pi";
            this.uxButton.UseVisualStyleBackColor = true;
            this.uxButton.Click += new System.EventHandler(this.button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 42);
            this.label2.TabIndex = 2;
            this.label2.Text = "Precision:";
            // 
            // uxPrecision
            // 
            this.uxPrecision.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxPrecision.Location = new System.Drawing.Point(200, 12);
            this.uxPrecision.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.uxPrecision.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxPrecision.Name = "uxPrecision";
            this.uxPrecision.Size = new System.Drawing.Size(312, 49);
            this.uxPrecision.TabIndex = 3;
            this.uxPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uxPrecision.Value = new decimal(new int[] {
            2474,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 42);
            this.label1.TabIndex = 4;
            this.label1.Text = "Iterations:";
            // 
            // uxIterations
            // 
            this.uxIterations.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxIterations.Location = new System.Drawing.Point(200, 76);
            this.uxIterations.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.uxIterations.Name = "uxIterations";
            this.uxIterations.Size = new System.Drawing.Size(312, 49);
            this.uxIterations.TabIndex = 5;
            this.uxIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uxIterations.Value = new decimal(new int[] {
            7777,
            0,
            0,
            0});
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 215);
            this.Controls.Add(this.uxIterations);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uxPrecision);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uxButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserInterface";
            this.Text = "Pi Calculator Controls";
            this.Load += new System.EventHandler(this.UserInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uxPrecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxIterations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown uxPrecision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown uxIterations;
    }
}

