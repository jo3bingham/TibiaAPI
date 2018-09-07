using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ItemLooted : ServerPacket
    {
        public string ItemDescription { get; set; }

        public ItemLooted()
        {
            PacketType = ServerPacketType.ItemLooted;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ItemLooted)
            {
                return false;
            }

            // TODO
            //message.ReadObjectInstance();
            ItemDescription = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ItemLooted);
            // TODO
        }
    }
}
