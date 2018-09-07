using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LeaveChannel : ClientPacket
    {
        public ushort ChannelId { get; set; }

        public LeaveChannel()
        {
            PacketType = ClientPacketType.LeaveChannel;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.LeaveChannel)
            {
                return false;
            }

            ChannelId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LeaveChannel);
            message.Write(ChannelId);
        }
    }
}
