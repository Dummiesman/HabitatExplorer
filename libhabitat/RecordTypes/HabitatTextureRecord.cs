using System;
using System.Collections.Generic;
using System.IO;
#if UNITY
using UnityEngine;
#else
using System.Numerics;
#endif

namespace Habitat
{
    public class HabitatTextureRecord : HabitatPropertyRecord
    {
        public HabitatRecordReference<HabitatBitmapRecord> Bitmap;
        public HabitatRecordReference<HabitatRecord> Palette;

        public float UOrigin = 0f;
        public float VOrigin = 0f;

        public List<Vector2> UVs = new List<Vector2>();

        public HabitatTextureRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            var reader = new BinaryReader(dataStream);

            int bitmapObjectId = reader.ReadInt32();
            if (bitmapObjectId != 0)
                Bitmap = new HabitatRecordReference<HabitatBitmapRecord>(owner, bitmapObjectId);

            int paletteObjectId = reader.ReadInt32();
            if (paletteObjectId != 0)
                Palette = new HabitatRecordReference<HabitatRecord>(owner, paletteObjectId);

            UOrigin = reader.ReadSingle(); //assumed
            VOrigin = reader.ReadSingle(); //assumed

            int uvCount = reader.ReadInt32();
            for(int i=0; i < uvCount; i++)
            {
                UVs.Add(new Vector2(reader.ReadSingle(), reader.ReadSingle()));
            }
        }
    }
}
