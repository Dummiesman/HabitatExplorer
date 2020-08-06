using Habitat;

namespace HabitatExplorer
{
    interface IPreviewer
    {
        void Initialize(HabitatRecord record);
        void Destroy();
    }
}
