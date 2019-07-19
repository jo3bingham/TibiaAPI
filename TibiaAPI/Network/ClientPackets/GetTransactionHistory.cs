using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetTransactionHistory : ClientPacket
    {
        public uint CurrentPage { get; set; }

        public byte EntriesPerPage { get; set; }

        public GetTransactionHistory(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GetTransactionHistory;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CurrentPage = message.ReadUInt32();
            EntriesPerPage = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetTransactionHistory);
            message.Write(CurrentPage);
            message.Write(EntriesPerPage);
        }
    }
}
