using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY
using UnityEngine;
#else
using System.Numerics;
#endif

namespace Habitat
{
    public class HabitatTemplateRecord : RVModel
    {
        /// <summary>
        /// Maps PinID to an index in the Vertices list
        /// </summary>
        public readonly Dictionary<int, int> PinVertexIndices = new Dictionary<int, int>();

        public readonly List<Vector3> Vertices = new List<Vector3>();
        public readonly List<Face> Faces = new List<Face>();

        public HabitatTemplateRecord(HabitatDatabase owner, Stream dataStream) : base(owner, dataStream)
        {
            var reader = new BinaryReader(dataStream);

            int vertexCount = reader.ReadInt32();
            for (int i = 0; i < vertexCount; i++)
            {
                byte listClassType = reader.ReadByte(); //0 or 1 (OR 2?)
                int pinId = -1;

                if (listClassType >= 1)
                    reader.ReadInt32(); //? may be present if the value is 0 too. Don't know
                if (listClassType == 2)
                    pinId = reader.ReadInt32(); //pin ID
              
                //add it to the list
                var vector = Helpers.ReadVector3D(reader);
                if (pinId >= 0)
                    PinVertexIndices[pinId] = Vertices.Count;
                Vertices.Add(vector);
            }

            int faceCount = reader.ReadInt32();
            for (int i = 0; i < faceCount; i++)
            {
                byte listClassType = reader.ReadByte(); //0 
                Faces.Add(new Face(owner, dataStream));
            }      
        }
    }
}
