using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Speaker : User
    {
        public Speaker(string name, string phoneNumber, string email, string password) : base(name, phoneNumber, email, password)
        {
            
        }

        public Speaker()
        {
            
        }
    }
}
