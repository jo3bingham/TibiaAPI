using System;

namespace OXGaming.TibiaAPI.Network
{
    public class Packet
    {
        public Client Client { get; set; }

        public bool Forward { get; set; } = true;

        public virtual void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public virtual void AppendToNetworkMessage(NetworkMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
