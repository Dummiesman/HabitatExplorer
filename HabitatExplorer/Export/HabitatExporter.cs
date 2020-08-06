using Habitat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace HabitatExplorer.Export
{
    class HabitatExporter
    {
        private bool exportChildren = true;
        private RVModel model;

        public void ExportTextures(string directory)
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

        public void Export(string path)
        {
            //Our builder
            var builder = new ObjExportHelper();

            //Bunch of helpers
            void addFace(HabitatTemplateRecord template, Face face, HabitatTextureRecord texture, bool backface)
            {
                if (face.Sides.Count < 3 || face.Sides.Count > 4)
                    return;

                //First and foremost, get our position
                Vector3 ourPosition = new Vector3(0f, 0f, 0f);
                HabitatTemplateRecord parent = template.Parent.Value as HabitatTemplateRecord;
                if (parent != null)
                {
                    int parentPin = template.Anchor.PinId;
                    var parentOffset = template.Anchor.AnchorPos;
                    if (parent.PinVertexIndices.TryGetValue(parentPin, out int parentPinVertexIndex))
                    {
                        var parentPinVertex = parent.Vertices[parentPinVertexIndex];
                        ourPosition = new Vector3(parentPinVertex.X + parentOffset.X, parentPinVertex.Y + parentOffset.Y, parentPinVertex.Z + parentOffset.Z);
                    }
                }
                

                var bitmap = texture?.Bitmap.Value;
                string materialName = (bitmap != null) ? bitmap.Name : "default";
                builder.SetMaterial(materialName);

                List<Vector3> facePoints = new List<Vector3>();
                Vector2[] faceUvs = new Vector2[face.Sides.Count];
                               
                foreach (var side in face.Sides)
                {
                    int rebasedVertexIndex = side.VertexIndex - 1;
                    facePoints.Add(template.Vertices[rebasedVertexIndex]);
                }

                if (texture != null)
                {
                    int txi = face.FrontOrient.FirstFaceVertex;
                    int txj = face.FrontOrient.FirstTextureVertex;
                    int txInc = face.FrontOrient.Reversed ? face.Sides.Count - 1 : 1;
                    for (int i = 0; i < face.Sides.Count; i++)
                    {
                        int sourceIdx = txj % texture.UVs.Count;
                        int dstIdx = txi % face.Sides.Count;

                        var source = texture.UVs[texture.UVs.Count - sourceIdx - 1];
                        faceUvs[dstIdx] = source;

                        txj++;
                        txi += txInc;
                    }
                }

                //reverse if backface
                if (backface)
                {
                    facePoints.Reverse();
                    Array.Reverse(faceUvs);
                }

                //get indices
                int[] faceIndices = new int[facePoints.Count];
                int[] uvIndices = new int[faceUvs.Length];
                for (int i = 0; i < facePoints.Count; i++)
                    faceIndices[i] = builder.AddVertex(new Vector3((facePoints[i] + ourPosition).X * -1f, (facePoints[i] + ourPosition).Y, (facePoints[i] + ourPosition).Z));
                for (int i = 0; i < faceUvs.Length; i++)
                    uvIndices[i] = builder.AddUV(new Vector2(faceUvs[i].X, 1f - faceUvs[i].Y));

                //since we flipped on the X axis, we must do this
                Array.Reverse(faceIndices);
                Array.Reverse(uvIndices);

                builder.AddFaceVU(faceIndices, uvIndices);
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

            void exportTemplate(HabitatTemplateRecord template)
            {
                //set object
                builder.SetObject($"{template.ObjectId}_{template.Name}");

                //export faces
                foreach(var face in template.Faces)
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
                            exportTemplate(childTemplateRecord);
                        }
                    }
                }
            }
          

            //Do the actual export
            if(this.model is HabitatTemplateRecord templateRecord)
            {
                exportTemplate(templateRecord);
            }
            else
            {
                foreach(var child in model.ChildObjects)
                {
                    if(child?.Value is HabitatTemplateRecord childTemplateRecord)
                    {
                        exportTemplate(childTemplateRecord);
                    }
                }
            }

            //Write it
            File.WriteAllText(path, builder.ToString());
        }


        public HabitatExporter(RVModel model) : this(model, true)
        {
        }

        public HabitatExporter(RVModel model, bool exportChildren)
        {
            this.model = model;
            this.exportChildren = exportChildren;
        }
    }
}
