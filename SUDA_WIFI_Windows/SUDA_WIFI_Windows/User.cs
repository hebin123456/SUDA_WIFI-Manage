using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUDA_WIFI_Windows
{
    class User
    {
        public string Name { set; get; }
        public string Username { set; get; }
        public string Account { set; get; }

        public User(string Name, string Username, string Account)
        {
            this.Name = Name;
            this.Username = Username;
            this.Account = Account;
        }
    }
}
