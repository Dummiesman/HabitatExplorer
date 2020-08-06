using System;
using System.IO;
using System.Text;

namespace Habitat
{
    public class Property
    {
        public HabitatRecordReference<HabitatRecord> NameRecord;
        public BinaryData Data = new BinaryData();
        public byte SwitchValue; //should be an enum but lazy

        public Property(HabitatDatabase owner, Stream dataStream)
        {
            var reader = new BinaryReader(dataStream);
            
            int nameRecordId = reader.ReadInt32();
            if (nameRecordId != 0)
                NameRecord = new HabitatRecordReference<HabitatRecord>(owner, nameRecordId);
            
            SwitchValue = reader.ReadByte();
            Data = new BinaryData(owner, dataStream);
            
            if(Data.Type != null)
                reader.ReadByte(); //always null terminated?
        }
    }
}
