using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TransferCurrency : ClientPacket
    {
        public string PlayerName { get; set; }

        public uint Amount { get; set; }

        public TransferCurrency(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TransferCurrency;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerName = message.ReadString();
            Amount = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TransferCurrency);
            message.Write(PlayerName);
            message.Write(Amount);
        }
    }
}
