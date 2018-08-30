using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ThankYou : ClientPacket
    {
        public uint StatementId { get; set; }

        public ThankYou()
        {
            Type = ClientPacketType.ThankYou;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            StatementId = message.ReadUInt32();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ThankYou);
            message.Write(StatementId);
        }
    }
}
