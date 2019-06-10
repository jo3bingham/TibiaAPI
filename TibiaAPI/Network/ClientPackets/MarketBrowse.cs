using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketBrowse : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public MarketBrowse(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MarketBrowse;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ObjectId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketBrowse);
            message.Write(ObjectId);
        }
    }
}
