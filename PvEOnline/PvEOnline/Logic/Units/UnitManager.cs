using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PvEOnline.Net;
using System.Collections;
using PvEOnline.Logic.Dungeons;

namespace PvEOnline.Logic.Units
{
    public class UnitManager
    {
        private List<Unit> units;
        private List<Unit> selected;
        
        private Game1 gameRef;
        private Texture2D selUnitTex;
        private NetManagerT net;
        private Dungeon dung;
        private Random rnd;
        private Boolean online;
        public UnitManager(Game1 game, NetManagerT netManager,Dungeon dungeon, int seed)
        {
            rnd = new Random(seed);
            units = new List<Unit>();
            selected = new List<Unit>();
            gameRef = game;
            net = netManager;
            online = (net != null);
            dung = dungeon;
            if(online) netManager.setUnitManager(this);
            selUnitTex = game.Content.Load<Texture2D>(@"GUI/SelUnit");
        }
        public void Add(Unit u)
        {
            u.LoadContent(gameRef.Content);
            u.loadAi(dung, this, rnd.Next());
            units.Add(u);
        }
        public void Update(GameTime gameTime)
        {
            foreach (Unit u in units)
                u.Update(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            foreach (Unit u in selected)
                drawSelection(u);
            foreach (Unit u in units)
                u.Draw(gameTime, gameRef.spriteBatch);
        }
        private void drawSelection(Unit u)
        {
            gameRef.spriteBatch.Draw(selUnitTex, u.pos, Color.Lime);
        }
        public void Select(Vector2 point)
        {
            selected.Clear();
            bool found=false;
            int i = 0;
            Rectangle rect = new Rectangle((int)point.X - CONST.RECTSELSIZE / 2, (int)point.Y - CONST.RECTSELSIZE / 2, CONST.RECTSELSIZE, CONST.RECTSELSIZE);
            while (i<units.Count() && !found)
            {
                if(units[i].usable)
                    if (rect.Contains((int)units[i].pos.X + CONST.TILESIZEX / 2, (int)units[i].pos.Y + CONST.TILESIZEY / 2))
                    {
                        found = true;
                        selected.Add(units[i]);
                    }
                i++;
            }
        }

        public void SelectRect(Rectangle rect)
        {
            selected.Clear();
            foreach (Unit u in units)
                if (u.usable)
                    if (rect.Contains((int)u.pos.X + CONST.TILESIZEX / 2, (int)u.pos.Y + CONST.TILESIZEY / 2))
                        selected.Add(u);
        }
        public void OrderMove(Vector2 dest)
        {
            if(online) net.SendOrder(selected,dest);
            foreach(Unit u in selected)
                u.setDest(dest);
        }

        public Unit getUnit(string name)
        {
            foreach (Unit u in units)
                if (u.name == name)
                    return u;
            return null;
        }
        public void NetOrderMove(Hashtable p)
        {
            foreach (DictionaryEntry kv in p)
                getUnit((string)kv.Key).setDest(intToVector2((int[])kv.Value));
            
        }
        public Vector2 intToVector2(int[] arr)
        {
            return new Vector2(arr[0],arr[1]);
        }
    }
}
