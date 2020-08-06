using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace HabitatExplorer.Export
{
    public class ObjObject
    {
        public string Name = string.Empty;
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

        public ObjObject()
        {
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

    public class ObjExportHelper
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

        /// <summary>
        /// Adds a vertex
        /// </summary>
        /// <returns>The index of this new vertex, ready to be inserted into a face</returns>
        public int AddVertex(Vector3 vertex)
        {
            if(!vertexRemap.TryGetValue(vertex, out int index))
            {
                vertices.Add(vertex);
                vertexRemap[vertex] = vertices.Count;
                index = vertices.Count;
            }
            return index;
        }

        /// <summary>
        /// Adds a normal
        /// </summary>
        /// <returns>The index of this new normal, ready to be inserted into a face</returns>
        public int AddNormal(Vector3 normal)
        {
            if (!normalRemap.TryGetValue(normal, out int index))
            {
                normals.Add(normal);
                normalRemap[normal] = normals.Count;
                index = normals.Count;
            }
            return index;
        }

        /// <summary>
        /// Adds a UV
        /// </summary>
        /// <returns>The index of this new UV, ready to be inserted into a face</returns>
        public int AddUV(Vector2 uv)
        {
            if (!uvRemap.TryGetValue(uv, out int index))
            {
                uvs.Add(uv);
                uvRemap[uv] = uvs.Count;
                index = uvs.Count;
            }
            return index;
        }

        public void AddFace(int[] vertexIndices, int[] uvIndices, int[] normalIndices)
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

        public void AddFaceVN(int[] vertexIndices, int[] normalIndices)
        {
            AddFace(vertexIndices, null, normalIndices);
        }

        public void AddFaceVU(int[] vertexIndices, int[] uvIndices)
        {
            AddFace(vertexIndices, uvIndices, null);
        }

        public void AddFaceV(int[] vertexIndices)
        {
            AddFace(vertexIndices, null, null);
        }

        /// <summary>
        /// Sets the current material
        /// </summary>
        public void SetMaterial(string name)
        {
            if (name == lastMaterialName)
                return;
            lastMaterialName = name;
            if(currentObject != null)
            {
                currentObject.SetMaterial(name);
            }

        }
        /// <summary>
        /// Sets the current object we're exporting to
        /// </summary>
        public void SetObject(string name)
        {
            if (name == lastObjectName)
                return;
            if (!objects.TryGetValue(name, out currentObject))
            {
                currentObject = new ObjObject() { Name = name };
                objects[name] = currentObject;
                currentObject.SetMaterial(lastMaterialName);
            }
            lastObjectName = name;
        }

        public void SetDefaultObject()
        {
            SetObject("default");
        }

        //
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("# Exported with ObjExportHelper");

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

        public ObjExportHelper()
        {
            SetDefaultObject();
        }
    }
}
