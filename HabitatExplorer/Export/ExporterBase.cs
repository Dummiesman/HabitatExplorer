using Habitat;

namespace HabitatExplorer.Export
{
    abstract class ExporterBase
    {
        protected RVModel model { get; private set; }
        protected bool exportChildren { get; private set; }

        public abstract void Export(string path);

        public ExporterBase(RVModel model) : this(model, true)
        {
        }

        public ExporterBase(RVModel model, bool exportChildren)
        {
            this.model = model;
            this.exportChildren = exportChildren;
        }
    }
}
