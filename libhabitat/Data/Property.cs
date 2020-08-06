using System;
using System.IO;
using System.Text;

namespace Habitat
{

    public class Property
    {
        public HabitatRecordReference<HabitatRecord> NameRecord;
        public object Value;
        public byte SwitchValue; //should be an enum but lazy
        public Type Type => Value?.GetType();

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public Property(HabitatDatabase owner, Stream dataStream)
        {
            var reader = new BinaryReader(dataStream);
            
            int nameRecordId = reader.ReadInt32();
            if (nameRecordId != 0)
                NameRecord = new HabitatRecordReference<HabitatRecord>(owner, nameRecordId);
            SwitchValue = reader.ReadByte();

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
                case 7:
                    Value = reader.ReadInt32() != 0; //BOOL
                    break;
                case 8:
                    Value = reader.ReadSingle();
                    break;
                case 9:
                    Value = reader.ReadDouble();
                    break;
                case 11:
                    Value = Helpers.ReadCString(reader);
                    break;
                case 17:
                    Value = new EnumPropertyValue(owner, dataStream);
                    break;
                case 0:
                    Value = null;
                    break;
                default:
                    throw new Exception($"Can't read property value with data type {valueDataType}, next 16 bytes are {ByteArrayToString(reader.ReadBytes(16))}");
                    break;
            }

            if(valueDataType != 0)
                reader.ReadByte(); //always null terminated?
        }
    }
}
