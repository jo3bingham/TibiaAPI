using System;

namespace OXGaming.TibiaAPI.Network
{
    public class Packet
    {
        public virtual Packet FromNetworkMessage(NetworkMessage message)
        {
            throw new NotImplementedException();
        }

        public virtual NetworkMessage ToNetworkMessage()
        {
            throw new NotImplementedException();
        }
    }
}
