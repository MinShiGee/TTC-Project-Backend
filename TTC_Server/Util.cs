using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_Server
{
    class Util
    {
        public static Packet ChangeRoomPlayersToPacket()
        {
            Packet _packet = new Packet((int)ServerPackets.roomList);

            for(int i = 1; i <= Constants.MAXROOMS; i++)
            {
             
            }

            return _packet;
        }
    }
}
