using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseTest
{
    [Serializable]
    struct Tile
    {

        public byte type;
        public byte textureIndex;

        public Tile(byte _type, byte textIndex)
        {
            type = _type;
            textureIndex = textIndex;
        }

    }
}
