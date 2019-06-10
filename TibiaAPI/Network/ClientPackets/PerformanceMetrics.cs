using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PerformanceMetrics : ClientPacket
    {
        public ushort FpsCounterAverage { get; set; }
        public ushort FpsCounterMaximum { get; set; }
        public ushort FpsCounterMinimum { get; set; }
        public ushort FpsLimit { get; set; }
        public ushort ObjectCounterAverage { get; set; }
        public ushort ObjectCounterMaximum { get; set; }
        public ushort ObjectCounterMinimum { get; set; }

        public PerformanceMetrics(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PerformanceMetrics;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ObjectCounterMinimum = message.ReadUInt16();
            ObjectCounterMaximum = message.ReadUInt16();
            ObjectCounterAverage = message.ReadUInt16();
            FpsCounterMinimum = message.ReadUInt16();
            FpsCounterMaximum = message.ReadUInt16();
            FpsCounterAverage = message.ReadUInt16();
            FpsLimit = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PerformanceMetrics);
            message.Write(ObjectCounterMinimum);
            message.Write(ObjectCounterMaximum);
            message.Write(ObjectCounterAverage);
            message.Write(FpsCounterMinimum);
            message.Write(FpsCounterMaximum);
            message.Write(FpsCounterAverage);
            message.Write(FpsLimit);
        }
    }
}
