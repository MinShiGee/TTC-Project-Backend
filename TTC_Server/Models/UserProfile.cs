using System;
using System.Collections.Generic;
using System.Text;

namespace TTC_Server.Models
{
    class UserProfile
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public long Money { get; set; }

        public int PlayCount { get; set; }

        public int WinCount { get; set; }

        public DateTime CreateTime { get; set; }

        public UserProfile(string _name)
        {
            Id = "";
            Name = _name;
            Money = 0;
            PlayCount = 0;
            WinCount = 0;
            CreateTime = DateTime.Now;
        }
    }
}
