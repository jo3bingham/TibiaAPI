using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenTransactionHistory : ClientPacket
    {
        public byte EntriesPerPage { get; set; }

        public OpenTransactionHistory(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenTransactionHistory;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            EntriesPerPage = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenTransactionHistory);
            message.Write(EntriesPerPage);
        }
    }
}
