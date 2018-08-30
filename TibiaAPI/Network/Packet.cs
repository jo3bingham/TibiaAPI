using System;

namespace OXGaming.TibiaAPI.Network
{
    public class Packet
    {
        public bool Forward { get; set; } = true;

        public virtual bool ParseMessage(NetworkMessage message)
        {
            throw new NotImplementedException();
        }

        public virtual void AppendToMessage(NetworkMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
