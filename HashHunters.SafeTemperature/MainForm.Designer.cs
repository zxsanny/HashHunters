namespace HashHunters.SafeTemperature
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
            this.nuMaxTemp = new System.Windows.Forms.NumericUpDown();
            this.lAlert = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // nuMaxTemp
            // 
            this.nuMaxTemp.Location = new System.Drawing.Point(106, 12);
            this.nuMaxTemp.Name = "nuMaxTemp";
            this.nuMaxTemp.Size = new System.Drawing.Size(82, 20);
            this.nuMaxTemp.TabIndex = 1;
            this.nuMaxTemp.Value = new decimal(new int[] {
            78,
            0,
            0,
            0});
            // 
            // lAlert
            // 
            this.lAlert.AutoSize = true;
            this.lAlert.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lAlert.ForeColor = System.Drawing.Color.Red;
            this.lAlert.Location = new System.Drawing.Point(360, 25);
            this.lAlert.Name = "lAlert";
            this.lAlert.Size = new System.Drawing.Size(149, 31);
            this.lAlert.TabIndex = 2;
            this.lAlert.Text = "ALARMA!!!";
            this.lAlert.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 380);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lAlert);
            this.Controls.Add(this.nuMaxTemp);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxTemp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown nuMaxTemp;
        private System.Windows.Forms.Label lAlert;
        private System.Windows.Forms.TextBox textBox1;
    }
}

