using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TTC_Server
{
    class ServerHandle
    {
        //1
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }

            Server.clients[_fromClient].userName = _username;
        }
        //2
        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            bool[] _inputs = new bool[_packet.ReadInt()];
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = _packet.ReadBool();
            }
            Quaternion _rotation = _packet.ReadQuaternion();

            Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
        }
        //3
        public static void RoomCreate(int _fromClient, Packet _packet)
        {

            int _roomId = Util.GetEmptyRoomId();
            bool isJoin = false;

            string _name = _packet.ReadString();
            bool _isPrivate = _packet.ReadBool();
            string _password = _packet.ReadString();

            if(_roomId == 0)
            {
                ServerSend.SendRoomCreateStatus(_fromClient, isJoin);
                return;
            }

            isJoin = Server.rooms[_roomId].JoinPlayer(_fromClient);
            if (isJoin)
            {
                Server.rooms[_roomId].SetRoom(_name, _isPrivate, _password);
            }
            ServerSend.SendRoomCreateStatus(_fromClient, isJoin);
            return;
        }
        //4
        public static void LobbyChatMessage(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();
            if (_msg.Length < 1)
                return;
            ServerSend.LobbyChatMessage(_fromClient, _msg);
        }
        //5
        public static void RoomStartGame(int _fromClient, Packet packet)
        {
            if (Server.clients[_fromClient].joinedRoomId == 0)
            {

                /* Send Error to Client Code */

                return;
            }

            /* Room Start Game Code */

        }

    }

}
