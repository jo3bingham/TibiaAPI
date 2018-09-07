using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ChannelEvent : ServerPacket
    {
        public string PlayerName { get; set; }

        public ushort ChannelId { get; set; }

        public byte EventType { get; set; }

        public ChannelEvent()
        {
            PacketType = ServerPacketType.ChannelEvent;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ChannelEvent)
            {
                return false;
            }

            ChannelId = message.ReadUInt16();
            PlayerName = message.ReadString();
            EventType = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ChannelEvent);
            message.Write(ChannelId);
            message.Write(PlayerName);
            message.Write(EventType);
        }
    }
}
