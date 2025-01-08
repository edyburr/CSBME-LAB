namespace waveform
{
    partial class Waveform
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
            this.btnUpDown = new System.Windows.Forms.Button();
            this.labelUpDown = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpDown
            // 
            this.btnUpDown.Location = new System.Drawing.Point(58, 64);
            this.btnUpDown.Name = "btnUpDown";
            this.btnUpDown.Size = new System.Drawing.Size(150, 50);
            this.btnUpDown.TabIndex = 0;
            this.btnUpDown.Text = "Up - Down";
            this.btnUpDown.UseVisualStyleBackColor = true;
            this.btnUpDown.Click += new System.EventHandler(this.btnUpDown_Click);
            // 
            // labelUpDown
            // 
            this.labelUpDown.AutoSize = true;
            this.labelUpDown.Location = new System.Drawing.Point(242, 77);
            this.labelUpDown.Name = "labelUpDown";
            this.labelUpDown.Size = new System.Drawing.Size(394, 25);
            this.labelUpDown.TabIndex = 1;
            this.labelUpDown.Text = "Apasă butonul pentru a începe afișarea.";
            this.labelUpDown.UseMnemonic = false;
            // 
            // Waveform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelUpDown);
            this.Controls.Add(this.btnUpDown);
            this.Name = "Waveform";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Waveform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpDown;
        private System.Windows.Forms.Label labelUpDown;
    }
}

