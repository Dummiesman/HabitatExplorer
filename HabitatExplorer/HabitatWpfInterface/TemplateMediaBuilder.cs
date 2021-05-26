using Habitat;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace HabitatExplorer.HabitatWpfInterface
{
    /// <summary>
    /// Helper for building 3d objects from habitat meshes
    /// </summary>
    class TemplateMediaBuilder : IDisposable
    {
        //FOR HANDLES
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private List<IntPtr> bitmapHandles = new List<IntPtr>();

        //
        private HabitatTemplateRecord template;
        private bool positionAtParent = false;

        public List<Visual3D> Build(bool includeChildren)
        {
            //Ugh
            List<Visual3D> returnList = new List<Visual3D>();

            //First and foremost, get our position
            Vector3D ourPosition = new Vector3D(0d, 0d, 0d);
            if (positionAtParent)
            {
                HabitatTemplateRecord parent = template.Parent.Value as HabitatTemplateRecord;
                if(parent != null)
                {
                    int parentPin = template.Anchor.PinId;
                    var parentOffset = template.Anchor.AnchorPos;
                    if (parent.PinVertexIndices.TryGetValue(parentPin, out int parentPinVertexIndex))
                    {
                        var parentPinVertex = parent.Vertices[parentPinVertexIndex];
                        ourPosition = new Vector3D(parentPinVertex.X + parentOffset.X, parentPinVertex.Y + parentOffset.Y, parentPinVertex.Z + parentOffset.Z);
                    }
                }
            }
            var ourTransform = new Transform3DGroup() { Children = { new TranslateTransform3D(ourPosition), new ScaleTransform3D(-1d, 1d, 1d) } };

            // Create a mesh builder
            Dictionary<int, MeshBuilder> texturedMeshBuilders = new Dictionary<int, MeshBuilder>();
            var defaultMeshBuilder = new MeshBuilder(true, false);

            void addFace(Face face, HabitatTextureRecord texture, bool backface)
            {
                if (face.Sides.Count < 3 || face.Sides.Count > 4)
                    return;

                var bitmap = texture?.Bitmap.Value;
                if (bitmap != null && !texturedMeshBuilders.ContainsKey(bitmap.ObjectId))
                {
                    texturedMeshBuilders[bitmap.ObjectId] = new MeshBuilder(true, true);
                }

                var builder = (bitmap != null) ? texturedMeshBuilders[bitmap.ObjectId] : defaultMeshBuilder;
                List<Point3D> facePoints = new List<Point3D>();
                System.Windows.Point[] faceUvs = new System.Windows.Point[face.Sides.Count];

                foreach (var side in face.Sides)
                {
                    int rebasedVertexIndex = side.VertexIndex - 1;
                    var vertex = template.Vertices[rebasedVertexIndex];
                    facePoints.Add(new Point3D(vertex.X, vertex.Y, vertex.Z));
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
                        faceUvs[dstIdx] = new System.Windows.Point(source.X, source.Y);

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

                if (face.Sides.Count == 3)
                    builder.AddTriangle(facePoints[0], facePoints[1], facePoints[2], faceUvs[0], faceUvs[1], faceUvs[2]);
                else
                    builder.AddQuad(facePoints[0], facePoints[1], facePoints[2], facePoints[3], faceUvs[0], faceUvs[1], faceUvs[2], faceUvs[3]);
            }

            void addFaceFront(Face face)
            {
                addFace(face, face.FrontTexture.Value, false);
            }

            void addFaceBack(Face face)
            {
                if(face.BackTexture.HasValue)
                    addFace(face, face.BackTexture.Value, true);
            }

            foreach (var face in template.Faces)
            {
                addFaceFront(face);
                addFaceBack(face);
            }

            //add default to viewport
            if (defaultMeshBuilder.TriangleIndices.Count > 0)
            {
                var whiteMaterial = MaterialHelper.CreateMaterial(Colors.White);
                var defaultMesh = defaultMeshBuilder.ToMesh(true);
                var defaultVisual = new ModelVisual3D() { Content = new GeometryModel3D { Geometry = defaultMesh, Material = whiteMaterial }, Transform = ourTransform };

                returnList.Add(defaultVisual);
            }

            //add others
            foreach (var builder in texturedMeshBuilders)
            {
                HabitatBitmapRecord bitmapRecord = template.Database.GetRecordByObjectId<HabitatBitmapRecord>(builder.Key);
                MemoryStream bitmapMs = new MemoryStream();
                bitmapRecord.SaveAsBitmap(bitmapMs);
                bitmapMs.Seek(0, SeekOrigin.Begin);

                Image image = Image.FromStream(bitmapMs);
                var bitmap = new Bitmap(image);


                var hBitmap = bitmap.GetHbitmap();
                bitmapHandles.Add(hBitmap);

                var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap,
                                  IntPtr.Zero,
                                  System.Windows.Int32Rect.Empty,
                                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                bitmap.Dispose();

                //create material
                var brush = new ImageBrush(bitmapSource) { TileMode = TileMode.None };
                var material = new DiffuseMaterial(brush);
                var mesh = builder.Value.ToMesh();
                var visual = new ModelVisual3D() { Content = new GeometryModel3D { Geometry = mesh, Material = material }, Transform = ourTransform };
                returnList.Add(visual);
            }

            //build children if specified
            if (includeChildren)
            {
                foreach(var child in template.ChildObjects)
                {
                    HabitatTemplateRecord childTemplate = child?.Value as HabitatTemplateRecord;
                    if(childTemplate != null)
                    {
                        var childBuilder = new TemplateMediaBuilder(childTemplate, true);
                        returnList.AddRange(childBuilder.Build(includeChildren));
                        childBuilder.Dispose();
                    }
                }
            }

            return returnList;
        }

        public void Dispose()
        {
            foreach (var handle in bitmapHandles)
                DeleteObject(handle);
        }

        public TemplateMediaBuilder(HabitatTemplateRecord template, bool positionAtParent)
        {
            this.template = template;
            this.positionAtParent = positionAtParent;
        }

        public TemplateMediaBuilder(HabitatTemplateRecord template) : this(template, false)
        {
            
        }

    }
}
