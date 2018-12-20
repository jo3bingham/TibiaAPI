using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ItemLooted : ServerPacket
    {
        public ObjectInstance Item { get; set; }

        public string ItemDescription { get; set; }

        public ItemLooted(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ItemLooted;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ItemLooted)
            {
                return false;
            }

            Item = message.ReadObjectInstance();
            ItemDescription = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ItemLooted);
            message.Write(Item);
            message.Write(ItemDescription);
        }
    }
}
