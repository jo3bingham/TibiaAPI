using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LookTrade : ClientPacket
    {
        public byte Index { get; set; }
        public byte Side { get; set; }

        public LookTrade(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.LookTrade;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Side = message.ReadByte();
            Index = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LookTrade);
            message.Write(Side);
            message.Write(Index);
        }
    }
}
