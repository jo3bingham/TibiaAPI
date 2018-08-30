using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class JoinParty : ClientPacket
    {
        public uint PlayerId { get; set; }

        public JoinParty()
        {
            Type = ClientPacketType.JoinParty;
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
            message.Write((byte)ClientPacketType.JoinParty);
            message.Write(PlayerId);
        }
    }
}
