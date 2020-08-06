namespace HabitatExplorer.Previewers
{
    partial class PalettePreviewControl
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
            this.components = new System.ComponentModel.Container();
            this.listBox = new System.Windows.Forms.ListBox();
            this.paletteContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyAsHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsRGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.paletteContextMenuStrip.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.ContextMenuStrip = this.paletteContextMenuStrip;
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox.FormattingEnabled = true;
            this.listBox.IntegralHeight = false;
            this.listBox.Location = new System.Drawing.Point(0, 0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(525, 426);
            this.listBox.TabIndex = 0;
            this.listBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // paletteContextMenuStrip
            // 
            this.paletteContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAsHexToolStripMenuItem,
            this.copyAsRGBToolStripMenuItem});
            this.paletteContextMenuStrip.Name = "paletteContextMenuStrip";
            this.paletteContextMenuStrip.Size = new System.Drawing.Size(159, 48);
            // 
            // copyAsHexToolStripMenuItem
            // 
            this.copyAsHexToolStripMenuItem.Name = "copyAsHexToolStripMenuItem";
            this.copyAsHexToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyAsHexToolStripMenuItem.Text = "Copy As Hex";
            this.copyAsHexToolStripMenuItem.Click += new System.EventHandler(this.copyAsHexToolStripMenuItem_Click);
            // 
            // copyAsRGBToolStripMenuItem
            // 
            this.copyAsRGBToolStripMenuItem.Name = "copyAsRGBToolStripMenuItem";
            this.copyAsRGBToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyAsRGBToolStripMenuItem.Text = "Copy as R,G,B,A";
            this.copyAsRGBToolStripMenuItem.Click += new System.EventHandler(this.copyAsRGBToolStripMenuItem_Click);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.saveButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 426);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(525, 27);
            this.bottomPanel.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Location = new System.Drawing.Point(0, 0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(525, 27);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save As...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Comma Separated Values|*.csv";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // PalettePreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.bottomPanel);
            this.Name = "PalettePreviewControl";
            this.Size = new System.Drawing.Size(525, 453);
            this.Load += new System.EventHandler(this.PalettePreviewControl_Load);
            this.paletteContextMenuStrip.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ContextMenuStrip paletteContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyAsHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsRGBToolStripMenuItem;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
