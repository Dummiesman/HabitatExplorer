using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HabitatExplorer
{
    public static class Helpers
    {
        public static string ReadCString(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            return (length != 0) ? new string(reader.ReadChars(length)) : string.Empty;
        }

        public static Vector3 ReadVector3D(BinaryReader reader)
        {
            double x = reader.ReadDouble();
            double y = reader.ReadDouble();
            double z = reader.ReadDouble();
            return new Vector3((float)x, (float)y, (float)z);
        }
    }
}
