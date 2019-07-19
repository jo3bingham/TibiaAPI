using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LeaveChannel : ClientPacket
    {
        public ushort ChannelId { get; set; }

        public LeaveChannel(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.LeaveChannel;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ChannelId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LeaveChannel);
            message.Write(ChannelId);
        }
    }
}
