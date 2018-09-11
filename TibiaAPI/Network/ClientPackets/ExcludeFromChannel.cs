using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ExcludeFromChannel : ClientPacket
    {
        public string PlayerName { get; set; }

        public ushort ChannelId { get; set; }

        public ExcludeFromChannel()
        {
            PacketType = ClientPacketType.ExcludeFromChannel;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.ExcludeFromChannel)
            {
                return false;
            }

            PlayerName = message.ReadString();
            ChannelId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ExcludeFromChannel);
            message.Write(PlayerName);
            message.Write(ChannelId);
        }
    }
}
