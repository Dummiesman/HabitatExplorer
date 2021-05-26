using System;
using System.Collections.Generic;
using System.IO;

namespace Habitat
{
    public class HabitatBitmapRecord : HabitatPropertyRecord
    {
        public BITMAPINFOHEADER Header;
        public byte[] ImageData;
        public List<HabitatRecordReference<HabitatTextureRecord>> TextureReferences = new List<HabitatRecordReference<HabitatTextureRecord>>();
        public HabitatRecordReference<HabitatRecord> Palette;

        //Bitmap Writing Helpers
        int PaletteSize(BITMAPINFOHEADER info)
        {
            return (DIBNumColors(info) * 4);
        }

        int DIBNumColors(BITMAPINFOHEADER info)
        {
            int wBitCount;
            int dwClrUsed;

            dwClrUsed = info.biClrUsed;

            if (dwClrUsed != 0)
                return dwClrUsed;

            wBitCount = info.biBitCount;

            switch (wBitCount)
            {
                case 1: return 2;
                case 4: return 16;
                case 8: return 256;
                default: return 0;
            }
        }

        private int CalculateBitmapOffset()
        {
            //File header + Info header + Pal size
            return 14 + 40 + PaletteSize(Header);
        }

        //
        public void SaveAsBitmap(Stream stream)
        {
            var writer = new BinaryWriter(stream);
            writer.Write('B');
            writer.Write('M');
            writer.Write(14 + ImageData.Length);
            writer.Write(0); //reserved, 2 WORDs
            writer.Write(CalculateBitmapOffset());
            writer.Write(ImageData);
        }

        public void SaveAsBitmap(string path)
        {
            if (File.Exists(path))
                File.Delete(path);

            using(var stream = File.OpenWrite(path))
            {
                SaveAsBitmap(stream);
            }
        }

  
        public HabitatBitmapRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            using(var reader = new BinaryReader(dataStream))
            {
                byte dataType = reader.ReadByte(); //Should be TokBinary (0x0A) or NULL (0x00)
                if(dataType != 0x0A && dataType != 0)
                {
                    throw new Exception("Image has bad data type??");
                }

                if (dataType != 0)
                {
                    int dataLength = reader.ReadInt32();
                    ImageData = reader.ReadBytes(dataLength);
                    Header = BITMAPINFOHEADER.FromBytes(ImageData, 0);
                }

                //get palette
                int paletteObjectId = reader.ReadInt32();
                if(paletteObjectId != 0)
                    Palette = new HabitatRecordReference<HabitatRecord>(owner, paletteObjectId);
                
                //get texture refs
                int textureReferenceCount = reader.ReadInt32();
                for(int i=0; i < textureReferenceCount; i++)
                {
                    byte listDataType = reader.ReadByte(); // should be 0
                    int textureObjectId = reader.ReadInt32();
                    TextureReferences.Add(new HabitatRecordReference<HabitatTextureRecord>(owner, textureObjectId));
                }
            }
        }
    }
}
