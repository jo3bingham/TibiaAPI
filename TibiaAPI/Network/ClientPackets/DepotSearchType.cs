using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class DepotSearchType : ClientPacket
    {
        public ushort ItemId { get; set; }

        public DepotSearchType(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.DepotSearchType;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ItemId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.DepotSearchType);
            message.Write(ItemId);
        }
    }
}
