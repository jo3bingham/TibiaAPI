using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class InviteToChannel : ClientPacket
    {
        public string PlayerName { get; set; }

        public ushort ChannelId { get; set; }

        public InviteToChannel()
        {
            Type = ClientPacketType.InviteToChannel;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            PlayerName = message.ReadString();
            ChannelId = message.ReadUInt16();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.InviteToChannel);
            message.Write(PlayerName);
            message.Write(ChannelId);
        }
    }
}
