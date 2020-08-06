using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitat
{
    public class BinaryData
    {
        public object Value = null;
        public Type Type => Value?.GetType();

        //Debug function
        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// Create default null
        /// </summary>
        public BinaryData()
        {

        }

        public BinaryData(HabitatDatabase owner, Stream dataStream)
        {
            var reader = new BinaryReader(dataStream);
            byte valueDataType = reader.ReadByte();
            switch (valueDataType)
            {
                case 1:
                    Value = reader.ReadByte();
                    break;
                case 2:
                    Value = reader.ReadSByte();
                    break;
                case 3:
                    Value = reader.ReadUInt16();
                    break;
                case 4:
                    Value = reader.ReadInt16();
                    break;
                case 5:
                    Value = reader.ReadUInt32();
                    break;
                case 6:
                    Value = reader.ReadInt32();
                    break;
                case 7: //BOOL
                    Value = reader.ReadInt32() != 0; 
                    break;
                case 8:
                    Value = reader.ReadSingle();
                    break;
                case 9:
                    Value = reader.ReadDouble();
                    break;
                case 10: //Binary
                    {
                        int dataLen = reader.ReadInt32();
                        Value = reader.ReadBytes(dataLen);
                    }
                    break;
                case 11: //String
                    Value = Helpers.ReadCString(reader);
                    break;
                case 17: //EnumRef
                    Value = new EnumPropertyValue(owner, dataStream);
                    break;
                case 0: //Null
                    Value = null;
                    break;
                default:
                    throw new Exception($"Can't read property value with data type {valueDataType}, next 16 bytes are {ByteArrayToString(reader.ReadBytes(16))}");
                    break;
            }
        }
    }
}
