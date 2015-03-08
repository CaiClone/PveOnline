using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon.LoadBalancing;
using PvEOnline.Logic.Units;
using Microsoft.Xna.Framework;
using System.Collections;
using ExitGames.Client.Photon;
using PvEOnline.Screens;

namespace PvEOnline.Net
{
    public class NetManager : LoadBalancingClient, NetManagerT
    {
        UnitManager um;
        Game1 gameRef;
        public void Start(Game1 game)
        {
            AppId = PRIVATECONST.APPID;
            if (!Connect())
                Console.WriteLine("Can't Connect to EU server");
            gameRef = game;
        }
        public void newRoom(string boss,string name, byte maxPlayers)
        {
            RoomOptions ro = new RoomOptions();
            ro.MaxPlayers = maxPlayers;
            OpCreateRoom((boss +"-"+name), ro, null);

        }
        public Dictionary<string, RoomInfo> getRoomList()
        {
            return RoomInfoList;
        }
        public void Update()
        {
            Service();
        }
        public void setUnitManager(UnitManager unitManager)
        {
            um = unitManager;
        }
        public void SendOrder(List<Unit> selected, Vector2 dest)
        {
            Hashtable data = new Hashtable();
            foreach (Unit u in selected)
                data.Add(u.name, Vector2toInt(dest));
            OpRaiseEvent((byte)Event.OMove, data, true, null);
        }
        public void SendStart(int seed)
        {
            Hashtable data = new Hashtable();
            data.Add(seed, seed);
            OpRaiseEvent((byte)Event.Start, data, true, null);
        }
        public override void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case (byte)Event.OMove:
                    um.NetOrderMove((Hashtable)photonEvent[(byte)ParameterCode.CustomEventContent]);
                    break;
                case (byte)Event.Start:
                    gameRef.screenManager.PushScreen(new GameScreen(gameRef, this, 1000000));
                    break;
            }
            base.OnEvent(photonEvent);
        }
        public int[] Vector2toInt(Vector2 v)
        {
            return new int[] {(int)v.X,(int)v.Y};
        }
    }
    enum Event 
    {
        OMove = 1,
        Start = 2
    }
}
