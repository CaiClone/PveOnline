using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PvEOnline.Logic.Units.Buffs
{
    public abstract class Buff
    {
        public string name;
        protected uint time;
        protected Unit carrier;
        public void setCarrier(Unit u){
            carrier=u;
            name = u.name + "b_"+name;
        }
        public void Apply()
        {
            if (!TimerHandler.hasTimer(name))
                ApplyEffects();
            TimerHandler.AddTimer(name, time);
        }
        public void ApplyCD() //can be called to set cd without applying other effects in sons
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
        public abstract void ApplyEffects();
    }
}
