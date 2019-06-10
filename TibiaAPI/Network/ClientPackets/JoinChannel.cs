using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class JoinChannel : ClientPacket
    {
        public ushort ChannelId { get; set; }

        public JoinChannel(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.JoinChannel;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ChannelId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.JoinChannel);
            message.Write(ChannelId);
        }
    }
}
