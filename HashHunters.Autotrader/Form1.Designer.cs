﻿namespace HashHunters.Autotrader
{
    partial class Form1
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
            this.tbMarketName = new System.Windows.Forms.TextBox();
            this.lMarketName = new System.Windows.Forms.Label();
            this.bEnter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMarketName
            // 
            this.tbMarketName.Location = new System.Drawing.Point(94, 12);
            this.tbMarketName.Name = "tbMarketName";
            this.tbMarketName.Size = new System.Drawing.Size(100, 20);
            this.tbMarketName.TabIndex = 0;
            // 
            // lMarketName
            // 
            this.lMarketName.AutoSize = true;
            this.lMarketName.Location = new System.Drawing.Point(12, 15);
            this.lMarketName.Name = "lMarketName";
            this.lMarketName.Size = new System.Drawing.Size(71, 13);
            this.lMarketName.TabIndex = 1;
            this.lMarketName.Text = "Market Name";
            // 
            // bEnter
            // 
            this.bEnter.Location = new System.Drawing.Point(200, 7);
            this.bEnter.Name = "bEnter";
            this.bEnter.Size = new System.Drawing.Size(101, 28);
            this.bEnter.TabIndex = 2;
            this.bEnter.Text = "Enter to market";
            this.bEnter.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 449);
            this.Controls.Add(this.bEnter);
            this.Controls.Add(this.lMarketName);
            this.Controls.Add(this.tbMarketName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMarketName;
        private System.Windows.Forms.Label lMarketName;
        private System.Windows.Forms.Button bEnter;
    }
}
