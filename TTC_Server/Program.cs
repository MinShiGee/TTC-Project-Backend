using System;
using System.Threading;

namespace TTC_Server
{
    class Program
    {
        private static bool isRunning = false;

        static void Main(string[] args)
        {
            Console.Title = Constants.SERVERNAME;
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Thread lobbyThread = new Thread(new ThreadStart(LobbyThread));
            lobbyThread.Start();

            Server.Start(Constants.MAXSERVERPLAYER, Constants.MAXROOMS, Constants.PORT);

            string _input;
            do
            {
                _input = Console.ReadLine();
                string[] _str = _input.Split(' ');

                switch (_str[0])
                {
                    case "say":
                        string _msg = String.Empty;
                        for (int i = 1; i < _str.Length; i++)
                            _msg += _str[i] + " ";
                        
                        ServerSend.LobbyServerMessage(_msg);
                        break;

                    default:
                        break;
                }

            } while (true);

        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }

        private static void LobbyThread()
        {
            Console.WriteLine($"Lobby thread started. Running at {Constants.SEND_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.RoomListUpdate();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.SEND_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }

    }
}
