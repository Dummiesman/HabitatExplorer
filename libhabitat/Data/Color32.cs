using System;
using System.IO;
#if UNITY
using UnityEngine;
#else
using System.Numerics;
#endif

namespace Habitat
{
    public struct Color32
    {
        public byte R, G, B, A;

        public Color32(byte r, byte g, byte b, byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

#if !UNITY
        public System.Drawing.Color ToDrawingColorNoAlpha()
        {
            return System.Drawing.Color.FromArgb(255, R, G, B);
        }

        public System.Drawing.Color ToDrawingColor()
        {
            return System.Drawing.Color.FromArgb(A, R, G, B);
        }
#endif

        public Vector4 ToVector4()
        {
            return new Vector4(R / 255f, G / 255f, B / 255f, A / 255f);
        }

        public override int GetHashCode()
        {
            return R + (256 * G) + (512 * B) + (1024 * A);
        }

        public override string ToString()
        {
            return $"({R},{G},{B},{A})";
        }

        public Color32(byte r, byte g, byte b) : this(r, g, b, 255)
        {
        }

        public Color32(Stream dataStream)
        {
            this.R = (byte)dataStream.ReadByte();
            this.G = (byte)dataStream.ReadByte();
            this.B = (byte)dataStream.ReadByte();
            this.A = (byte)dataStream.ReadByte();
        }

        public Color32(int bytes)
        {
            byte[] splitBytes = BitConverter.GetBytes(bytes);
            this.R = splitBytes[0];
            this.G = splitBytes[1];
            this.B = splitBytes[2];
            this.A = splitBytes[3];
        }
    }
}
