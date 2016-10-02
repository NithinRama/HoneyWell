namespace HoneyWell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LabelMac = new System.Windows.Forms.Label();
            this.MacAddress = new System.Windows.Forms.RichTextBox();
            this.ApMacAddress = new System.Windows.Forms.Label();
            this.ApAddress = new System.Windows.Forms.RichTextBox();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelMac
            // 
            this.LabelMac.AutoSize = true;
            this.LabelMac.Location = new System.Drawing.Point(29, 9);
            this.LabelMac.Name = "LabelMac";
            this.LabelMac.Size = new System.Drawing.Size(78, 13);
            this.LabelMac.TabIndex = 0;
            this.LabelMac.Text = "Mac Address : ";
            // 
            // MacAddress
            // 
            this.MacAddress.Enabled = false;
            this.MacAddress.Location = new System.Drawing.Point(112, 6);
            this.MacAddress.Name = "MacAddress";
            this.MacAddress.Size = new System.Drawing.Size(348, 24);
            this.MacAddress.TabIndex = 1;
            this.MacAddress.Text = "";
            // 
            // ApMacAddress
            // 
            this.ApMacAddress.AutoSize = true;
            this.ApMacAddress.Location = new System.Drawing.Point(12, 52);
            this.ApMacAddress.Name = "ApMacAddress";
            this.ApMacAddress.Size = new System.Drawing.Size(95, 13);
            this.ApMacAddress.TabIndex = 2;
            this.ApMacAddress.Text = "AP Mac Address : ";
            // 
            // ApAddress
            // 
            this.ApAddress.Enabled = false;
            this.ApAddress.Location = new System.Drawing.Point(112, 52);
            this.ApAddress.Name = "ApAddress";
            this.ApAddress.Size = new System.Drawing.Size(348, 25);
            this.ApAddress.TabIndex = 3;
            this.ApAddress.Text = "";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(32, 102);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(197, 23);
            this.Start.TabIndex = 4;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(235, 102);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(215, 23);
            this.Stop.TabIndex = 5;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 139);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.ApAddress);
            this.Controls.Add(this.ApMacAddress);
            this.Controls.Add(this.MacAddress);
            this.Controls.Add(this.LabelMac);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "HoneyWell Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelMac;
        private System.Windows.Forms.RichTextBox MacAddress;
        private System.Windows.Forms.Label ApMacAddress;
        private System.Windows.Forms.RichTextBox ApAddress;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
    }
}

