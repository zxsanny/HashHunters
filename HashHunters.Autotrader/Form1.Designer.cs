namespace HashHunters.Autotrader
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
            this.bGetTicker = new System.Windows.Forms.Button();
            this.cbMarkets = new System.Windows.Forms.ComboBox();
            this.bGetSummary = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMarketName
            // 
            this.tbMarketName.Location = new System.Drawing.Point(94, 12);
            this.tbMarketName.Name = "tbMarketName";
            this.tbMarketName.Size = new System.Drawing.Size(100, 20);
            this.tbMarketName.TabIndex = 0;
            this.tbMarketName.Text = "ltc_btc";
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
            // bGetTicker
            // 
            this.bGetTicker.Location = new System.Drawing.Point(64, 54);
            this.bGetTicker.Name = "bGetTicker";
            this.bGetTicker.Size = new System.Drawing.Size(101, 28);
            this.bGetTicker.TabIndex = 2;
            this.bGetTicker.Text = "Get ticker";
            this.bGetTicker.UseVisualStyleBackColor = true;
            this.bGetTicker.Click += new System.EventHandler(this.bGetTicker_Click);
            // 
            // cbMarkets
            // 
            this.cbMarkets.FormattingEnabled = true;
            this.cbMarkets.Location = new System.Drawing.Point(318, 11);
            this.cbMarkets.Name = "cbMarkets";
            this.cbMarkets.Size = new System.Drawing.Size(121, 21);
            this.cbMarkets.TabIndex = 3;
            // 
            // bGetSummary
            // 
            this.bGetSummary.Location = new System.Drawing.Point(64, 88);
            this.bGetSummary.Name = "bGetSummary";
            this.bGetSummary.Size = new System.Drawing.Size(101, 28);
            this.bGetSummary.TabIndex = 4;
            this.bGetSummary.Text = "Get summary";
            this.bGetSummary.UseVisualStyleBackColor = true;
            this.bGetSummary.Click += new System.EventHandler(this.bGetSummary_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 747);
            this.Controls.Add(this.bGetSummary);
            this.Controls.Add(this.cbMarkets);
            this.Controls.Add(this.bGetTicker);
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
        private System.Windows.Forms.Button bGetTicker;
        private System.Windows.Forms.ComboBox cbMarkets;
        private System.Windows.Forms.Button bGetSummary;
    }
}

