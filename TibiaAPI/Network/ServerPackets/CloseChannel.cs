using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseChannel : ServerPacket
    {
        public ushort ChannelId { get; set; }

        public CloseChannel(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CloseChannel;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ChannelId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseChannel);
            message.Write(ChannelId);
        }
    }
}
