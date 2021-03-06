﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PvEOnline.GUI
{
    public static class EffectManager
    {
        private static List<Drawable>effects = new List<Drawable>();
        public static SpriteFont Damagespf;
        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < effects.Count; i++)
                effects[i].update(gameTime);
        }
        public static void Draw(GameTime gameTime,SpriteBatch sp)
        {
            for (int i = 0; i < effects.Count;i++)
                    effects[i].draw(gameTime, sp);
        }
        public static void Remove(Drawable draw)
        {
            effects.Remove(draw);
        }
        public static void AdddmgEffectDrawable(int num,Vector2 pos)
        {
            effects.Add(new dmgEffectDrawable(num, pos));
        }
    }
    public abstract class Drawable
    {
        public abstract void update(GameTime gameTime);
        public abstract void draw(GameTime gameTime, SpriteBatch sp);
    }
    class dmgEffectDrawable : Drawable
    {
        Color col;
        float size;
        Vector2 pos;
        string txt;
        float alpha;
        public dmgEffectDrawable(int num, Vector2 pos)
        {
            col = (num>=0)? Color.Red:Color.Green;
            size = MathHelper.Clamp(num/1000,1,5);
            txt = num.ToString();
            this.pos = pos;
            alpha = 0;
        }
        public override void update(GameTime gameTime)
        {
            pos += new Vector2(0, (float)gameTime.ElapsedGameTime.TotalSeconds * -100);
            alpha += (float)gameTime.ElapsedGameTime.TotalSeconds *0.6f;
            if (alpha==1)
                EffectManager.Remove(this);
        }
        public override void draw(GameTime gameTime, SpriteBatch sp)
        {
            sp.DrawString(EffectManager.Damagespf, txt, pos, Color.Lerp(col,Color.Transparent,alpha));
        }
    }
}
