using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PvEOnline.Logic.Units.Buffs
{
    public abstract class Buff
    {
        protected string name;
        protected uint time;
        protected Unit carrier;
        public  Buff(Unit u){
            carrier=u;
            name = u.name + name;
        }
        public virtual void Apply(Unit u)
        {
            TimerHandler.AddTimer(name, time);
        }
        public virtual void Update()
        {
            if (TimerHandler.CheckTimer(name))
                Remove();
        }
        public virtual void Draw() { }
        public virtual void Remove()
        {
            carrier.Remove(this);
        }
    }
}
