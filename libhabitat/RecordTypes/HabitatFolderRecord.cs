using System.Collections.Generic;
using System.IO;

namespace Habitat
{
    public class HabitatFolderRecord : HabitatPropertyRecord
    {
        public List<HabitatRecordReference<HabitatRecord>> Children = new List<HabitatRecordReference<HabitatRecord>>();

        public HabitatFolderRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            var reader = new BinaryReader(dataStream);

            int numChildren = reader.ReadInt32();
            for(int i=0; i < numChildren; i++)
            {
                byte listDataType = reader.ReadByte(); // should be 0
                int childObjectId = reader.ReadInt32();
                if(childObjectId != 0)
                {
                    Children.Add(new HabitatRecordReference<HabitatRecord>(owner, childObjectId));
                }
            }
        }
    }
}
