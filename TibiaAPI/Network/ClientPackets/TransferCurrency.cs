using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TransferCurrency : ClientPacket
    {
        public string PlayerName { get; set; }

        public uint Amount { get; set; }

        public TransferCurrency()
        {
            PacketType = ClientPacketType.TransferCurrency;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.TransferCurrency)
            {
                return false;
            }

            PlayerName = message.ReadString();
            Amount = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TransferCurrency);
            message.Write(PlayerName);
            message.Write(Amount);
        }
    }
}
