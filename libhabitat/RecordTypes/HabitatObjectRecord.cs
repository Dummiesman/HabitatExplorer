using System.IO;

namespace Habitat
{
    public class HabitatObjectRecord : RVModel
    {
        public HabitatObjectRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
        }
    }
}
