using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Habitat;
using System.IO;
using System.Numerics;

namespace HabitatExplorer.Previewers
{
    public partial class BitmapPreviewControl : UserControl, IPreviewer
    {
        private HabitatBitmapRecord bitmapRecord = null;
        private HabitatTextureRecord previewTexture = null;

        public void SetTexturePreviewRecord(HabitatTextureRecord preview)
        {
            previewTexture = preview;
            previewPictureBox.Invalidate();
        }

        public BitmapPreviewControl()
        {
            InitializeComponent();
        }

        public void Destroy() { }

        public void Initialize(HabitatRecord record)
        {
            bitmapRecord = record as HabitatBitmapRecord;
            if(bitmapRecord != null)
            {
                MemoryStream bitmapStream = new MemoryStream();
                bitmapRecord.SaveAsBitmap(bitmapStream);
                bitmapStream.Seek(0, SeekOrigin.Begin);

                Image bitmap = Bitmap.FromStream(bitmapStream);
                previewPictureBox.Image = bitmap;

                //get texture records
                textureRecordsListBox.ClearSelected();
                foreach(var textureRecordRef in bitmapRecord.TextureReferences)
                {
                    var textureRecord = textureRecordRef.Value;
                    textureRecordsListBox.Items.Add(textureRecord);
                }

                //set info
                infoLabel.Text = $"{record.Name} | {bitmapRecord.Header.biWidth}x{bitmapRecord.Header.biHeight} | {bitmapRecord.Header.biBitCount}bpp";
            }
        }

        public void SetSelectedTextureIndex(int index)
        {
            textureRecordsListBox.SelectedIndex = index;
        }

        public void SetSelectedTexture(HabitatTextureRecord textureRecord)
        {
            textureRecordsListBox.SelectedItem = textureRecord;
        }

        private void textureRecordsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTexturePreviewRecord((HabitatTextureRecord)textureRecordsListBox.SelectedItem);
        }

        private void previewPictureBox_Paint(object sender, PaintEventArgs e)
        {
            var bgPen = new Pen(Color.Cyan, 3f);
            var fgPen = Pens.Red;

            void drawUv(Vector2 start, Vector2 end)
            {
                float startPixelX = previewPictureBox.Image.Width * start.X;
                float startPixelY = previewPictureBox.Image.Height * start.Y;
                float endPixelX = previewPictureBox.Image.Width * end.X;
                float endPixelY = previewPictureBox.Image.Height * end.Y;

                e.Graphics.DrawLine(bgPen, startPixelX, startPixelY, endPixelX, endPixelY);
                e.Graphics.DrawLine(fgPen, startPixelX, startPixelY, endPixelX, endPixelY);
            }

            if (previewTexture != null)
            {
                for (int i = 0; i < previewTexture.UVs.Count - 1; i++)
                {
                    var uvA = previewTexture.UVs[i];
                    var uvB = previewTexture.UVs[i + 1];
                    drawUv(uvA, uvB);
                }
                drawUv(previewTexture.UVs[0], previewTexture.UVs[previewTexture.UVs.Count - 1]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(previewPictureBox.Image);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = bitmapRecord.Name;
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            System.Drawing.Imaging.ImageFormat format = (saveFileDialog.FilterIndex == 1) ? System.Drawing.Imaging.ImageFormat.Bmp
                                                                                          : System.Drawing.Imaging.ImageFormat.Png;
            previewPictureBox.Image.Save(saveFileDialog.FileName, format);
        }

        private void previewPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
