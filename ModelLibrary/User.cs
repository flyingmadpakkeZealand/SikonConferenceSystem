using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string name, string phoneNumber, string email, string password)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
        }

        public User()
        {
            
        }

        public override string ToString()
        {
            return $"Name: {Name}\nPhoneNumber: {PhoneNumber}\nID: {Id}";
        }
    }
}
