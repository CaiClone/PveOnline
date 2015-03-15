using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Input;

namespace PvEOnline
{
    [Serializable]
    public class Settings
    {
        public Vector2 resolution { get; set; }
        public bool fullscreen { get; set; }
        public string playerName;
        public Keys[] skillKeys;
        public Settings()
        {
            resolution = new Vector2(1280, 720);
            fullscreen = false;
            playerName = "Faggot"; 
            skillKeys = new Keys[] {Keys.Q, Keys.W,Keys.E,Keys.A,Keys.S,Keys.D,Keys.Z,Keys.X,Keys.C};
        }
        public static Settings loadSettings()
        {
            Settings s;
            try
            {
                FileStream fs = new FileStream("Settings.xml", FileMode.Open, FileAccess.Read);
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                s = (Settings)xs.Deserialize(fs);
                fs.Close();
            }
            catch (System.IO.FileNotFoundException e) //We do not have a settigns file let's create it then
            {
                s = new Settings();
                FileStream fs = new FileStream("Settings.xml", FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                xs.Serialize(fs, s);
                fs.Close();
            }
            return s;
        }
    }
}
