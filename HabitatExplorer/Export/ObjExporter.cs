using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Habitat;
using System.IO;

namespace HabitatExplorer.Export
{
    public class ObjObject
    {
        public string Name { get; private set; } = string.Empty;
        public readonly Dictionary<string, List<ObjFace>> MaterialFaces = new Dictionary<string, List<ObjFace>>();

        public List<ObjFace> ActiveFaceBucket => activeFaceBucket;

        private string lastMaterialName = null;
        private List<ObjFace> activeFaceBucket = null;
        public void SetMaterial(string material)
        {
            if (material == lastMaterialName)
                return;
            if(!MaterialFaces.TryGetValue(material, out activeFaceBucket))
            {
                activeFaceBucket = new List<ObjFace>();
                MaterialFaces[material] = activeFaceBucket;
            }
            lastMaterialName = material;
        }
        public void SetDefaultMaterial()
        {
            SetMaterial("default");
        }

        public ObjObject(string name)
        {
            this.Name = name;
            SetDefaultMaterial();
        }
    }

    public class ObjFace
    {
        public readonly List<ObjFaceIndex> Loops = new List<ObjFaceIndex>();

        public override string ToString()
        {
            return string.Join(" ", Loops);
        }
    }

    public class ObjFaceIndex
    {
        public int VertexIndex = -1;
        public int TexCoordIndex = 1;
        public int NormalIndex = -1;

        public override string ToString()
        {
            if (VertexIndex < 0)
                return "";

            string value = VertexIndex.ToString();
            if(TexCoordIndex < 0 &&  NormalIndex >= 0)
            {
                value += $"//{NormalIndex}";
            }else if(TexCoordIndex >= 0 && NormalIndex < 0)
            {
                value += $"/{TexCoordIndex}";
            }
            else
            {
                value += $"/{TexCoordIndex}/{NormalIndex}";
            }
            return value;
        }
    }

    class ObjExporter : ExporterBase
    {
        private List<Vector3> vertices = new List<Vector3>();
        private List<Vector2> uvs = new List<Vector2>();
        private List<Vector3> normals = new List<Vector3>();

        private Dictionary<Vector3, int> vertexRemap = new Dictionary<Vector3, int>();
        private Dictionary<Vector2, int> uvRemap = new Dictionary<Vector2, int>();
        private Dictionary<Vector3, int> normalRemap = new Dictionary<Vector3, int>();

        private string lastMaterialName = "default";
        private string lastObjectName = null;
        private ObjObject currentObject;
        private Dictionary<string, ObjObject> objects = new Dictionary<string, ObjObject>();

        private int AddVertex(Vector3 vertex)
        {
            if(!vertexRemap.TryGetValue(vertex, out int index))
            {
                vertices.Add(vertex);
                vertexRemap[vertex] = vertices.Count;
                index = vertices.Count;
            }
            return index;
        }

        private int AddNormal(Vector3 normal)
        {
            if (!normalRemap.TryGetValue(normal, out int index))
            {
                normals.Add(normal);
                normalRemap[normal] = normals.Count;
                index = normals.Count;
            }
            return index;
        }

        private int AddUV(Vector2 uv)
        {
            if (!uvRemap.TryGetValue(uv, out int index))
            {
                uvs.Add(uv);
                uvRemap[uv] = uvs.Count;
                index = uvs.Count;
            }
            return index;
        }

        private void AddFace(int[] vertexIndices, int[] uvIndices, int[] normalIndices)
        {
            if (vertexIndices == null)
                throw new ArgumentException("Need at least vertex indices.");

            var face = new ObjFace();
            for(int i=0; i < vertexIndices.Length; i++)
            {
                var loop = new ObjFaceIndex();
                loop.VertexIndex = vertexIndices[i];
                loop.NormalIndex = (normalIndices != null && i < normalIndices.Length) ? normalIndices[i] : loop.NormalIndex;
                loop.TexCoordIndex = (uvIndices != null && i < uvIndices.Length) ? uvIndices[i] : loop.TexCoordIndex;
                face.Loops.Add(loop);
            }
            currentObject.ActiveFaceBucket.Add(face);
        }

