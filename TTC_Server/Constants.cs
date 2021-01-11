using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_Server
{
    class Constants
    {
        public const string SERVERNAME = "TTC-GameServer";

        public const int MAXSERVERPLAYER = 1500;
        public const int MAXROOMS = 500;
        public const int MAXROOMPLAYER = 6;

        public const int TICKS_PER_SEC = 30;
        public const float MS_PER_TICK = 1000f / TICKS_PER_SEC;
    }
}
