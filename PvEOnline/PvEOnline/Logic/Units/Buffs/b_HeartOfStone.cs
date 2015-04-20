using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PvEOnline.Logic.Units.Buffs
{
    public class b_HeartOfStone:Buff
    {
        public b_HeartOfStone(uint ms)
            : base()
        {
            time = ms;
            name = "Heart Of Stone";
        }
        public override void ApplyEffects()
        {
            carrier.colors.Add(Color.Gray);
            carrier.setFlag(UFlags.Stopped);
            carrier.setFlag(UFlags.Akagi);
        }
        public override void Remove()
        {
            base.Remove();
            carrier.colors.Remove(Color.Gray);
            carrier.removeFlag(UFlags.Stopped);
            carrier.removeFlag(UFlags.Akagi);
        }
    }
}
