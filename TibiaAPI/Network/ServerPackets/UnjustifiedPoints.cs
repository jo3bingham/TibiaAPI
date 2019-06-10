using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class UnjustifiedPoints : ServerPacket
    {
        public byte KillsRemainingDay { get; set; }
        public byte KillsRemainingMonth { get; set; }
        public byte KillsRemainingWeek { get; set; }
        public byte ProgressDay { get; set; }
        public byte ProgressMonth { get; set; }
        public byte ProgressWeek { get; set; }
        public byte SkullDuration { get; set; }

        public UnjustifiedPoints(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.UnjustifiedPoints;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ProgressDay = message.ReadByte();
            KillsRemainingDay = message.ReadByte();
            ProgressWeek = message.ReadByte();
            KillsRemainingWeek = message.ReadByte();
            ProgressMonth = message.ReadByte();
            KillsRemainingMonth = message.ReadByte();
            SkullDuration = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.UnjustifiedPoints);
            message.Write(ProgressDay);
            message.Write(KillsRemainingDay);
            message.Write(ProgressWeek);
            message.Write(KillsRemainingWeek);
            message.Write(ProgressMonth);
            message.Write(KillsRemainingMonth);
            message.Write(SkullDuration);
        }
    }
}
