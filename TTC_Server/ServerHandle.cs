using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TTC_Server
{
    class ServerHandle
    {
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

        public static void RoomCreate(int _fromClient, Packet _packet)
        {

            int _roomId = Util.GetEmptyRoomId();

            if(_roomId == 0)
            {
                /* Send Failed Msg to Client */
                return;
            }

            Server.rooms[_roomId].JoinPlayer(_fromClient);
            
            /* Send Accept Msg to Client */
            return;
        }
    }
}
