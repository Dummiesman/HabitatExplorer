using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitat
{
    public class HabitatPropertyNameRecord : HabitatRecord
    {
        public BinaryData DefaultValue = new BinaryData();
        
        public double TopNumericValue = 1d;
        public double BottomNumericValue = 0d;

        public Color32 TopColorValue = new Color32(255, 255, 255);
        public Color32 BottomColorValue = new Color32(0, 0, 0);

        public HabitatPropertyNameRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            var reader = new BinaryReader(dataStream);

            int num1 = reader.ReadInt32(); //unknown

            DefaultValue = new BinaryData(owner, dataStream);
            TopNumericValue = reader.ReadDouble();
            BottomNumericValue = reader.ReadDouble();
            TopColorValue = new Color32(reader.ReadInt32());
            BottomColorValue = new Color32(reader.ReadInt32());
        }
    }
}
