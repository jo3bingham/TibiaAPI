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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CloseChannel)
            {
                return false;
            }

            ChannelId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseChannel);
            message.Write(ChannelId);
        }
    }
}
