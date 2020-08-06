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

        public HabitatEnumRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            var reader = new BinaryReader(dataStream);

            //these probably have some significance...
            int num1 = reader.ReadInt32();
            int num2 = reader.ReadInt32();

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
