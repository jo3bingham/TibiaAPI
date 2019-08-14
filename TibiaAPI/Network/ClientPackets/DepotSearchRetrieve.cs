using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class DepotSearchRetrieve : ClientPacket
    {
        public ushort ItemId { get; set; }
        public byte Amount { get; set; }

        public DepotSearchRetrieve(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.DepotSearchRetrieve;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ItemId = message.ReadUInt16();
            Amount = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.DepotSearchRetrieve);
            message.Write(ItemId);
            message.Write(Amount);
        }
    }
}
