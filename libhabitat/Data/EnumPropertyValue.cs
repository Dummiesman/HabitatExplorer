using System.IO;

namespace Habitat
{
    public class EnumPropertyValue
    {
        public HabitatRecordReference<HabitatEnumRecord> EnumRecord = new HabitatRecordReference<HabitatEnumRecord>();
        public string Value;
        
        public EnumPropertyValue(HabitatDatabase owner, Stream dataStream)
        {
            var reader = new BinaryReader(dataStream);
            Value = Helpers.ReadCString(reader);

            int recordObjectId = reader.ReadInt32();
            EnumRecord = new HabitatRecordReference<HabitatEnumRecord>(owner, recordObjectId);
        }
    }
}
