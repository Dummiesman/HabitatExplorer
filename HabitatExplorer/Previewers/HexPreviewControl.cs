using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Habitat;
using Be.Windows.Forms;

namespace HabitatExplorer.Previewers
{
    public partial class HexPreviewControl : UserControl, IPreviewer
    {
        public HexPreviewControl()
        {
            InitializeComponent();
        }

        public void Destroy() { }

        public void Initialize(HabitatRecord record)
        {
            hexBox.ByteProvider = new DynamicByteProvider(record.RawData);
        }

        private void HexPreviewControl_Load(object sender, EventArgs e)
        {

        }
    }
}
