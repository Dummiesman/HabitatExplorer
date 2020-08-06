using System.Collections.Generic;
using System.IO;

namespace Habitat
{
    public class HabitatPropertyRecord : HabitatRecord
    {
        public List<Property> Properties = new List<Property>();

        public HabitatPropertyRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            var reader = new BinaryReader(dataStream);
            
            int numProperties = reader.ReadInt32();
            for (int i = 0; i < numProperties; i++)
            {
                byte listDataType = reader.ReadByte(); //should always be 0
                var property = new Property(owner, dataStream);
                Properties.Add(property);
            }
        }
    }
}
