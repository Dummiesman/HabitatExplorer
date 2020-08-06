namespace Habitat
{
    public class HabitatRecordReference<T> where T : HabitatRecord
    {
        private HabitatDatabase database;
        public int ObjectId { get; private set; }

        public bool HasValue => (database != null && ObjectId != 0);
        public T Value
        {
            get
            {
                return (T)database.GetRecordByObjectId(ObjectId);
            }
        }

        /// <summary>
        /// Creates a null (not HasValue) reference
        /// </summary>
        public HabitatRecordReference()
        {

        }

        public HabitatRecordReference(HabitatDatabase database, int objectId)
        {
            this.database = database;
            this.ObjectId = objectId;
        }
    }
}
