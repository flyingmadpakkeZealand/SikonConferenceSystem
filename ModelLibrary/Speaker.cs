using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Speaker : User
    {
        public string Bio { get; set; }
        public string Occupation { get; set; }

        public Speaker(string name, string phoneNumber, string email, string password, string bio, string occupation) : base(name, phoneNumber, email, password)
        {
            Bio = bio;
            Occupation = occupation;
        }

        public Speaker()
        {
            
        }
    }
}
