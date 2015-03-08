using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using Microsoft.Xna.Framework;

namespace PvEOnline.Net
{
    public interface NetManagerT
    {
        void Update();
        void SendOrder(List<Unit> selected, Vector2 dest);
        void setUnitManager(UnitManager unitManager);
        void SendStart(int seed);
    }
}
