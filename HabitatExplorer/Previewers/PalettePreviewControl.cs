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
using System.Drawing.Drawing2D;

namespace HabitatExplorer.Previewers
{
    public partial class PalettePreviewControl : UserControl, IPreviewer
    {
        private HabitatPaletteRecord palette;

        public PalettePreviewControl()
        {
            InitializeComponent();
        }

        public void Destroy() { }

        public void Initialize(HabitatRecord record)
        {
            palette = record as HabitatPaletteRecord;
            if(palette != null)
            {
                foreach(var color in palette.Colors)
                {
                    listBox.Items.Add(color);
                }
            }
        }

        private void PalettePreviewControl_Load(object sender, EventArgs e)
        {

        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
             e.DrawBackground();
            Graphics g = e.Graphics;

            //
            var item = listBox.Items[e.Index];
            Color brushColor = Color.White;
            if (item is Color32 color)
            {
                brushColor = color.ToDrawingColorNoAlpha();
            }
            Color foreColor = Color.FromArgb(255, 255 - brushColor.R, 255 - brushColor.G, 255 - brushColor.B);

            if(e.Index != listBox.SelectedIndex)
                g.FillRectangle(new SolidBrush(brushColor), e.Bounds);
            else
                g.FillRectangle(new HatchBrush(HatchStyle.LightUpwardDiagonal, foreColor, brushColor), e.Bounds);

            g.DrawString(item.ToString(), e.Font, new SolidBrush(foreColor), new PointF(e.Bounds.X, e.Bounds.Y));

            e.DrawFocusRectangle();
        }

        //Save Events
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            StringBuilder csvBuilder = new StringBuilder();
            csvBuilder.AppendLine($"r,g,b,a");
            foreach(var color in palette.Colors)
            {
                csvBuilder.AppendLine($"{color.R},{color.G},{color.B},{color.A}");
            }
            System.IO.File.WriteAllText(saveFileDialog.FileName, csvBuilder.ToString());
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = palette.Name;
            saveFileDialog.ShowDialog();
        }

        //Context Menu
        private void copyAsHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var color = (Color32)listBox.SelectedItem;
            
            byte[] colorBytes = new byte[] { color.R, color.G, color.B, color.A };
            Clipboard.SetText($"#{Utils.ByteArrayToHexString(colorBytes)}");
        }

        private void copyAsRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var color = (Color32)listBox.SelectedItem;
            Clipboard.SetText($"{color.R},{color.G},{color.B},{color.A}"); ;
        }

        //List Event
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox.Invalidate(); //redraw
        }
    }
}
