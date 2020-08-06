namespace HabitatExplorer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.recordCountLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.projectStructureTreeView = new System.Windows.Forms.TreeView();
            this.projectContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.preivewInNewWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportAsBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRawDataToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewIcons = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.projectTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.projectRecordsListView = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.previewGroupBox = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.projectInfoLabel = new System.Windows.Forms.ToolStripLabel();
            this.projectContextMenu.SuspendLayout();
            this.projectTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // recordCountLabel
            // 
            this.recordCountLabel.AutoSize = true;
            this.recordCountLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.recordCountLabel.Location = new System.Drawing.Point(0, 526);
            this.recordCountLabel.Name = "recordCountLabel";
            this.recordCountLabel.Size = new System.Drawing.Size(51, 13);
            this.recordCountLabel.TabIndex = 1;
            this.recordCountLabel.Text = "0 records";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "P3D Files|*.p3d";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // projectStructureTreeView
            // 
            this.projectStructureTreeView.ContextMenuStrip = this.projectContextMenu;
            this.projectStructureTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectStructureTreeView.FullRowSelect = true;
            this.projectStructureTreeView.HideSelection = false;
            this.projectStructureTreeView.ImageIndex = 0;
            this.projectStructureTreeView.ImageList = this.treeViewIcons;
            this.projectStructureTreeView.Location = new System.Drawing.Point(3, 3);
            this.projectStructureTreeView.Name = "projectStructureTreeView";
            this.projectStructureTreeView.SelectedImageIndex = 0;
            this.projectStructureTreeView.ShowNodeToolTips = true;
            this.projectStructureTreeView.Size = new System.Drawing.Size(208, 494);
            this.projectStructureTreeView.TabIndex = 7;
            this.projectStructureTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.projectStructureTreeView_AfterSelect);
            this.projectStructureTreeView.DoubleClick += new System.EventHandler(this.projectStructureTreeView_DoubleClick);
            // 
            // projectContextMenu
            // 
            this.projectContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preivewInNewWindowToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportAsBinaryToolStripMenuItem,
            this.copyRawDataToClipboardToolStripMenuItem});
            this.projectContextMenu.Name = "projectContextMenu";
            this.projectContextMenu.Size = new System.Drawing.Size(224, 76);
            // 
            // preivewInNewWindowToolStripMenuItem
            // 
            this.preivewInNewWindowToolStripMenuItem.Name = "preivewInNewWindowToolStripMenuItem";
            this.preivewInNewWindowToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.preivewInNewWindowToolStripMenuItem.Text = "Preivew in New Window";
            this.preivewInNewWindowToolStripMenuItem.Click += new System.EventHandler(this.preivewInNewWindowToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(220, 6);
            // 
            // exportAsBinaryToolStripMenuItem
            // 
            this.exportAsBinaryToolStripMenuItem.Name = "exportAsBinaryToolStripMenuItem";
            this.exportAsBinaryToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.exportAsBinaryToolStripMenuItem.Text = "Export as Binary";
            this.exportAsBinaryToolStripMenuItem.Click += new System.EventHandler(this.exportAsBinaryToolStripMenuItem_Click);
            // 
            // copyRawDataToClipboardToolStripMenuItem
            // 
            this.copyRawDataToClipboardToolStripMenuItem.Name = "copyRawDataToClipboardToolStripMenuItem";
            this.copyRawDataToClipboardToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.copyRawDataToClipboardToolStripMenuItem.Text = "Copy Hex Data To Clipboard";
            this.copyRawDataToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyRawDataToClipboardToolStripMenuItem_Click);
            // 
            // treeViewIcons
            // 
            this.treeViewIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewIcons.ImageStream")));
            this.treeViewIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewIcons.Images.SetKeyName(0, "bitmap.png");
            this.treeViewIcons.Images.SetKeyName(1, "Enum.png");
            this.treeViewIcons.Images.SetKeyName(2, "object.png");
            this.treeViewIcons.Images.SetKeyName(3, "Property.png");
            this.treeViewIcons.Images.SetKeyName(4, "PropertyName.png");
            this.treeViewIcons.Images.SetKeyName(5, "template.png");
            this.treeViewIcons.Images.SetKeyName(6, "Texture.png");
            this.treeViewIcons.Images.SetKeyName(7, "palette.png");
            this.treeViewIcons.Images.SetKeyName(8, "folder.png");
            this.treeViewIcons.Images.SetKeyName(9, "REVOLT.png");
            this.treeViewIcons.Images.SetKeyName(10, "project.png");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1095, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Selected Record Properties";
            // 
            // projectTabControl
            // 
            this.projectTabControl.Controls.Add(this.tabPage1);
            this.projectTabControl.Controls.Add(this.tabPage2);
            this.projectTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTabControl.Location = new System.Drawing.Point(0, 0);
            this.projectTabControl.Name = "projectTabControl";
            this.projectTabControl.SelectedIndex = 0;
            this.projectTabControl.Size = new System.Drawing.Size(222, 526);
            this.projectTabControl.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.projectStructureTreeView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(214, 500);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Project Structure";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.projectRecordsListView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(214, 500);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Raw Record List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // projectRecordsListView
            // 
            this.projectRecordsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.projectRecordsListView.ContextMenuStrip = this.projectContextMenu;
            this.projectRecordsListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.projectRecordsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectRecordsListView.FullRowSelect = true;
            this.projectRecordsListView.GridLines = true;
            this.projectRecordsListView.HideSelection = false;
            this.projectRecordsListView.Location = new System.Drawing.Point(3, 3);
            this.projectRecordsListView.MultiSelect = false;
            this.projectRecordsListView.Name = "projectRecordsListView";
            this.projectRecordsListView.Size = new System.Drawing.Size(208, 494);
            this.projectRecordsListView.TabIndex = 0;
            this.projectRecordsListView.UseCompatibleStateImageBehavior = false;
            this.projectRecordsListView.View = System.Windows.Forms.View.Details;
            this.projectRecordsListView.SelectedIndexChanged += new System.EventHandler(this.projectRecordsListView_SelectedIndexChanged);
            this.projectRecordsListView.DoubleClick += new System.EventHandler(this.projectRecordsListView_DoubleClick);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ID";
            this.columnHeader5.Width = 43;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date";
            this.columnHeader4.Width = 150;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.projectTabControl);
            this.mainSplitContainer.Panel1.Controls.Add(this.recordCountLabel);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.previewGroupBox);
            this.mainSplitContainer.Size = new System.Drawing.Size(919, 539);
            this.mainSplitContainer.SplitterDistance = 222;
            this.mainSplitContainer.TabIndex = 18;
            // 
            // previewGroupBox
            // 
            this.previewGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewGroupBox.Location = new System.Drawing.Point(0, 0);
            this.previewGroupBox.Name = "previewGroupBox";
            this.previewGroupBox.Size = new System.Drawing.Size(693, 539);
            this.previewGroupBox.TabIndex = 3;
            this.previewGroupBox.TabStop = false;
            this.previewGroupBox.Text = "Preview";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.helpToolStripButton,
            this.toolStripSeparator1,
            this.projectInfoLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(919, 25);
            this.toolStrip1.TabIndex = 19;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Visible = false;
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Visible = false;
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // projectInfoLabel
            // 
            this.projectInfoLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectInfoLabel.Name = "projectInfoLabel";
            this.projectInfoLabel.Size = new System.Drawing.Size(105, 22);
            this.projectInfoLabel.Text = "No Project Loaded";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 564);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Habitat Explorer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.projectContextMenu.ResumeLayout(false);
            this.projectTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel1.PerformLayout();
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label recordCountLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TreeView projectStructureTreeView;
        private System.Windows.Forms.ImageList treeViewIcons;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl projectTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel projectInfoLabel;
        private System.Windows.Forms.GroupBox previewGroupBox;
        private System.Windows.Forms.ListView projectRecordsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip projectContextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportAsBinaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyRawDataToClipboardToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ToolStripMenuItem preivewInNewWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

