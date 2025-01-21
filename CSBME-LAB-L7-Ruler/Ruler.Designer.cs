namespace L7_Ruler
{
    partial class Ruler
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
            this.button1 = new System.Windows.Forms.Button();
            this.vmax = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ppm = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.vmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppm)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(0, 0);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // vmax
            // 
            this.vmax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.vmax.Location = new System.Drawing.Point(204, 335);
            this.vmax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.vmax.Name = "vmax";
            this.vmax.Size = new System.Drawing.Size(100, 27);
            this.vmax.TabIndex = 1;
            this.vmax.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.vmax.ValueChanged += new System.EventHandler(this.vmax_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lungime (cm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "pixeli/mm";
            // 
            // ppm
            // 
            this.ppm.Location = new System.Drawing.Point(480, 332);
            this.ppm.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ppm.Name = "ppm";
            this.ppm.Size = new System.Drawing.Size(100, 31);
            this.ppm.TabIndex = 4;
            this.ppm.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ppm.ValueChanged += new System.EventHandler(this.ppm_ValueChanged);
            // 
            // Ruler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1974, 429);
            this.Controls.Add(this.ppm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vmax);
            this.Controls.Add(this.button1);
            this.Name = "Ruler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.vmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown vmax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ppm;
    }
}

