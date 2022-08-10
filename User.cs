using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15
{
    internal class User
    {
        public string Name;
        public string Password;
        public string Type;
        public string UserId;
        public User(string name_, string password_ ,string type_, string userId_)
        {
            Name = name_;
            Password = password_;
            Type = type_;
            UserId = userId_;
        }
    }
}
