namespace HabitatExplorer.Previewers
{
    partial class PropertyNamePreviewControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.recordTitleLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.maxColorValueLabel = new System.Windows.Forms.Label();
            this.minColorValueLabel = new System.Windows.Forms.Label();
            this.maxNumberValueLabel = new System.Windows.Forms.Label();
            this.minNumberValueLabel = new System.Windows.Forms.Label();
            this.defaultvalueTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // recordTitleLabel
            // 
            this.recordTitleLabel.AutoSize = true;
            this.recordTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recordTitleLabel.Location = new System.Drawing.Point(14, 17);
            this.recordTitleLabel.Name = "recordTitleLabel";
            this.recordTitleLabel.Size = new System.Drawing.Size(98, 13);
            this.recordTitleLabel.TabIndex = 0;
            this.recordTitleLabel.Text = "RECORD TITLE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Default Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Min Number Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Max Number Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Min Color Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Max Color Value";
            // 
            // maxColorValueLabel
            // 
            this.maxColorValueLabel.AutoSize = true;
            this.maxColorValueLabel.Location = new System.Drawing.Point(141, 210);
            this.maxColorValueLabel.Name = "maxColorValueLabel";
            this.maxColorValueLabel.Size = new System.Drawing.Size(88, 13);
            this.maxColorValueLabel.TabIndex = 6;
            this.maxColorValueLabel.Text = "255,255,255,255";
            // 
            // minColorValueLabel
            // 
            this.minColorValueLabel.AutoSize = true;
            this.minColorValueLabel.Location = new System.Drawing.Point(141, 187);
            this.minColorValueLabel.Name = "minColorValueLabel";
            this.minColorValueLabel.Size = new System.Drawing.Size(88, 13);
            this.minColorValueLabel.TabIndex = 7;
            this.minColorValueLabel.Text = "255,255,255,255";
            // 
            // maxNumberValueLabel
            // 
            this.maxNumberValueLabel.AutoSize = true;
            this.maxNumberValueLabel.Location = new System.Drawing.Point(141, 164);
            this.maxNumberValueLabel.Name = "maxNumberValueLabel";
            this.maxNumberValueLabel.Size = new System.Drawing.Size(31, 13);
            this.maxNumberValueLabel.TabIndex = 8;
            this.maxNumberValueLabel.Text = "5555";
            // 
            // minNumberValueLabel
            // 
            this.minNumberValueLabel.AutoSize = true;
            this.minNumberValueLabel.Location = new System.Drawing.Point(141, 142);
            this.minNumberValueLabel.Name = "minNumberValueLabel";
            this.minNumberValueLabel.Size = new System.Drawing.Size(13, 13);
            this.minNumberValueLabel.TabIndex = 9;
            this.minNumberValueLabel.Text = "0";
            // 
            // defaultvalueTextBox
            // 
            this.defaultvalueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultvalueTextBox.Location = new System.Drawing.Point(144, 43);
            this.defaultvalueTextBox.Multiline = true;
            this.defaultvalueTextBox.Name = "defaultvalueTextBox";
            this.defaultvalueTextBox.ReadOnly = true;
            this.defaultvalueTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.defaultvalueTextBox.Size = new System.Drawing.Size(231, 85);
            this.defaultvalueTextBox.TabIndex = 10;
            // 
            // PropertyNamePreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.defaultvalueTextBox);
            this.Controls.Add(this.minNumberValueLabel);
            this.Controls.Add(this.maxNumberValueLabel);
            this.Controls.Add(this.minColorValueLabel);
            this.Controls.Add(this.maxColorValueLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.recordTitleLabel);
            this.Name = "PropertyNamePreviewControl";
            this.Size = new System.Drawing.Size(400, 306);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label recordTitleLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label maxColorValueLabel;
        private System.Windows.Forms.Label minColorValueLabel;
        private System.Windows.Forms.Label maxNumberValueLabel;
        private System.Windows.Forms.Label minNumberValueLabel;
        private System.Windows.Forms.TextBox defaultvalueTextBox;
    }
}
