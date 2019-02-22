using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TransactionDetails : ServerPacket
    {
        public string Character { get; set; }
        public string Description { get; set; }

        public uint Id { get; set; }
        public uint PiecePrice { get; set; }
        public uint Timestamp { get; set; }

        public int PurchasedTibiaCoins { get; set; }
        public int SpentGold { get; set; }

        public byte Unknown { get; set; }

        public TransactionDetails(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TransactionDetails;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.TransactionDetails)
            {
                return false;
            }

            Id = message.ReadUInt32();
            Unknown = message.ReadByte();
            Timestamp = message.ReadUInt32();
            Description = message.ReadString();
            Character = message.ReadString();
            PurchasedTibiaCoins = message.ReadInt32();
            PiecePrice = message.ReadUInt32();
            SpentGold = message.ReadInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TransactionDetails);
            message.Write(Id);
            message.Write(Unknown);
            message.Write(Timestamp);
            message.Write(Description);
            message.Write(Character);
            message.Write(PurchasedTibiaCoins);
            message.Write(PiecePrice);
            message.Write(SpentGold);
        }
    }
}
