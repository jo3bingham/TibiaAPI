using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketAccept : ClientPacket
    {
        public uint Timestamp { get; set; }

        public ushort Amount { get; set; }
        public ushort Counter { get; set; }

        public MarketAccept(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MarketAccept;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Timestamp = message.ReadUInt32();
            Counter = message.ReadUInt16();
            Amount = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketAccept);
            message.Write(Timestamp);
            message.Write(Counter);
            message.Write(Amount);
        }
    }
}
