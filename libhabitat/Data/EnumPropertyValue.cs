using System.IO;

namespace Habitat
{
    public class EnumPropertyValue
    {
        public HabitatRecordReference<HabitatRecord> EnumRecord;
        public string Value;
        
        public EnumPropertyValue(HabitatDatabase owner, Stream dataStream)
        {
            var reader = new BinaryReader(dataStream);
            Value = Helpers.ReadCString(reader);

            int recordObjectId = reader.ReadInt32();
            if (recordObjectId != 0)
                EnumRecord = new HabitatRecordReference<HabitatRecord>(owner, recordObjectId);
        }
    }
}
