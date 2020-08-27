using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreditBalance : ServerPacket
    {
        public int TransferableTibiaCoins { get; set; }
        public int TotalTibiaCoins { get; set; }
        public int TournamentCoins { get; set; }
        public int Unknown { get; set; }

        public bool UpdateCreditBalance { get; set; }

        public CreditBalance(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreditBalance;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            UpdateCreditBalance = message.ReadBool();
            if (UpdateCreditBalance)
            {
                TotalTibiaCoins = message.ReadInt32();
                TransferableTibiaCoins = message.ReadInt32();
                if (Client.VersionNumber >= 125010109)
                {
                    Unknown = message.ReadInt32();
                }
                if (Client.VersionNumber >= 12158493)
                {
                    TournamentCoins = message.ReadInt32();
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreditBalance);
            message.Write(UpdateCreditBalance);
            if (UpdateCreditBalance)
            {
                message.Write(TotalTibiaCoins);
                message.Write(TransferableTibiaCoins);
                if (Client.VersionNumber >= 125010109)
                {
                    message.Write(Unknown);
                }
                if (Client.VersionNumber >= 12158493)
                {
                    message.Write(TournamentCoins);
                }
            }
        }
    }
}
