using Habitat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HabitatExplorer
{
    public partial class PopoutPreviewWindow : Form, IPreviewer
    {
        private const string FORMTITLE = "Popout Preview Window";

        public PopoutPreviewWindow()
        {
            InitializeComponent();
        }

        public void Destroy()
        {
            
        }

        public void Initialize(HabitatRecord record)
        {
            this.Text = $"{FORMTITLE} - {record.Name}";
            PreviewUtils.CreatePreview(record, this);
        }

        private void PopoutPreviewWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            PreviewUtils.ClearPreview(this);
        }

        private void PopoutPreviewWindow_Load(object sender, EventArgs e)
        {

        }

        private void PopoutPreviewWindow_KeyUp(object sender, KeyEventArgs e)
        {
            //Escape key, close form
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
