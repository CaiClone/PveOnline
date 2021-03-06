﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PvEOnline.Net;
using System.Collections;
using PvEOnline.Logic.Dungeons;
using Microsoft.Xna.Framework.Input;
using PvEOnline.Skills;
namespace PvEOnline.Logic.Units
{
    public class UnitManager
    {
        private List<Unit> units;
        private List<Unit> selected;
        
        public Game1 gameRef;
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
            foreach (Unit u in units)
            {
                //26 is the displacement of the sprite
                if(selected.Contains(u))
                    gameRef.spriteBatch.Draw(selUnitTex, new Vector2(u.pos.X - selUnitTex.Width / 2, u.pos.Y - selUnitTex.Height / 2 + CONST.TILESIZEY / 2-26), Color.GreenYellow);
                else
                    gameRef.spriteBatch.Draw(selUnitTex,new Vector2(u.pos.X - selUnitTex.Width / 2, u.pos.Y-selUnitTex.Height/2+CONST.TILESIZEY/2-26), (u.usable) ? Color.Green : Color.DarkSalmon);
                u.Draw(gameTime, gameRef.spriteBatch);
            }
            foreach (Unit u in selected)
            {
                if (u.Target != null)
                {
                    gameRef.spriteBatch.Draw(selUnitTex, new Vector2(u.Target.pos.X - selUnitTex.Width / 2, u.Target.pos.Y - selUnitTex.Height / 2 + CONST.TILESIZEY / 2 - 26), Color.DarkRed);
                }
            }
        }

        public void Select(Vector2 point)
        {
            selected.Clear();
            Unit found = getInSelectRect(point, true);
            if(found!=null)
                selected.Add(found);
        }
        public Unit getInSelectRect(Vector2 point, bool usable)
        {
            bool found = false;
            int i = 0;
            Rectangle rect = new Rectangle((int)point.X - CONST.RECTSELSIZE / 2, (int)point.Y - CONST.RECTSELSIZE / 2, CONST.RECTSELSIZE, CONST.RECTSELSIZE);
            while (i < units.Count() && !found)
            {
                if (units[i].usable==usable)
                    if (rect.Contains((int)units[i].pos.X + CONST.TILESIZEX / 2, (int)units[i].pos.Y + CONST.TILESIZEY / 2))
                    {
                        found = true;
                        return units[i];
                    }
                i++;
            }
            return null;
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
            Unit found = getInSelectRect(dest, false);
            if(found!=null){
                //TODO ONLINE SET TARGET
                foreach (Unit u in selected)
                    u.Target = found;
            }else{
                if(online) net.SendOrder(selected,dest);
                foreach(Unit u in selected)
                    u.ai.orderMove(dest);
            }
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
                getUnit((string)kv.Key).ai.orderMove(intToVector2((int[])kv.Value));
            
        }
        public Vector2 intToVector2(int[] arr)
        {
            return new Vector2(arr[0],arr[1]);
        }

        public bool canUseSkills()
        {
            return selected.Count == 1; //may want to add the ability to use a skill if everyone has it but it dosn't seem terribly useful with only one char from each class
        }

        public int getSelectedSkillNum()
        {
            return selected[0].getSkillNum();
        }

        public void orderSkill(int skillnum)
        {
            selected[0].skillStart(skillnum);
        }
        public List<Skill> getSkills()
        {
            return selected[0].getSkills();
        }

        public List<Unit> getPlayerUnits()
        {
            List<Unit> uL = new List<Unit>();
            foreach (Unit u in units)
                if (u.usable)
                    uL.Add(u);
            return uL;
        }
    }
}
