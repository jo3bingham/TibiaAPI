using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network
{
    public class ServerPacket : Packet
    {
        public ServerPacketType PacketType { get; set; }
    }
}
