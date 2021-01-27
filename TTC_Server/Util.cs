using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_Server
{
    class Util
    {
        public static int GetEmptyRoomId()
        {
            int res = 0;

            for(int i = 1; i <= Constants.MAXROOMS; i++)
            {
                if(Server.rooms[i].ownerClientId == 0)
                {
                    res = i;
                    break;
                }
            }

            return res;
        }

        
    }
}
