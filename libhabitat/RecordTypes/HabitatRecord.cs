using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitat
{
    public class HabitatRecord
    {
        public HabitatDatabase Database { get; private set; }
        public HabitatRecordReference<HabitatRecord> ParentFolder = new HabitatRecordReference<HabitatRecord>();
        public bool HasFolder => ParentFolder.HasValue;

        public int ObjectId = -1;
        public string Name = "";
        public HabitatRecordType Type = HabitatRecordType.Record;
        public byte[] RawData; //Todo: make this have a cleaner api?
        public DateTime ModifiedDate = default;

        public override string ToString()
        {
            return $"{ObjectId}:{Name}:{Type}({(int)Type})";
        }

        public HabitatRecord(HabitatDatabase owner, Stream dataStream) 
        {
            this.Database = owner;
            var reader = new BinaryReader(dataStream);
            
            int unkval = reader.ReadUInt16(); //unk value
                
            int numFolderRefs = reader.ReadInt32();
            for(int i=0; i < numFolderRefs; i++)
            {
                byte listType = reader.ReadByte(); //this should always be 0
                int folderId = reader.ReadInt32();
                if(folderId != 0) //This check is here just in case there are two folders, one valid, one null. We take the valid one.
                    ParentFolder = new HabitatRecordReference<HabitatRecord>(owner, folderId);
            }
            
        }
    }
}
