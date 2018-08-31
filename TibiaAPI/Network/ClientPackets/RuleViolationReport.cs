using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RuleViolationReport : ClientPacket
    {
        public ReportType ReportType { get; set; }

        public string Comment { get; set; }
        public string PlayerName { get; set; }
        public string Translation { get; set; }

        public uint StatementId { get; set; }

        public byte Reason { get; set; }

        public RuleViolationReport()
        {
            Type = ClientPacketType.RuleViolationReport;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.RuleViolationReport)
            {
                return false;
            }

            ReportType = (ReportType)message.ReadByte();
            Reason = message.ReadByte();
            PlayerName = message.ReadString();
            Comment = message.ReadString();
            if (ReportType == ReportType.Name)
            {
                Translation = message.ReadString();
            }
            else if (ReportType == ReportType.Statement)
            {
                Translation = message.ReadString();
                StatementId = message.ReadUInt32();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RuleViolationReport);
            message.Write((byte)ReportType);
            message.Write(Reason);
            message.Write(PlayerName);
            message.Write(Comment);
            if (ReportType == ReportType.Name)
            {
                message.Write(Translation);
            }
            else if (ReportType == ReportType.Statement)
            {
                message.Write(Translation);
                message.Write(StatementId);
            }
        }
    }
}
