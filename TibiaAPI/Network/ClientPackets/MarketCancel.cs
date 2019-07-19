using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketCancel : ClientPacket
    {
        public uint Timestamp { get; set; }

        public ushort Counter { get; set; }

        public MarketCancel(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MarketCancel;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Timestamp = message.ReadUInt32();
            Counter = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketCancel);
            message.Write(Timestamp);
            message.Write(Counter);
        }
    }
}
