using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Habitat;
using HabitatExplorer.Previewers;

namespace HabitatExplorer
{
    public partial class MainForm : Form
    {
        private const string APPTITLE = "Habitat Explorer";
        private readonly List<Form> OpenPreviewForms = new List<Form>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var cmdArgs = Environment.GetCommandLineArgs();
            if (cmdArgs.Length <= 1)
                return;
            string dbPath = cmdArgs[1];
            LoadFile(dbPath);
        }

        //Load new project
        void LoadFile(string path)
        {
            var db = new HabitatDatabase(path);

            //clear any old preview
            PreviewUtils.ClearPreview(previewGroupBox);

            //close all preview forms
            foreach(var form in OpenPreviewForms)
            {
                if (!form.IsDisposed)
                    form.Close();
            }
            OpenPreviewForms.Clear();
            
            //setup the raw list
            projectRecordsListView.Items.Clear();
            recordCountLabel.Text = $"{db.RecordCount} records total";
            foreach (var record in db.Records.OrderBy(x => x.ObjectId))
            {
                //ID T N S D
                var lvi = new ListViewItem(new[] { $"{record.ObjectId}", $"{record.Type}", record.Name, $"{record.RawData.Length}", $"{record.ModifiedDate}" });
                lvi.Tag = record;
                projectRecordsListView.Items.Add(lvi);
            }

            //setup app title
            string fileName = new FileInfo(path).Name;
            this.Text = $"{APPTITLE} - {fileName}";

            //setup project info labels
            var projectRecord = db.Records.FirstOrDefault(x => x.Type == HabitatRecordType.Project);
            projectInfoLabel.Text = $"{projectRecord.Name} - {projectRecord.ModifiedDate}";

            string projetDateString = (projectRecord != null) ? $"{projectRecord.ModifiedDate}" : "Can't Find Date";

            //test
            projectStructureTreeView.Nodes.Clear();
            Dictionary<int, TreeNode> folderNodes = new Dictionary<int, TreeNode>();
            
            void assignNodeImage(HabitatRecord record, TreeNode node)
            {
                switch (record.Type)
                {
                    case HabitatRecordType.Bitmap:
                        node.ImageIndex = 0;
                        break;
                    case HabitatRecordType.EnumRecord:
                        node.ImageIndex = 1;
                        break;
                    case HabitatRecordType.Object:
                        node.ImageIndex = 2;
                        break;
                    case HabitatRecordType.PropertyRecord:
                        node.ImageIndex = 3;
                        break;
                    case HabitatRecordType.PropertyName:
                        node.ImageIndex = 4;
                        break;
                    case HabitatRecordType.Template:
                        node.ImageIndex = 5;
                        break;
                    case HabitatRecordType.Texture:
                        node.ImageIndex = 6;
                        break;
                    case HabitatRecordType.Palette:
                        node.ImageIndex = 7;
                        break;
                    case HabitatRecordType.Folder:
                        node.ImageIndex = 8;
                        break;
                    case HabitatRecordType.Project:
                        node.ImageIndex = 10;
                        break;
                    default:
                        node.ImageIndex = 999;
                        break;
                }
                node.SelectedImageIndex = node.ImageIndex;
                node.ToolTipText = record.Type.ToString();
            }

            void addFolderRecursive(TreeNode parent, HabitatFolderRecord record)
            {
                var recordNode = (parent == null) ? projectStructureTreeView.Nodes.Add(record.Name) : parent.Nodes.Add(record.Name);
                recordNode.Tag = record;
                assignNodeImage(record, recordNode);

                foreach(var child in record.Children)
                {
                    var childValue = child.Value;
                    if(childValue.Type != HabitatRecordType.Folder)
                    {
                        var childNode = recordNode.Nodes.Add(childValue.Name);
                        childNode.Tag = childValue;
                        assignNodeImage(childValue, childNode);
                    }
                    else
                    {
                        addFolderRecursive(recordNode, childValue as HabitatFolderRecord);
                    }
                    
                }
            }
            addFolderRecursive(null, (HabitatProjectRecord)projectRecord);
            
        }

        //Previews and selection handler
        private void OpenPreviewWindow(HabitatRecord record)
        {
            var previewer = new PopoutPreviewWindow();
            previewer.Initialize(record);
            previewer.Show();
            OpenPreviewForms.Add(previewer);
        }

        private void DoubleClickHandler(HabitatRecord record)
        {
            OpenPreviewWindow(record);
        }

        private void RecordSelectHandler(HabitatRecord record)
        {
            PreviewUtils.CreatePreview(record, previewGroupBox);
        }

        private void projectRecordsListView_DoubleClick(object sender, EventArgs e)
        {
            var items = projectRecordsListView.SelectedItems;
            if (items.Count > 0 && items[0].Tag is HabitatRecord record)
                DoubleClickHandler(record);
        }

        private void projectStructureTreeView_DoubleClick(object sender, EventArgs e)
        {
            var node = projectStructureTreeView.SelectedNode;
            if (node.Tag is HabitatRecord record && record.Type != HabitatRecordType.Folder && record.Type != HabitatRecordType.Project)
                DoubleClickHandler(record);
        }


        private void projectStructureTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var record = e.Node.Tag as HabitatRecord;
            if(record != null)
            {
                RecordSelectHandler(record);
            }
        }

        private void projectRecordsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = projectRecordsListView.SelectedItems;
            if(selected.Count > 0)
            {
                if(selected[0].Tag is HabitatRecord record)
                {
                    RecordSelectHandler(record);
                }
            }
        }

        //File Dialog Handler
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            LoadFile(openFileDialog.FileName);
        }

        //Tool Strip stuff
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            string buildDate = HabitatExplorer.Properties.Resources.BuildDate;
            buildDate = buildDate.Trim();
            MessageBox.Show($"{APPTITLE}\nCreated by Dummiesman\nBuild date: {buildDate}\n\nMakes use of Helix Toolkit, Copyright (c) 2019 Helix Toolkit contributors\n\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.", $"About {APPTITLE}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Context Menu Stuff
        private object GetContextTarget()
        {
            if(projectTabControl.SelectedIndex == 1)
            {
                var items = projectRecordsListView.SelectedItems;
                return (items.Count > 0) ? items[0].Tag : null;
            }
            else
            {
                var node = projectStructureTreeView.SelectedNode;
                return (node == null) ? null : node.Tag;
            }
        }

        private void exportAsBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HabitatRecord record = GetContextTarget() as HabitatRecord;
            if(record != null)
            {
                string saveFile = $"{record.Name}.dat";
                File.WriteAllBytes(saveFile, record.RawData);
                MessageBox.Show($"Exported as {saveFile}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void copyRawDataToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HabitatRecord record = GetContextTarget() as HabitatRecord;
            if (record != null)
            {
                Clipboard.SetText(Utils.ByteArrayToHexString(record.RawData));
            }
        }

        private void preivewInNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HabitatRecord record = GetContextTarget() as HabitatRecord;
            if (record != null)
            {
                OpenPreviewWindow(record);   
            }
        }
    }
}
