using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_Server
{
    class ServerSend
    {
        #region Send Packet
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToRoom(int _roomId, Packet _packet)
        {
            if (_roomId == 0)
                return;

            _packet.WriteLength();
            int roomMaxPlayers = Server.rooms[_roomId].maxPlayerCount;
            var roomPlayers = Server.rooms[_roomId].GetRoomPlayers();

            for (int i = 1; i <= roomMaxPlayers; i++)
            {
                if (roomPlayers[i].id == 0)
                    continue;
                Server.clients[roomPlayers[i].id].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToRoom(int _roomId, Packet _packet)
        {
            _packet.WriteLength();
            int roomMaxPlayers = Server.rooms[_roomId].maxPlayerCount;
            var roomPlayers = Server.rooms[_roomId].GetRoomPlayers();

            for (int i = 1; i <= roomMaxPlayers; i++)
            {
                Server.clients[roomPlayers[i].id].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                } 
            }
        }

        private static void SendTCPDataToLobby(Packet _packet)
        {
            _packet.WriteLength();
            for(int i = 1; i <= Server.MaxPlayers; i++)
            {

                if (Server.clients[i].tcp.socket == null)
                    continue;

                if (Server.clients[i].joinedRoomId != 0)
                    continue;

                Server.clients[i].tcp.SendData(_packet);
            }
        }
        #endregion

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        #region Player(Spawn/Movement)
        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void PlayerPosition(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.position);

                SendUDPDataToAll(_packet);
            }
        }

        public static void PlayerRotation(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.rotation);

                SendUDPDataToAll(_player.id, _packet);
            }
        }
        #endregion

        #region Room List Update
        public static void SendRoomList()
        {
            using (Packet _packet = new Packet((int)ServerPackets.roomList))
            {

                _packet.Write(Constants.MAXROOMS);
                int _cnt = 0;
                for (int i = 1; i <= Constants.MAXROOMS; i++)
                    if (Server.rooms[i].ownerClientId != 0)
                        _cnt++;

                _packet.Write(_cnt);

                for (int i = 1; i <= Constants.MAXROOMS; i++)
                {
                    if (Server.rooms[i].ownerClientId == 0)
                        continue;

                    _packet.Write(Server.rooms[i].id);
                    _packet.Write(Server.rooms[i].name);
                    _packet.Write(Server.clients[Server.rooms[i].ownerClientId].userName);
                    _packet.Write(Server.rooms[i].curPlayerCount);
                    _packet.Write(Server.rooms[i].maxPlayerCount);
                }
 
               SendTCPDataToLobby(_packet);
            }
        }
        #endregion

        #region Room Create Status
        public static void SendRoomJoinStatus(int _fromClient, bool isJoin)
        {
            using (Packet _packet = new Packet((int)ServerPackets.roomJoinStatus))
            {

                _packet.Write(isJoin);

                SendTCPData(_fromClient, _packet);
            }
        }
        #endregion

        public static void LobbyChatMessage(int _fromClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.lobbyChatMessage))
            {
                string _str = "<color=#00f500>" + Server.clients[_fromClient].userName + "</color>" + ": " + _msg;
                _packet.Write(_str);

                SendTCPDataToLobby(_packet);
            }
            return;
        }

        public static void LobbyServerMessage(string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.lobbyServerMessage))
            {
                _packet.Write(_msg);

                SendTCPDataToLobby(_packet);
            }
            return;
        }
        public static void RoomChatMessage(int _fromClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.roomChatMessage))
            {
                string _str = "<color=#00f500>" + Server.clients[_fromClient].userName + "</color>" + ": " + _msg;
                _packet.Write(_str);

                SendTCPDataToRoom(Server.clients[_fromClient].joinedRoomId, _packet);
            }
            return;
        }
        #endregion
    }
}
