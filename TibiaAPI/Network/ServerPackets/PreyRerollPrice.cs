using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyRerollPrice : ServerPacket
    {
        public uint ListRerollPrice { get; set; }

        public PreyRerollPrice()
        {
            PacketType = ServerPacketType.PreyRerollPrice;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PreyRerollPrice)
            {
                return false;
            }

            ListRerollPrice = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyRerollPrice);
            message.Write(ListRerollPrice);
        }
    }
}
