using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketAccept : ClientPacket
    {
        public uint Timestamp { get; set; }

        public ushort Amount { get; set; }
        public ushort Counter { get; set; }

        public MarketAccept()
        {
            PacketType = ClientPacketType.MarketAccept;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.MarketAccept)
            {
                return false;
            }

            Timestamp = message.ReadUInt32();
            Counter = message.ReadUInt16();
            Amount = message.ReadUInt16();
            return true;
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
