using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseChannel : ServerPacket
    {
        public ushort ChannelId { get; set; }

        public CloseChannel()
        {
            PacketType = ServerPacketType.CloseChannel;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
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
