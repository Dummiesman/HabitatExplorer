using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using SharpGLTF.Geometry;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;
using Habitat;
using System.IO;
using SharpGLTF.Schema2;
using SharpGLTF.Scenes;

namespace HabitatExplorer.Export
{
    class GltfExporter : ExporterBase
    {
        private readonly Dictionary<string, MaterialBuilder> materials = new Dictionary<string, MaterialBuilder>();
        private MaterialBuilder currentMaterial;

        private readonly Dictionary<string, MeshBuilder<VertexPosition, VertexColor1Texture1, VertexEmpty>> meshes = new Dictionary<string, MeshBuilder<VertexPosition, VertexColor1Texture1, VertexEmpty>>();
        private MeshBuilder<VertexPosition, VertexColor1Texture1, VertexEmpty> currentMesh;

        private void SetMaterial(string name)
        {
            if (currentMaterial != null && currentMaterial.Name == name)
                return;
            
            if(!materials.TryGetValue(name, out currentMaterial))
            {
                currentMaterial = new MaterialBuilder(name);
                materials[name] = currentMaterial;
            }
        }

        private void SetDefaultMaterial()
        {
            SetMaterial("default");
        }

        private void SetMesh(string name)
        {
            if (currentMesh != null && currentMesh.Name == name)
                return;

            if (!meshes.TryGetValue(name, out currentMesh))
            {
                currentMesh = new MeshBuilder<VertexPosition, VertexColor1Texture1, VertexEmpty>(name);
                meshes[name] = currentMesh;
            }
        }

        private void AddFace(Face face, HabitatTemplateRecord template, HabitatTextureRecord texture, bool backface)
        {
            if (face.Sides.Count < 3 || face.Sides.Count > 4)
                return;

            var bitmap = texture?.Bitmap.Value;
            string materialName = (bitmap != null) ? bitmap.Name : "default";
            SetMaterial(materialName);

            List<Vector3> facePoints = new List<Vector3>(face.Sides.Count);
            List<Color32> faceColors = new List<Color32>(face.Sides.Count);
            Vector2[] faceUvs = new Vector2[face.Sides.Count];

            foreach (var side in face.Sides)
            {
                int rebasedVertexIndex = side.VertexIndex - 1;
                facePoints.Add(template.Vertices[rebasedVertexIndex]);
                faceColors.Add(side.Color);
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

            //reverse if NOT backface
            if (!backface)
            {
                facePoints.Reverse();
                faceColors.Reverse();
                Array.Reverse(faceUvs);
            }

            //add to mesh
            (VertexPosition, VertexColor1Texture1, VertexEmpty)[] gltfData = new (VertexPosition, VertexColor1Texture1, VertexEmpty)[face.Sides.Count];
            for (int i = 0; i < face.Sides.Count; i++)
            {
                gltfData[i] =
                (
                    new VertexPosition(new Vector3(facePoints[i].X  * -1f, facePoints[i].Y, facePoints[i].Z)),
                    new VertexColor1Texture1(faceColors[i].ToVector4(), faceUvs[i]),
                    new VertexEmpty()
                );
            }

            currentMesh.UsePrimitive(currentMaterial).AddTriangle(gltfData[0], gltfData[1], gltfData[2]);
            if (face.Sides.Count == 4)
                currentMesh.UsePrimitive(currentMaterial).AddTriangle(gltfData[2], gltfData[3], gltfData[0]);
        }

        private void AddFaceFront(HabitatTemplateRecord template, Face face)
        {
            AddFace(face, template, face.FrontTexture.Value, false);
        }

        private void AddFaceBack(HabitatTemplateRecord template, Face face)
        {
            if (face.BackTexture.HasValue)
                AddFace(face, template, face.BackTexture.Value, true);
        }

        private NodeBuilder AddToExport(SceneBuilder sceneBuilder, NodeBuilder parentNode, HabitatTemplateRecord template)
        {
            //create node
            var node = new NodeBuilder($"{template.ObjectId}_{template.Name}");
            if (parentNode != null) {
                parentNode.AddNode(node);
            }

            //First and foremost, get our position
            HabitatTemplateRecord parent = template.Parent.Value as HabitatTemplateRecord;
            Vector3 ourPosition = new Vector3(0f, 0f, 0f);
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
            node.UseTranslation().Value = new Vector3(ourPosition.X * -1f, ourPosition.Y, ourPosition.Z);

            //set object
            SetMesh($"{template.ObjectId}_{template.Name}");

            //export faces
            foreach (var face in template.Faces)
            {
                AddFaceFront(template, face);
                AddFaceBack(template, face);
            }

            //add to scene
            sceneBuilder.AddRigidMesh(currentMesh, node);

            //
            if (exportChildren)
            {
                foreach (var child in template.ChildObjects)
                {
                    if (child?.Value is HabitatTemplateRecord childTemplateRecord)
                    {
                        AddToExport(sceneBuilder, node, childTemplateRecord);
                    }
                }
            }

            //return node
            return node;
        }

        //ExporterBase
        public override void Export(string path)
        {
            var scene = new SceneBuilder();

            if (this.model is HabitatTemplateRecord templateRecord)
            {
                AddToExport(scene, null, templateRecord);
            }
            else
            {
                foreach (var child in model.ChildObjects)
                {
                    if (child?.Value is HabitatTemplateRecord childTemplateRecord)
                    {
                        AddToExport(scene, null, childTemplateRecord);
                    }
                }
            }

            //Write it
            scene.ToGltf2().SaveGLTF(path, new WriteSettings() { JsonIndented = true });
        }

        //Constructors
        public GltfExporter(RVModel model) : this(model, true)
        {
        }

        public GltfExporter(RVModel model, bool exportChildren) : base(model, exportChildren)
        {
            SetDefaultMaterial();
        }
    }
}
