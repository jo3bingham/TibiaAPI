using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketCancel : ClientPacket
    {
        public uint Timestamp { get; set; }

        public ushort Counter { get; set; }

        public MarketCancel()
        {
            Type = ClientPacketType.MarketCancel;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            Timestamp = message.ReadUInt32();
            Counter = message.ReadUInt16();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketCancel);
            message.Write(Timestamp);
            message.Write(Counter);
        }
    }
}
