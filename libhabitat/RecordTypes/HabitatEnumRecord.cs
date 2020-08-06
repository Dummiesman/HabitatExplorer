using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitat
{
    public class HabitatEnumRecord : HabitatRecord
    {
        public readonly List<EnumValue> Values = new List<EnumValue>();
        public int DefaultValue = 0;

        public HabitatEnumRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            var reader = new BinaryReader(dataStream);

            int num1 = reader.ReadInt32();

            DefaultValue = reader.ReadInt32(); //assumed
            int count = reader.ReadInt32();

            for(int i=0; i < count; i++)
            {
                byte listItemType = reader.ReadByte(); //should be 0
                
                string name = Helpers.ReadCString(reader);
                int value = reader.ReadInt32();
                Values.Add(new EnumValue(name, value));
            }

            //colors are stored after? weird..
            for(int i=0; i < count; i++)
            {
                Values[i].Color = new Color32(reader.ReadInt32());
            }
        }
    }
}
