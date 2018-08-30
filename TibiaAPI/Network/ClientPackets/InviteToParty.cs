using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class InviteToParty : ClientPacket
    {
        public uint PlayerId { get; set; }

        public InviteToParty()
        {
            Type = ClientPacketType.InviteToParty;
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
            message.Write((byte)ClientPacketType.InviteToParty);
            message.Write(PlayerId);
        }
    }
}
