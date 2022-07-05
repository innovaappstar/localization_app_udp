using System;
using System.Collections.Generic;
using ServerUDP.domain;
using ServerUDP.dto;
using ServerUDP.interfaces;

namespace ServerUDP.constants
{
    public class Constants
    {
        public static List<CommandDTO> Commands = new List<CommandDTO> {
            new CommandDTO("mobile-gps", new TrackerCommand()),
            new CommandDTO("show_all_clients_command", new ShowAllClientsCommand())
        };

    }
}
