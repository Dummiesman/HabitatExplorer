using Habitat;

namespace HabitatExplorer
{
    public interface IPreviewer
    {
        void Initialize(HabitatRecord record);
        void Destroy();
    }
}
