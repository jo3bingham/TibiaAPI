using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoNorth : ClientPacket
    {
        public GoNorth()
        {
            PacketType = ClientPacketType.GoNorth;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GoNorth)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoNorth);
        }
    }
}
