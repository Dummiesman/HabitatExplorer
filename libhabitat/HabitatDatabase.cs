﻿using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Habitat
{
    public enum HabitatRecordType
    {
        Record = 0,
        PropertyRecord = 1,
        Template = 2,
        Object = 3,
        PropertyName = 4,
        Texture = 5,
        TextureAnimation = 6,
        Bitmap = 7,
        Palette = 8,
        Folder = 9,
        Project = 10,
        ObsoleteTextureSet = 11,
        DialogTemplateRecord = 12,
        EnumRecord = 13,
        TextureSetFolder = 14,
        AnimatedTemplate = 15,
        AnimatedObject = 16,
        Link = 17,
        Model = 18,
        NewTextureSet = 19
    }

    public class HabitatDatabase
    {
        public int RecordCount => records.Count;
        public IEnumerable<HabitatRecord> Records => records.Values;
        private Dictionary<int, HabitatRecord> records = new Dictionary<int, HabitatRecord>();

        public void AddRecord(HabitatRecord record)
        {
            records.Add(record.ObjectId, record);
        }

        public bool ContainsRecord(int objectId)
        {
            return records.ContainsKey(objectId);
        }

        public bool ContainsRecord(HabitatRecord record)
        {
            return ContainsRecord(record.ObjectId);
        }

        public bool RemoveRecord(int objectId)
        {
            return records.Remove(objectId);
        }

        public bool RemoveRecord(HabitatRecord record)
        {
            return RemoveRecord(record.ObjectId);
        }

        public T GetRecordByObjectId<T>(int objectId) where T : HabitatRecord
        {
            if (records.TryGetValue(objectId, out HabitatRecord record))
                return (T)record;
            return null;
        }

        public HabitatRecord GetRecordByObjectId(int objectId)
        {
            if(records.TryGetValue(objectId, out HabitatRecord record))
                return record;
            return null;
        }

        private void AddRecordFromReader(OleDbDataReader reader)
        {
            int objectId = reader.GetInt32(0);
            string name = reader.GetString(1);
            HabitatRecordType type = (HabitatRecordType)reader.GetInt32(2);
            var date = reader.GetDateTime(4);
            byte[] data = (byte[])reader["Blob"];

            using (var dataStream = new MemoryStream(data))
            {
                HabitatRecord record = null;
                switch (type)
                {
                    case HabitatRecordType.PropertyName:
                        record = new HabitatPropertyNameRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                    case HabitatRecordType.EnumRecord:
                        record = new HabitatEnumRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                    case HabitatRecordType.Palette:
                        record = new HabitatPaletteRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                    case HabitatRecordType.Object:
                        record = new HabitatObjectRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                    case HabitatRecordType.Template:
                        record = new HabitatTemplateRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                    case HabitatRecordType.Project:
                        record = new HabitatProjectRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                    case HabitatRecordType.Folder:
                        record = new HabitatFolderRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                    case HabitatRecordType.Texture:
                        record = new HabitatTextureRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                    case HabitatRecordType.Bitmap:
                        record = new HabitatBitmapRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                    default:
                        record = new HabitatRecord(this, dataStream) { ModifiedDate = date, Name = name, ObjectId = objectId, RawData = data, Type = type };
                        break;
                }
                AddRecord(record);
            }
        }

        public HabitatDatabase(string path)
        {
            string myConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                                        $"Data Source={path};" +
                                         "Persist Security Info=True;";

            // Open OleDb Connection
            using(OleDbConnection myConnection = new OleDbConnection(myConnectionString))
            {
                // Open connection
                myConnection.Open();

                // Execute Queries
                OleDbCommand cmd = myConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `HABITAT`";
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AddRecordFromReader(reader);
                    }
                }
                reader.Close();
            }
            //done
        }
    }
}
