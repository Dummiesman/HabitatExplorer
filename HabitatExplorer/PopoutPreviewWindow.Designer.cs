namespace HabitatExplorer
{
    partial class PopoutPreviewWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopoutPreviewWindow));
            this.SuspendLayout();
            // 
            // PopoutPreviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 406);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "PopoutPreviewWindow";
            this.Text = "Popout Preview Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PopoutPreviewWindow_FormClosing);
            this.Load += new System.EventHandler(this.PopoutPreviewWindow_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PopoutPreviewWindow_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}