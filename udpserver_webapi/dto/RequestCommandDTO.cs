using System;
using ServerUDP.interfaces;

namespace ServerUDP.dto
{
    public class RequestCommandDTO
    {
        public RequestCommandDTO(string type, string content)
        {
            this.type = type;
            this.content = content;
        }

        public string type { get; set; }
        public string content { get; set; }


    }
}
