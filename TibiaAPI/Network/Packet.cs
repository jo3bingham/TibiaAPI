using System;

namespace OXGaming.TibiaAPI.Network
{
    public class Packet
    {
        public Client Client { get; set; }

        public bool Forward { get; set; } = true;

        public virtual bool ParseFromNetworkMessage(NetworkMessage message)
        {
            throw new NotImplementedException();
        }

        public virtual void AppendToNetworkMessage(NetworkMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
