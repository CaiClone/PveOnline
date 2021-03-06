﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PvEOnline.Logic.Units;
using PvEOnline.Logic.Units.Classes;
using PvEOnline.Net;
using Microsoft.Xna.Framework.Input;
using PvEOnline.GUI;
using PvEOnline.Skills;

namespace PvEOnline.Logic.Dungeons
{

    public class Dungeon
    {
        private UnitManager uManager;
        private string name;
        public Map map;
        private Game1 gameRef;

        private Vector2 selS;
        private Texture2D selTex;
        public SpriteFont spf;
        private NetManagerT net;
        private GameUI gui;
        private bool falseClick=false;

        public Dungeon(String name,Game1 game,NetManagerT netManager,int seed)
        {
            this.name=name;
            gameRef = game;
            net = netManager;

            uManager = new UnitManager(gameRef,netManager,this,seed);
            uManager.Add(new PClass("Guardian", "TankHealDps"));
            uManager.Add(new PClass("Guardian", "BestTank",100,100));
            uManager.Add(new Boss("Elemental","Elemental",500,500));
            selTex = gameRef.Content.Load<Texture2D>(@"GUI/Selection");
            spf = gameRef.Content.Load<SpriteFont>(@"Font/MenuFont");
            gui = new GameUI();
            gui.Load(gameRef.Content, gameRef.settings.skillKeys);
            EffectManager.Damagespf = gameRef.Content.Load<SpriteFont>(@"Font/MenuFont");
        }
        public void newMap(string name)
        {
            map = new Map(name);
            map.LoadContent(gameRef.Content);
        }
        public void Update(GameTime gameTime)
        {
            HandleControls(gameTime);
            uManager.Update(gameTime);
            map.Update(gameTime);
            EffectManager.Update(gameTime);
        }
        public void Draw(GameTime gameTime,SpriteBatch sp)
        {
            map.DrawBackground(sp);
            uManager.Draw(gameTime);
            if (InputHandler.LeftMouseDown() && TimerHandler.CheckTimer("ClickSelectBox",false))
                sp.Draw(selTex, calcRectangle(selS, InputHandler.MousePosition()), Color.White);
            if (uManager.canUseSkills()){
                List<Skill> s = uManager.getSkills();
                gui.drawSkills(s,sp);
                gui.hoverSkills(s, InputHandler.MousePosition(), sp);
            }
            EffectManager.Draw(gameTime, sp);
        }
        private void HandleControls(GameTime gameTime)
        {

            if (InputHandler.LeftMouseReleased())
            {
                if (falseClick)
                    falseClick = false;
                else
                {
                    if (TimerHandler.CheckTimer("ClickSelectBox"))
                        uManager.SelectRect(calcRectangle(selS, InputHandler.MousePosition()));
                    else
                    {
                        TimerHandler.RemoveTimer("ClickSelectBox");
                        uManager.Select(InputHandler.MousePosition());
                    }
                }
            }
            else if (InputHandler.LeftMousePressed())
            {
                Vector2 mousepos = InputHandler.MousePosition();
                if(uManager.canUseSkills()){
                    int usedSkill = gui.clickedSkill(mousepos,uManager.getSelectedSkillNum()); //TODO fix overhead right here
                    if (usedSkill > -1)
                    {
                        uManager.orderSkill(usedSkill);
                        falseClick = true;
                    }
                }
                if(!falseClick)
                {
                    selS = mousepos;
                    TimerHandler.AddTimer("ClickSelectBox", CONST.CLICKTIME);
                }
            }
            else if (InputHandler.RightMousePressed())
                uManager.OrderMove(InputHandler.MousePosition());
            if (uManager.canUseSkills())
            {
                int nskills = uManager.getSelectedSkillNum();
                for (int i = 0; i < nskills; i++)
                    if (InputHandler.KeyPressed(gameRef.settings.skillKeys[nskills-i-1]))
                        uManager.orderSkill(i);
            }

        }
        private Rectangle calcRectangle(Vector2 start, Vector2 end)
        {
            return new Rectangle((int)Math.Min(start.X, end.X), (int)Math.Min(start.Y, end.Y), (int)Math.Abs(end.X - start.X), (int)Math.Abs(end.Y - start.Y));
        }
    }
}
