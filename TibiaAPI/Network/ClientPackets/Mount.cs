using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Mount : ClientPacket
    {
        public bool EnableMount { get; set; }

        public Mount()
        {
            Type = ClientPacketType.Mount;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            EnableMount = message.ReadBool();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Mount);
            message.Write(EnableMount);
        }
    }
}
