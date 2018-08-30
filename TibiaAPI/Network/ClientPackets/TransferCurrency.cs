using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TransferCurrency : ClientPacket
    {
        public string PlayerName { get; set; }

        public uint Amount { get; set; }

        public TransferCurrency()
        {
            Type = ClientPacketType.TransferCurrency;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            PlayerName = message.ReadString();
            Amount = message.ReadUInt32();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TransferCurrency);
            message.Write(PlayerName);
            message.Write(Amount);
        }
    }
}
