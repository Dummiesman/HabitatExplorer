using System.Collections.Generic;
using System.IO;

namespace Habitat
{
    public struct FaceSide
    {
        public Color32 Color; 
        public int VertexIndex;
    }

    public struct VertexOrient
    {
        public int FirstFaceVertex;
        public int FirstTextureVertex;
        public bool Reversed;
    }

    public class Face
    {
        public readonly List<FaceSide> Sides = new List<FaceSide>();
        public readonly List<Property> Properties = new List<Property>();

        public HabitatRecordReference<HabitatTextureRecord> FrontTexture = new HabitatRecordReference<HabitatTextureRecord>();
        public HabitatRecordReference<HabitatTextureRecord> BackTexture = new HabitatRecordReference<HabitatTextureRecord>();

        public VertexOrient FrontOrient = default;
        public VertexOrient BackOrient = default;

        public double Reflectivity = 0d;
        public double Translucency = 0d;

        public byte[] UnkData = new byte[] { };

        public Face(HabitatDatabase owner, Stream dataStream)
        {
            var reader = new BinaryReader(dataStream);

            int sideCount = reader.ReadInt32();
            for(int i=0; i < sideCount; i++)
            {
                byte listClassType = reader.ReadByte();
                int color = reader.ReadInt32();
                int index = reader.ReadInt32();
                Sides.Add(new FaceSide() { Color = new Color32(color), VertexIndex = index });
            }

            int numProps = reader.ReadInt32();
            for (int i=0; i < numProps; i++)
            {
                byte listClassType = reader.ReadByte(); //should always be 0
                Properties.Add(new Property(owner, dataStream));
            }

            //read front and back textures
            int frontTextureObjectId = reader.ReadInt32();
            int backTextureObjectId = reader.ReadInt32();

            FrontTexture = new HabitatRecordReference<HabitatTextureRecord>(owner, frontTextureObjectId);
            BackTexture = new HabitatRecordReference<HabitatTextureRecord>(owner, backTextureObjectId);

            //Vertex orients
            FrontOrient = new VertexOrient()
            {
                FirstFaceVertex = reader.ReadUInt16(),
                FirstTextureVertex = reader.ReadUInt16(),
                Reversed = reader.ReadInt32() != 0
            };

            BackOrient = new VertexOrient()
            {
                FirstFaceVertex = reader.ReadUInt16(),
                FirstTextureVertex = reader.ReadUInt16(),
                Reversed = reader.ReadInt32() != 0
            };

            Reflectivity = reader.ReadDouble();
            Translucency = reader.ReadDouble();
        }
    }
}
