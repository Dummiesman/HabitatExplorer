using System.Windows.Forms;
using Habitat;

namespace HabitatExplorer.Previewers
{
    public partial class TexturePreviewControl : UserControl, IPreviewer
    {
        public TexturePreviewControl()
        {
            InitializeComponent();
        }

        public void Destroy() { }

        public void Initialize(HabitatRecord record)
        {
            if(record is HabitatTextureRecord textureRecord)
            {
                //can't render anything here
                if(!textureRecord.Bitmap.HasValue)
                {
                    return;
                }

                //show it
                notFoundLabel.Visible = false;

                var bitmap = textureRecord.Bitmap.Value;
                bitmapPreviewControl.Initialize(bitmap);

                //select our record in the list if we can
                if(bitmap.TextureReferences.FindIndex(x => x.ObjectId == textureRecord.ObjectId) >= 0)
                {
                    bitmapPreviewControl.SetSelectedTexture(textureRecord);
                }
                else
                {
                    bitmapPreviewControl.SetTexturePreviewRecord(textureRecord);
                }
            }
        }
    }
}
