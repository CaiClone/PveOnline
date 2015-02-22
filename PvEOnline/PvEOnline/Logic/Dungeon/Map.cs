using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DataTypes;

namespace PvEOnline.Logic.Dungeon
{
    public class Map
    {
        string name;
        MapData info;
        Texture2D tileset;
        List<Rectangle> tiles;
        byte[,] background;
        byte[,] foreground;
        byte[,] properties;
        public Map(string name)
        {
            this.name = name;
        }
        public void LoadContent(ContentManager cont)
        {
            info = cont.Load<MapData>(@"Dungeons/" + name);
            tileset = cont.Load<Texture2D>(@"Dungeons/" + info.tileset);
            tiles = new List<Rectangle>();
            for (int x = 0; x < (int)info.tilesetSize.X; x++)
                for (int y = 0; y < (int)info.tilesetSize.Y; y++)
                    tiles.Add(new Rectangle(x * CONST.TILESIZEX, y * CONST.TILESIZEY, CONST.TILESIZEX, CONST.TILESIZEY));
            background = SDtoMD(info.background);
            foreground = SDtoMD(info.foreground);
            properties = SDtoMD(info.properties);

        }
        private byte[,] SDtoMD(byte[] sd)
        {
            byte[,] md = new byte[CONST.MAPSIZEX,CONST.MAPSIZEY];
            for (int x = 0; x < CONST.MAPSIZEX; x++)
                for (int y = 0; y < CONST.MAPSIZEY; y++)
                    md[x, y] = sd[x + y];
            return md;
        }
        public void Update(GameTime gameTime)
        {
            //nothing yet
        }

        public void DrawBackground(SpriteBatch sp)
        {
            for (int x = 0; x < CONST.MAPSIZEX; x++)
                for (int y = 0; y < CONST.MAPSIZEY; y++)
                    sp.Draw(tileset, new Vector2(x * CONST.TILESIZEX, y * CONST.TILESIZEY), tiles[background[x, y]], Color.White);
        }
    }
    public enum Tprop
    {
        None,
        Impassable
    }
}
