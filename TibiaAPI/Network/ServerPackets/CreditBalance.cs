using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreditBalance : ServerPacket
    {
        public uint Unknown { get; set; }

        public int ConfirmedCreditBalance { get; set; }
        public int CurrentCreditBalance { get; set; }

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
                CurrentCreditBalance = message.ReadInt32();
                ConfirmedCreditBalance = message.ReadInt32();
                if (Client.VersionNumber >= 12158493)
                {
                    Unknown = message.ReadUInt32();
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreditBalance);
            message.Write(UpdateCreditBalance);
            if (UpdateCreditBalance)
            {
                message.Write(CurrentCreditBalance);
                message.Write(ConfirmedCreditBalance);
                if (Client.VersionNumber >= 12158493)
                {
                    message.Write(Unknown);
                }
            }
        }
    }
}
