using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class JoinChannel : ClientPacket
    {
        public ushort ChannelId { get; set; }

        public JoinChannel()
        {
            Type = ClientPacketType.JoinChannel;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            ChannelId = message.ReadUInt16();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.JoinChannel);
            message.Write(ChannelId);
        }
    }
}
