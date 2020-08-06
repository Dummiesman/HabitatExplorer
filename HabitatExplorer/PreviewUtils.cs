using Habitat;
using HabitatExplorer.Previewers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HabitatExplorer
{
    public static class PreviewUtils
    {
        private static Dictionary<HabitatRecordType, Type> previewerTypes = new Dictionary<HabitatRecordType, Type>()
        {
            {HabitatRecordType.Palette , typeof(PalettePreviewControl) },
            {HabitatRecordType.Bitmap, typeof(BitmapPreviewControl) },
            {HabitatRecordType.Texture, typeof(TexturePreviewControl) },
            {HabitatRecordType.Project, typeof(NoPreviewControl) },
            {HabitatRecordType.Folder, typeof(NoPreviewControl) },
            {HabitatRecordType.Template, typeof(TemplatePreviewer) },
            {HabitatRecordType.Object, typeof(TemplatePreviewer) }
        };

        public static void ClearPreview(Control parent)
        {
            foreach (var control in parent.Controls)
            {
                if (control is IPreviewer previewerControl)
                {
                    previewerControl.Destroy();
                }
            }
            parent.Controls.Clear();
        }

        public static IPreviewer CreatePreview(HabitatRecord record, Control parent)
        {
            //
            ClearPreview(parent);

            //create previewer
            Type previewerType = null;
            if (!previewerTypes.TryGetValue(record.Type, out previewerType))
                previewerType = typeof(HexPreviewControl); //default fallback

            if (previewerType != null)
            {
                Control previewerControl = (Control)Activator.CreateInstance(previewerType);
                IPreviewer previewerInterface = previewerControl as IPreviewer;

                previewerControl.Parent = parent;
                previewerControl.Dock = DockStyle.Fill;

                previewerInterface.Initialize(record);

                return previewerInterface;
            }
            return null;
        }
    }
}
