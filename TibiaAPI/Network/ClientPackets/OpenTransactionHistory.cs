using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenTransactionHistory : ClientPacket
    {
        public byte EntriesPerPage { get; set; }

        public OpenTransactionHistory()
        {
            Type = ClientPacketType.OpenTransactionHistory;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            EntriesPerPage = message.ReadByte();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenTransactionHistory);
            message.Write(EntriesPerPage);
        }
    }
}
