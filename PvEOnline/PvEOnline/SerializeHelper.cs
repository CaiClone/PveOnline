using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTypes;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace PvEOnline
{
    public class SerializeHelper
    {
        public static void SerializeExampleStats()
        {
            StatsData d = new StatsData();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("stats.xml", settings))
            {
                IntermediateSerializer.Serialize(writer, d, null);
            }
        }
        public static void SerializeExampleMapInfo()
        {
            MapData d = new MapData();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("MapInfo.xml", settings))
            {
                IntermediateSerializer.Serialize(writer, d, null);
            }
        }
    }
}
