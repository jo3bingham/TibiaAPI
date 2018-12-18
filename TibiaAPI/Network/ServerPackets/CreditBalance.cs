using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreditBalance : ServerPacket
    {
        public int ConfirmedCreditBalance { get; set; }
        public int CurrentCreditBalance { get; set; }

        public bool UpdateCreditBalance { get; set; }

        public CreditBalance(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreditBalance;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreditBalance)
            {
                return false;
            }

            UpdateCreditBalance = message.ReadBool();
            if (UpdateCreditBalance)
            {
                CurrentCreditBalance = message.ReadInt32();
                ConfirmedCreditBalance = message.ReadInt32();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreditBalance);
            message.Write(UpdateCreditBalance);
            if (UpdateCreditBalance)
            {
                message.Write(CurrentCreditBalance);
                message.Write(ConfirmedCreditBalance);
            }
        }
    }
}
