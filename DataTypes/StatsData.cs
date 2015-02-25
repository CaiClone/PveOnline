using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace DataTypes
{
    public class StatsData
    {
        public int maxhp = 1;
        [ContentSerializer(Optional = true)]
        public int maxresource = 1;
        public int atk = 1;
        [ContentSerializer(Optional = true)]
        public int magic = 1;
        public int pDef = 1;
        public int mDef = 1;
        public float critRatio = 0f;
        [ContentSerializer(Optional = true)]
        public float haste = 0f;
        public float atkRange = 0f;
        public float moveSpeed= 0f;
        public String ai = "None"; //<3
        public String sprite = "Error";
        public Race race = Race.Error;
    }
    public enum Race
    {
        Error,
        Human
    }
}
