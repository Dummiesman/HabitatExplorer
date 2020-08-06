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
    public partial class PropertyNamePreviewControl : UserControl, IPreviewer
    {
        public PropertyNamePreviewControl()
        {
            InitializeComponent();
        }

        public void Destroy()
        {
        }

        private void SetDefaultLabelText(HabitatPropertyNameRecord record) 
        { 
            if(record.DefaultValue.Type == null)
            {
                defaultvalueTextBox.Text = "null";
            }
            else if(record.DefaultValue.Type == typeof(byte[]))
            {
                byte[] data = (byte[])record.DefaultValue.Value;
                defaultvalueTextBox.Text = Utils.ByteArrayToHexString(data);
            }
            else if(record.DefaultValue.Type == typeof(EnumPropertyValue))
            {
                var enumValueObj = (EnumPropertyValue)record.DefaultValue.Value;
                var enumRecord = enumValueObj.EnumRecord?.Value;
                string enumName = enumRecord?.Name ?? "ENUM_NOT_FOUND";
                string enumValue = string.IsNullOrEmpty(enumValueObj.Value) ? enumRecord?.Values.FirstOrDefault(x => x.Value == enumRecord.DefaultValue).Name ?? "ENUM_NOT_FOUND" :
                                                                              enumValueObj.Value;
                defaultvalueTextBox.Text = $"{enumName}.{enumValue}";
            }
            else
            {
                defaultvalueTextBox.Text = record.DefaultValue.Value.ToString();
            }
        }

        public void Initialize(HabitatRecord record)
        {
            recordTitleLabel.Text = record.Name;
            if(record is HabitatPropertyNameRecord propertyNameRecord)
            {
                SetDefaultLabelText(propertyNameRecord);

                minNumberValueLabel.Text = propertyNameRecord.BottomNumericValue.ToString();
                maxNumberValueLabel.Text = propertyNameRecord.TopNumericValue.ToString();
                minColorValueLabel.Text = propertyNameRecord.BottomColorValue.ToString();
                maxColorValueLabel.Text = propertyNameRecord.TopColorValue.ToString();
            }
        }
    }
}
