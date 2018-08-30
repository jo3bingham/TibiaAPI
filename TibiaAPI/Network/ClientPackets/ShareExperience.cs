using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ShareExperience : ClientPacket
    {
        public bool EnableSharedExperience { get; set; }

        public ShareExperience()
        {
            Type = ClientPacketType.ShareExperience;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            EnableSharedExperience = message.ReadBool();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ShareExperience);
            message.Write(EnableSharedExperience);
        }
    }
}
