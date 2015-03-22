using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PvEOnline
{
    public class CONST
    {
        public static int VRESOLUTIONX = 1920;
        public static int VRESOLUTIONY = 1080;
        public static int TILESIZEX = 64;
        public static int TILESIZEY = 64;
        public static int MAPSIZEX = 30;
        public static int MAPSIZEY = 17;
        public static uint CLICKTIME = 80;
        public static int BASESPEED = 160;

        public static int RECTSELSIZE = 100; //Extra size when selecting by clicking
        public static Color[] COLORS = new Color[] //JIBUUUUUn WOOOOOOOOOOOOOOOOOOOOOO
                                       {   Color.Black,         //Default
                                           Color.Red,           //Phys attack
                                           Color.Purple,        //Magic attack
                                           Color.LightBlue,     //Self buffs
                                           Color.DarkRed,       //Phys Damage Notations
                                           Color.DarkViolet,    //Magic Damage Notations [5]
                                           Color.LimeGreen,     //Heals
                                           Color.Aquamarine,    //AI text
                                           Color.Yellow,        //Time
                                       };

        public static int ICONSIZE = 128;
        public static int PADDING = 20;
        public static int MAXNSKILLS = 9;
    }
}
