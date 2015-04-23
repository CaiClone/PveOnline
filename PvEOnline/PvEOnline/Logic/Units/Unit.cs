using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PvEOnline.AIs;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DataTypes;
using PvEOnline.Logic.Dungeons;
using Microsoft.Xna.Framework.Input;
using PvEOnline.Skills;
using PvEOnline.Logic.Units.Buffs;
using PvEOnline.GUI;
namespace PvEOnline.Logic.Units
{

    public abstract class Unit
    {
        protected StatsData pstats;
        public StatsData bonusStats;
        public Vector2 pos;
        public List<Color> colors =  new List<Color>() {Color.White};
        public string name;
        public AI ai;
        protected string folder;
        protected string filename;
        public bool usable;
        protected int owner;
        protected Texture2D sprite;
        protected State state;
        private int health;
        public int[] unitFlags= new int[(int)UFlags.max];
        public Unit Target { get; set; }
        protected List<Buff> buffs;
        public Color color=Color.White;

        public int hp { get { return health; } }

        public void LoadContent(ContentManager cont)
        {
            buffs = new List<Buff>();
            pstats = cont.Load<StatsData>(@"Units/" + folder+"/"+ filename);
            sprite = cont.Load<Texture2D>(@"Sprites/" + folder + "/" + pstats.sprite);
            bonusStats = new StatsData();
        }
        public void setDest(Vector2 dest)
        {
            ai.setDest(dest);
            state = State.Moving;
        }
        public virtual void loadAi(Dungeon d, UnitManager uM, int seed)
        {
            Type t = Type.GetType("PvEOnline.AIs." + folder + "." + pstats.ai);
            ai = (AI)Activator.CreateInstance(t,this,d,uM,seed);
        }
        public Rectangle getRectangle()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)sprite.Width, (int)sprite.Height);
        }
        public virtual void Update(GameTime gameTime)
        {
            ai.Update();
            for(int i=0;i<buffs.Count;i++)
                buffs[i].Update();
            switch(state)
            {
                case State.Moving:
                    if (!getFlag(UFlags.Stopped))
                    {
                        Vector2 dest = ai.getDest();
                        Vector2 direction = pos - dest;
                        Vector2 oldpos = pos;
                        if (Math.Abs(direction.Length()) > 2f)
                        {
                            direction.Normalize();
                            pos -= (direction * CONST.BASESPEED) * pstats.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else
                            if(!ai.NextDest())
                                state = State.Idle;
                    }
                    break;
            }
        }
        public virtual void Apply(Buff b)
        {
            b.setCarrier(this);
            buffs.Add(b);
            b.Apply();
        }
        public virtual void Remove(Buff b)
        {
            buffs.Remove(b);
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch sp)
        {
            ai.Draw(gameTime, sp);
            sp.Draw(this.sprite, new Vector2(pos.X - sprite.Width / 2, pos.Y - sprite.Height +CONST.TILESIZEY/ 2),Color.Lerp(color, colors.Last(),0.5f));
        }
        public int getSkillNum()
        { 
            return ai.getSkillNum(); 
        }

        public void skillStart(int skillnum)
        {
            ai.skillStart(skillnum);
        }
        public List<Skill> getSkills()
        {
            return ai.getSkills();
        }
        private bool getFlag(UFlags u)
        {
            return unitFlags[(int)u]>0;
        }
        public void setFlag(UFlags u)
        {
            unitFlags[(int)u]++;
        }
        public void removeFlag(UFlags u)
        {
            unitFlags[(int)u]--;
        }

        public void clearRoute()
        {
            state = State.Idle;
        }

        public Unit getTarget()
        {
            return Target;
        }
        private int getPDef()
        {
            return pstats.pDef + bonusStats.pDef;
        }
        private int getMDef()
        {
            return pstats.mDef + bonusStats.mDef;
        }
        private void applyArmor(ref int num, DamageType flags)
        {
            if (flags.HasFlag(DamageType.Physical)){
                int armor = getPDef();
                num -= (armor <= num) ? armor : 0;
            }
            if (flags.HasFlag(DamageType.Magical))
            {
                int armor = getMDef();
                num -= (armor <= num) ? armor : 0;
            }
        }
        public void DealDamage(int num,DamageType flags)
        {
            int real = num;
            applyArmor(ref real,flags);

            //inform AI maybe he wants something with it
            ai.recieveDamage(ref real, flags);

            EffectManager.AdddmgEffectDrawable(real, pos);
            health -= real;
        }
        public int getMaxHP()
        {
            return pstats.maxhp + bonusStats.maxhp;
        }
    }
    public enum State
    {
        Idle,
        Moving
    }
    public enum UFlags
    {
        None,
        Stopped, //can't move
        Akagi,   //can't be damaged
        max,     //Just to calc length
    }
    public enum DamageType
    {
        Physical,
        Magical,
    }
}
