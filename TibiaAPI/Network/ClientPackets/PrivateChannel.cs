using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PrivateChannel : ClientPacket
    {
        public string PlayerName { get; set; }

        public PrivateChannel()
        {
            Type = ClientPacketType.PrivateChannel;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            PlayerName = message.ReadString();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PrivateChannel);
            message.Write(PlayerName);
        }
    }
}
