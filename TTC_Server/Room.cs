using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_Server
{
    class Room
    {
        public int id { get; private set; }
        public int ownerClientId { get; private set; }

        private Dictionary<int, RoomPlayer> roomPlayers = new Dictionary<int, RoomPlayer>();
        public int maxPlayerCount { get; private set; }
        public int curPlayerCount { get; private set; }

        public Room(int _id, int _maxPlayerCount)
        {
            id = _id;
            maxPlayerCount = _maxPlayerCount;
            InitRoom();
        }

        public Dictionary<int, RoomPlayer> GetRoomPlayers()
        {
            return roomPlayers;
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

                return true;
            }
            return false;
        }

        public void LeavePlayer(int _clientId)
        {
            roomPlayers[_clientId].LeaveRoom();
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
            Server.clients[id].LeaveRoom();
            isReady = false;
            id = 0;
        }
    }

}
