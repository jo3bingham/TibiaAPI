using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ShowGameNews : ServerPacket
    {
        public uint CategoryId { get;set; }

        public byte PageNumber { get; set; }

        public ShowGameNews()
        {
            PacketType = ServerPacketType.ShowGameNews;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ShowGameNews)
            {
                return false;
            }

            CategoryId = message.ReadUInt32();
            PageNumber = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ShowGameNews);
            message.Write(CategoryId);
            message.Write(PageNumber);
        }
    }
}
