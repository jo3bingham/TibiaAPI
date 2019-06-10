using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ItemWasted : ServerPacket
    {
        public ushort ItemId { get; set; }

        public ItemWasted(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ItemWasted;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ItemId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ItemWasted);
            message.Write(ItemId);
        }
    }
}
