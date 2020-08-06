using System.IO;


namespace Habitat
{
    public class HabitatProjectRecord : HabitatFolderRecord
    {
        public HabitatProjectRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            //There's some additional things that a ProjectRecord has
            //Could potentially add this later
        }
    }
}
