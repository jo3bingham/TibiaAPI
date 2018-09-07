using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network
{
    public class ClientPacket : Packet
    {
        public ClientPacketType PacketType { get; set; }
    }
}
