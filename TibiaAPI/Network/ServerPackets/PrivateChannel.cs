using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PrivateChannel : ServerPacket
    {
        public string ChannelName { get; set; }

        public PrivateChannel(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PrivateChannel;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ChannelName = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PrivateChannel);
            message.Write(ChannelName);
        }
    }
}
