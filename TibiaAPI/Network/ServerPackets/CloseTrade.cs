using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseTrade : ServerPacket
    {
        public CloseTrade()
        {
            PacketType = ServerPacketType.CloseTrade;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CloseTrade)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseTrade);
        }
    }
}
