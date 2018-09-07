using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class JoinChannel : ClientPacket
    {
        public ushort ChannelId { get; set; }

        public JoinChannel()
        {
            PacketType = ClientPacketType.JoinChannel;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.JoinChannel)
            {
                return false;
            }

            ChannelId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.JoinChannel);
            message.Write(ChannelId);
        }
    }
}
