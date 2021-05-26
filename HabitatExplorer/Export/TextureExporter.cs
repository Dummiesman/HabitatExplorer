using Habitat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace HabitatExplorer.Export
{
    class TextureExporter : ExporterBase
    {
        public override void Export(string directory)
        {
            //
            HashSet<int> exportBitmaps = new HashSet<int>();

            //Bunch of helpers
            void addFace(HabitatTemplateRecord template, Face face, HabitatTextureRecord texture, bool backface)
            {
                var bitmap = texture?.Bitmap.Value;
                if (bitmap != null)
                    exportBitmaps.Add(bitmap.ObjectId);
            }

            void addFaceFront(HabitatTemplateRecord template, Face face)
            {
                addFace(template, face, face.FrontTexture.Value, false);
            }

            void addFaceBack(HabitatTemplateRecord template, Face face)
            {
                if (face.BackTexture.HasValue)
                    addFace(template, face, face.BackTexture.Value, true);
            }

            void gatherTemplateTextures(HabitatTemplateRecord template)
            {
                //export faces
                foreach (var face in template.Faces)
                {
                    addFaceFront(template, face);
                    addFaceBack(template, face);
                }

                //
                if (exportChildren)
                {
                    foreach (var child in template.ChildObjects)
                    {
                        if (child?.Value is HabitatTemplateRecord childTemplateRecord)
                        {
                            gatherTemplateTextures(childTemplateRecord);
                        }
                    }
                }
            }


            //Gather our textures
            if (this.model is HabitatTemplateRecord templateRecord)
            {
                gatherTemplateTextures(templateRecord);
            }
            else
            {
                foreach (var child in model.ChildObjects)
                {
                    if (child?.Value is HabitatTemplateRecord childTemplateRecord)
                    {
                        gatherTemplateTextures(childTemplateRecord);
                    }
                }
            }

            //Export them
            foreach(var objectId in exportBitmaps)
            {
                var bitmap = model.Database.GetRecordByObjectId<HabitatBitmapRecord>(objectId);
                string path = Path.Combine(directory, $"{bitmap.Name}.BMP");
                bitmap.SaveAsBitmap(path);
            }
        }



        //Constructors
        public TextureExporter(RVModel model) : base(model)
        {
        }

        public TextureExporter(RVModel model, bool exportChildren) : base(model, exportChildren)
        {
        }
    }
}
