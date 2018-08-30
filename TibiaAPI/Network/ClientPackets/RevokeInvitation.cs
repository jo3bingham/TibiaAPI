using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RevokeInvitation : ClientPacket
    {
        public uint PlayerId { get; set; }

        public RevokeInvitation()
        {
            Type = ClientPacketType.RevokeInvitation;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RevokeInvitation);
            message.Write(PlayerId);
        }
    }
}
