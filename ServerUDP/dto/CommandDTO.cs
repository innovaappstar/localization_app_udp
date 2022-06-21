using System;
using ServerUDP.interfaces;

namespace ServerUDP.dto
{
    public class CommandDTO
    {
        public CommandDTO(string type, ICommand iCommand)
        {
            this.type = type;
            this.iCommand = iCommand;
        }

        public string type { get; set; }
        public ICommand iCommand { get; set; }
    }
}