        private void AddFaceVN(int[] vertexIndices, int[] normalIndices)
        {
            AddFace(vertexIndices, null, normalIndices);
        }

        private void AddFaceVU(int[] vertexIndices, int[] uvIndices)
        {
            AddFace(vertexIndices, uvIndices, null);
        }

        private void AddFaceV(int[] vertexIndices)
        {
            AddFace(vertexIndices, null, null);
        }

        private void SetMaterial(string name)
        {
            if (name == lastMaterialName)
                return;
            lastMaterialName = name;
            if(currentObject != null)
            {
                currentObject.SetMaterial(name);
            }
        }

        private void SetDefaultMaterial()
        {
            SetMaterial("default");
        }

        public void SetObject(string name)
        {
            if (name == lastObjectName)
                return;
            if (!objects.TryGetValue(name, out currentObject))
            {
                currentObject = new ObjObject(name);
                objects[name] = currentObject;
                currentObject.SetMaterial(lastMaterialName);
            }
            lastObjectName = name;
        }

        //
        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach(var vertex in vertices)
                builder.AppendLine($"v {vertex.X} {vertex.Y} {vertex.Z}");
            foreach (var normal in normals)
                builder.AppendLine($"vn {normal.X} {normal.Y} {normal.Z}");
            foreach (var uv in uvs)
                builder.AppendLine($"vt {uv.X} {uv.Y}");
            builder.AppendLine();

            foreach(var @object in objects.Values)
            {
                builder.AppendLine($"o {@object.Name}");
                foreach(var faceBucket in @object.MaterialFaces.Where(x => x.Value.Count > 0))
                {
                    builder.AppendLine($"usemtl {faceBucket.Key}");
                    foreach (var face in faceBucket.Value)
                    {
                        builder.AppendLine($"f {face.ToString()}");
                    }
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }

        //Habitat things
        private void AddFace(HabitatTemplateRecord template, Face face, HabitatTextureRecord texture, bool backface)
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
            SetMaterial(materialName);

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
                faceIndices[i] = AddVertex(new Vector3((facePoints[i] + ourPosition).X * -1f, (facePoints[i] + ourPosition).Y, (facePoints[i] + ourPosition).Z));
            for (int i = 0; i < faceUvs.Length; i++)
                uvIndices[i] = AddUV(new Vector2(faceUvs[i].X, 1f - faceUvs[i].Y));

            //since we flipped on the X axis, we must do this
            Array.Reverse(faceIndices);
            Array.Reverse(uvIndices);

            AddFaceVU(faceIndices, uvIndices);
        }

        private void AddFaceFront(HabitatTemplateRecord template, Face face)
        {
            AddFace(template, face, face.FrontTexture.Value, false);
        }

        private void AddFaceBack(HabitatTemplateRecord template, Face face)
        {
            if (face.BackTexture.HasValue)
                AddFace(template, face, face.BackTexture.Value, true);
        }

        private void AddToExport(HabitatTemplateRecord template)
        {
            //set object
            SetObject($"{template.ObjectId}_{template.Name}");

            //export faces
            foreach (var face in template.Faces)
            {
                AddFaceFront(template, face);
                AddFaceBack(template, face);
            }

            //
            if (exportChildren)
            {
                foreach (var child in template.ChildObjects)
                {
                    if (child?.Value is HabitatTemplateRecord childTemplateRecord)
                    {
                        AddToExport(childTemplateRecord);
                    }
                }
            }
        }

        //ExporterBase
        public override void Export(string path)
        {
            if (this.model is HabitatTemplateRecord templateRecord)
            {
                AddToExport(templateRecord);
            }
            else
            {
                foreach (var child in model.ChildObjects)
                {
                    if (child?.Value is HabitatTemplateRecord childTemplateRecord)
                    {
                        AddToExport(childTemplateRecord);
                    }
                }
            }

            //Write it
            File.WriteAllText(path, this.ToString());
        }

        //Constructors
        public ObjExporter(RVModel model) : this(model, true)
        {
        }

        public ObjExporter(RVModel model, bool exportChildren) : base(model, exportChildren)
        {
            SetDefaultMaterial();
        }
    }
}
