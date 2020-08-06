using System;
using System.IO;

namespace Habitat
{
    public struct BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;

        public static BITMAPINFOHEADER FromBytes(byte[] bytes, int offset)
        {
            var infoHeader = new BITMAPINFOHEADER()
            {
                biSize = BitConverter.ToInt32(bytes, offset)
            };

            //read the rest
            infoHeader.biWidth = BitConverter.ToInt32(bytes, offset + 4);
            infoHeader.biHeight = BitConverter.ToInt32(bytes, offset + 8);
            infoHeader.biPlanes = BitConverter.ToInt16(bytes, offset + 12);
            infoHeader.biBitCount = BitConverter.ToInt16(bytes, offset + 14);
            infoHeader.biCompression = BitConverter.ToInt32(bytes, offset + 16);
            infoHeader.biSizeImage = BitConverter.ToInt32(bytes, offset + 20);
            infoHeader.biXPelsPerMeter = BitConverter.ToInt32(bytes, offset + 24);
            infoHeader.biYPelsPerMeter = BitConverter.ToInt32(bytes, offset + 28);
            infoHeader.biClrUsed = BitConverter.ToInt32(bytes, offset + 32);
            infoHeader.biClrImportant = BitConverter.ToInt32(bytes, offset + 36);

            return infoHeader;
        }

        public static BITMAPINFOHEADER FromBinary(BinaryReader reader)
        {
            var infoHeader = new BITMAPINFOHEADER()
            {
                biSize = reader.ReadInt32()
            };

            //vibe check
            if(infoHeader.biSize != 40)
            {
                throw new Exception($"Unexpected BITMAPINFOHEADER size. Expected 40, got {infoHeader.biSize}");
            }

            //read the rest
            infoHeader.biWidth = reader.ReadInt32();
            infoHeader.biHeight = reader.ReadInt32();
            infoHeader.biPlanes = reader.ReadInt16();
            infoHeader.biBitCount = reader.ReadInt16();
            infoHeader.biCompression = reader.ReadInt32();
            infoHeader.biSizeImage = reader.ReadInt32();
            infoHeader.biXPelsPerMeter = reader.ReadInt32();
            infoHeader.biYPelsPerMeter = reader.ReadInt32();
            infoHeader.biClrUsed = reader.ReadInt32();
            infoHeader.biClrImportant = reader.ReadInt32();

            return infoHeader;
        }
    }
}
