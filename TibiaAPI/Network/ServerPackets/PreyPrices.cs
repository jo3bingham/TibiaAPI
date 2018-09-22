using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyPrices : ServerPacket
    {
        public uint ListRerollPrice { get; set; }

        public ushort Unknown { get; set; }

        public PreyPrices()
        {
            PacketType = ServerPacketType.PreyPrices;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PreyPrices)
            {
                return false;
            }

            ListRerollPrice = message.ReadUInt32();
            //---- Example
            // E9 // PreyRerollPrice
            // 32 00 00 00 // List reroll price
            // 01 05 // Unknown
            Unknown = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyPrices);
            message.Write(ListRerollPrice);
            message.Write(Unknown);
        }
    }
}
