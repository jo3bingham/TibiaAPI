using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseNpcTrade : ServerPacket
    {
        public CloseNpcTrade()
        {
            PacketType = ServerPacketType.CloseNpcTrade;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CloseNpcTrade)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseNpcTrade);
        }
    }
}
