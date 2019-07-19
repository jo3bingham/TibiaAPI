using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ChannelEvent : ServerPacket
    {
        public string PlayerName { get; set; }

        public ushort ChannelId { get; set; }

        public byte EventType { get; set; }

        public ChannelEvent(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ChannelEvent;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ChannelId = message.ReadUInt16();
            PlayerName = message.ReadString();
            EventType = message.ReadByte();
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
