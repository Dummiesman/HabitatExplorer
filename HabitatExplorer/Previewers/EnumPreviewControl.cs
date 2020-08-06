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

namespace HabitatExplorer.Previewers
{
    public partial class EnumPreviewControl : UserControl, IPreviewer
    {
        public EnumPreviewControl()
        {
            InitializeComponent();
        }

        public void Destroy()
        {
        }

        public void Initialize(HabitatRecord record)
        {
            if(record is HabitatEnumRecord enumRecord)
            {
                foreach(var value in enumRecord.Values)
                {
                    bool isDefault = (value.Value == enumRecord.DefaultValue);
                    Color backgroundColor = isDefault ? Color.LightSeaGreen : Color.White;
                    string itemName = isDefault ? $"{value.Name} (Default)" : value.Name;

                    var item = new ListViewItem(itemName)
                    {
                        UseItemStyleForSubItems = false,
                        BackColor = backgroundColor
                    };

                    var valuePart = item.SubItems.Add(value.Value.ToString());
                    valuePart.BackColor = backgroundColor;

                    var colorPart = item.SubItems.Add("");
                    colorPart.BackColor = value.Color.ToDrawingColorNoAlpha();

                    listView.Items.Add(item);
                }
            }

            //setup column sizes
            int sizeDiv3 = this.Width / 3;
            foreach (ColumnHeader column in listView.Columns)
                column.Width = sizeDiv3;
        }
    }
}
