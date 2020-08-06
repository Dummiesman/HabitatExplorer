namespace Habitat
{
    public class EnumValue
    {
        public string Name = string.Empty;
        public int Value = 0;
        public Color32 Color = new Color32(255, 255, 255);

        public EnumValue(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public EnumValue(string name, int value, Color32 color) : this(name, value)
        {
            this.Color = color;
        }
    }
}
