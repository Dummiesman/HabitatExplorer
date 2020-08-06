using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Habitat;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.Windows.Media;
using System.IO;
using HabitatExplorer.HabitatWpfInterface;
using HabitatExplorer.Export;

namespace HabitatExplorer.Previewers
{
    public partial class TemplatePreviewer : UserControl, IPreviewer
    {
        //
        private RVModel model;

        //
        private HelixViewport3D viewport => this.helix3DWrapper1.viewport;

        public TemplatePreviewer()
        {
            InitializeComponent();
        }


        private void MakeModelFromTemplate(HabitatTemplateRecord template)
        {
            var builder = new TemplateMediaBuilder(template);
            foreach(var built in builder.Build(true))
            {
                viewport.Children.Add(built);
            }
            builder.Dispose();
        }

        private void MakeModelFromObject(HabitatObjectRecord @object)
        {
            foreach(var child in @object.ChildObjects)
            {
                var childValue = child.Value;
                if(childValue is HabitatTemplateRecord template)
                {
                    var builder = new TemplateMediaBuilder(template);
                    foreach (var built in builder.Build(true))
                    {
                        viewport.Children.Add(built);
                    }
                    builder.Dispose();
                }
            }
        }

        public void Destroy() 
        {
            viewport.Children.Clear();
        }

        public void Initialize(HabitatRecord record)
        {
            //build our model
            if(record is HabitatTemplateRecord templateRecord)
            {
                MakeModelFromTemplate(templateRecord);
                infoLabel.Text = $"{record.Name} | Template | {templateRecord.Vertices.Count} vertices | {templateRecord.Faces.Count} faces | {templateRecord.ChildObjects.Count} direct children";
            }
            else if(record is HabitatObjectRecord objectRecord)
            {
                MakeModelFromObject(objectRecord);
                infoLabel.Text = $"{record.Name} | Object | {objectRecord.ChildObjects.Count} direct children";
            }

            //set our model
            if(record is RVModel modelRecord)
                this.model = modelRecord;

            //zoom out to see the whole modle
            viewport.ZoomExtents();
        }

        private void TemplatePreviewer_Load(object sender, EventArgs e)
        {
            viewport.IsHeadLightEnabled = true;
            viewport.ModelUpDirection = new Vector3D(0, 1, 0);
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = model.Name;
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            var exporter = new HabitatExporter(model);
            exporter.Export(saveFileDialog.FileName);

            DialogResult result = MessageBox.Show("Do you want to export textures?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                string directory = new FileInfo(saveFileDialog.FileName).Directory.FullName;
                exporter.ExportTextures(directory);
            }
        }
    }
}
