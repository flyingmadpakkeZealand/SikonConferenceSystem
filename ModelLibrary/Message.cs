using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Message
    {
        public int Id { get; set; }
        public string textMessage { get; set; }

        public Message(int id, string message)
        {
            textMessage = message;
            Id = id;
        }

        public Message()
        {
            
        }
    }

}
