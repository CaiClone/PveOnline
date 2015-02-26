using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon.LoadBalancing;

namespace PvEOnline
{
    public class NetManager
    {
        private LoadBalancingClient client;
        public void Start()
        {
            client = new LoadBalancingClient();
            client.AppId = PRIVATECONST.APPID;
            if (!client.Connect())
                Console.WriteLine("Can't Connect to EU server");

            /*if (!client.OpJoinLobby(null))
                Console.WriteLine("Can't Connect to Lobby");*/
        }
        public void newRoom(string boss,string name, byte maxPlayers)
        {
            RoomOptions ro = new RoomOptions();
            ro.MaxPlayers = maxPlayers;
            client.OpCreateRoom((boss +"-"+name), ro, null);

        }
        public Dictionary<string, RoomInfo> getRoomList()
        {
            return client.RoomInfoList;
        }
        public void Update()
        {
            client.Service();
        }
        public void Quit()
        {
            client.Disconnect();
        }
    }
}
