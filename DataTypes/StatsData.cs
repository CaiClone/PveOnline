using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace DataTypes
{
    public class StatsData
    {
        public int maxhp = 0;
        [ContentSerializer(Optional = true)]
        public int maxresource = 0;
        public int atk = 0;
        [ContentSerializer(Optional = true)]
        public int magic = 0;
        public int pDef = 0;
        public int mDef = 0;
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
