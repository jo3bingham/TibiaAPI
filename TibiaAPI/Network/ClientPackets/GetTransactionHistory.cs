using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetTransactionHistory : ClientPacket
    {
        public uint CurrentPage { get; set; }

        public byte EntriesPerPage { get; set; }

        public GetTransactionHistory()
        {
            PacketType = ClientPacketType.GetTransactionHistory;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GetTransactionHistory)
            {
                return false;
            }

            CurrentPage = message.ReadUInt32();
            EntriesPerPage = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetTransactionHistory);
            message.Write(CurrentPage);
            message.Write(EntriesPerPage);
        }
    }
}
