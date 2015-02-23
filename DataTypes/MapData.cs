using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace DataTypes
{
    public class MapData
    {
        public string tileset= "TileSetTest";
        public Vector2 tilesetSize = new Vector2(8,15);
        public byte[] background = new byte[30 * 17];
        [ContentSerializer(Optional = true)]
        public byte[] foreground = new byte[30 * 17];
        public byte[] properties = new byte[30 * 17];
        [ContentSerializer(Optional = true)]
        public List<SpecialEvent> special = new List<SpecialEvent>();

    }
    public struct SpecialEvent
    {
        public Vector2 pos;
        public string special;
    }
}
