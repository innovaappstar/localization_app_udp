using System;
using ServerUDP.interfaces;

namespace ServerUDP.domain
{
    public class TrackerCommand : ICommand
    {
        public TrackerCommand()
        {
        }

        public void invoke()
        {
            throw new NotImplementedException();
        }
    }

    public class ShowAllClientsCommand : ICommand
    {
        public ShowAllClientsCommand()
        {
        }

        public void invoke()
        {
            throw new NotImplementedException();
        }
    }
}
