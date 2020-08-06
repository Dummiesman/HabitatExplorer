using System.Collections.Generic;
using System.IO;
#if UNITY
using UnityEngine;
#else
using System.Numerics;
#endif

namespace Habitat
{
    /// <summary>
    /// Anchors an object to it's parent
    /// </summary>
    public class Anchor
    {
        public int PinId;
        public HabitatRecordReference<HabitatRecord> Template;
        public Vector3 UpVector = Vector3.UnitY;
        public Vector3 ForwardFector = Vector3.UnitZ;
        public Vector3 AnchorPos = Vector3.Zero;

        public Anchor(HabitatDatabase owner, Stream dataStream)
        {
            var reader = new BinaryReader(dataStream);
            PinId = reader.ReadInt32();

            int templateObjectId = reader.ReadInt32();
            if (templateObjectId != 0)
                Template = new HabitatRecordReference<HabitatRecord>(owner, templateObjectId);

            UpVector = Helpers.ReadVector3D(reader);
            ForwardFector = Helpers.ReadVector3D(reader);
            AnchorPos = Helpers.ReadVector3D(reader);
        }

        public Anchor() { }
    }

    public abstract class RVModel : HabitatPropertyRecord
    {
        public string OriginalName = string.Empty;
        public HabitatRecordReference<HabitatRecord> Parent = new HabitatRecordReference<HabitatRecord>();
        public Anchor Anchor;
        public Vector3 Scale;
        public List<HabitatRecordReference<HabitatRecord>> ChildObjects = new List<HabitatRecordReference<HabitatRecord>>();
        public List<HabitatRecordReference<HabitatRecord>> Links = new List<HabitatRecordReference<HabitatRecord>>();

        private List<byte[]> unknownLightData = new List<byte[]>();

        public RVModel(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            var reader = new BinaryReader(dataStream);

            OriginalName = Helpers.ReadCString(reader);

            int parentRecordObjectId = reader.ReadInt32();
            Parent = new HabitatRecordReference<HabitatRecord>(owner, parentRecordObjectId);

            Anchor = new Anchor(owner, dataStream);
            Scale = Helpers.ReadVector3D(reader);

            int childObjectsCount = reader.ReadInt32();
            for(int i=0; i < childObjectsCount; i++)
            {
                byte listItemType = reader.ReadByte(); //should be 0
                int childObjectId = reader.ReadInt32();
                if (childObjectId != 0)
                    ChildObjects.Add(new HabitatRecordReference<HabitatRecord>(owner, childObjectId));
            }

            int lightTypeCount = reader.ReadInt32();
            for(int i=0; i < lightTypeCount; i++)
            {
                byte listItemType = reader.ReadByte(); //should be 0
                unknownLightData.Add(reader.ReadBytes(91)); //todo: light structure
            }

            int linkCount = reader.ReadInt32();
            for(int i=0; i < linkCount; i++)
            {
                byte listItemType = reader.ReadByte(); //should be 0
                int linkObjectId = reader.ReadInt32();
                if (linkObjectId != 0)
                    Links.Add(new HabitatRecordReference<HabitatRecord>(owner, linkObjectId));
            }

            //don't know what this is yet
            reader.ReadBytes(4);
        }
    }
}
