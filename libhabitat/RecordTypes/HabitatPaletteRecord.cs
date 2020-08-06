using System.Collections.Generic;
using System.IO;

namespace Habitat
{
    public class HabitatPaletteRecord : HabitatRecord
    {
        public readonly List<Color32> Colors = new List<Color32>();

        public HabitatPaletteRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            var reader = new BinaryReader(dataStream);
            int numColors = reader.ReadUInt16();
            for(int i=0; i < numColors; i++)
            {
                int color = reader.ReadInt32();
                Colors.Add(new Color32(color));
            }
        }
    }
}
