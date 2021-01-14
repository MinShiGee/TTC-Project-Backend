using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace TTC_Server
{
    class Room
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public int ownerClientId { get; private set; }

        private Dictionary<int, RoomPlayer> roomPlayers = new Dictionary<int, RoomPlayer>();
        public int maxPlayerCount { get; private set; }
        public int curPlayerCount { get; private set; }

        public Room(int _id, int _maxPlayerCount)
        {
            id = _id;
            name = "Empty Room";
            maxPlayerCount = _maxPlayerCount;
            InitRoom();
        }

        public Dictionary<int, RoomPlayer> GetRoomPlayers()
        {
            return roomPlayers;
        }


        public string GetAddress()
        {
            if (ownerClientId == 0)
                return null;

            return ((IPEndPoint)Server.clients[ownerClientId].tcp.socket.Client.LocalEndPoint).Address.ToString();
        }

        public bool JoinPlayer(int _clientId)
        {
            for(int i = 1; i <= maxPlayerCount; i++)
            {
                if (roomPlayers[i].id != 0)
                    continue;

                bool isJoin = roomPlayers[i].JoinRoom(id, _clientId);

                if (!isJoin)
                    return false;

                curPlayerCount++;

                if (ownerClientId == 0)
                    ownerClientId = _clientId;

                return true;
            }
            return false;
        }

        public void LeavePlayer(int _clientId)
        {
            curPlayerCount--;

            for (int i = 1; i <= maxPlayerCount; i++)
            {
                if (_clientId == roomPlayers[i].id)
                {
                    roomPlayers[i].LeaveRoom();
                    break;
                }
            }

            if (ownerClientId == _clientId)
            {
                ownerClientId = 0;
                for(int i = 1; i <= maxPlayerCount; i++)
                    if(roomPlayers[i].id != 0)
                    {
                        ownerClientId = roomPlayers[i].id;
                        break;
                    }
            }

            return;
        }

        private void InitRoom()
        {
            curPlayerCount = 0;
            for(int i = 1; i <= maxPlayerCount; i++)
            {
                roomPlayers.Add(i, new RoomPlayer(0) );
            }
        }
    }

    class RoomPlayer
    {
        public int id { get; private set; }
        bool isReady;

        public RoomPlayer(int _id)
        {
            id = _id;
            isReady = false;
        }

        public bool JoinRoom(int _roomId, int _id)
        {
            id = _id;
            Server.clients[id].JoinRoom(_roomId);
            return true;
        }

        public void LeaveRoom()
        {
            isReady = false;
            id = 0;
        }
    }

}
