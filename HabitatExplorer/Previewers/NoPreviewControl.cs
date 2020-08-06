using System.Windows.Forms;
using Habitat;

namespace HabitatExplorer.Previewers
{
    public partial class NoPreviewControl : UserControl, IPreviewer
    {
        public NoPreviewControl()
        {
            InitializeComponent();
        }

        public void Destroy() { }

        public void Initialize(HabitatRecord record)
        {
            //do nothing
        }
    }
}
