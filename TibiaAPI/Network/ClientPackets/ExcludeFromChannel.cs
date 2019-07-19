using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ExcludeFromChannel : ClientPacket
    {
        public string PlayerName { get; set; }

        public ushort ChannelId { get; set; }

        public ExcludeFromChannel(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.ExcludeFromChannel;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerName = message.ReadString();
            ChannelId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ExcludeFromChannel);
            message.Write(PlayerName);
            message.Write(ChannelId);
        }
    }
}
