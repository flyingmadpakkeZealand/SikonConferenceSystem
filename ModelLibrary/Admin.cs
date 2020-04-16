using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Admin : User
    {
        public int AccessLevel { get; set; }

        public Admin(string name, string phoneNumber, string email, string password, int accessLevel) : base(name, phoneNumber, email, password)
        {
            AccessLevel = accessLevel;
        }

        public Admin()
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + $"\nAccessLevel: {AccessLevel}";
        }
    }
}
