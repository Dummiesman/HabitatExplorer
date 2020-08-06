namespace HabitatExplorer.Previewers
{
    partial class TexturePreviewControl
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
            this.notFoundLabel = new System.Windows.Forms.Label();
            this.bitmapPreviewControl = new HabitatExplorer.Previewers.BitmapPreviewControl();
            this.SuspendLayout();
            // 
            // notFoundLabel
            // 
            this.notFoundLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notFoundLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notFoundLabel.Location = new System.Drawing.Point(0, 0);
            this.notFoundLabel.Name = "notFoundLabel";
            this.notFoundLabel.Size = new System.Drawing.Size(690, 481);
            this.notFoundLabel.TabIndex = 0;
            this.notFoundLabel.Text = "No Bitmap Associated With This Texture";
            this.notFoundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bitmapPreviewControl
            // 
            this.bitmapPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bitmapPreviewControl.Location = new System.Drawing.Point(0, 0);
            this.bitmapPreviewControl.Name = "bitmapPreviewControl";
            this.bitmapPreviewControl.Size = new System.Drawing.Size(690, 481);
            this.bitmapPreviewControl.TabIndex = 1;
            // 
            // TexturePreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.notFoundLabel);
            this.Controls.Add(this.bitmapPreviewControl);
            this.Name = "TexturePreviewControl";
            this.Size = new System.Drawing.Size(690, 481);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label notFoundLabel;
        private BitmapPreviewControl bitmapPreviewControl;
    }
}
