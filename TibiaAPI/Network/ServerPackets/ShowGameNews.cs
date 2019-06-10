using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ShowGameNews : ServerPacket
    {
        public uint CategoryId { get;set; }

        public byte PageNumber { get; set; }

        public ShowGameNews(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ShowGameNews;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CategoryId = message.ReadUInt32();
            PageNumber = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ShowGameNews);
            message.Write(CategoryId);
            message.Write(PageNumber);
        }
    }
}
