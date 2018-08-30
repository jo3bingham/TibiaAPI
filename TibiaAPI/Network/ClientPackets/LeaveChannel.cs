using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LeaveChannel : ClientPacket
    {
        public ushort ChannelId { get; set; }

        public LeaveChannel()
        {
            Type = ClientPacketType.LeaveChannel;
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
            message.Write((byte)ClientPacketType.LeaveChannel);
            message.Write(ChannelId);
        }
    }
}
