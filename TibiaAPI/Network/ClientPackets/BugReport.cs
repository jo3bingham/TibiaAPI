using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BugReport : ClientPacket
    {
        public BugCategory BugCategory { get; set; }

        public Position Position { get; set; }

        public string ReportText { get; set; }
        public string SpeakerName { get; set; }
        public string TypoText { get; set; }

        public BugReport(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.BugReport;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            BugCategory = (BugCategory)message.ReadByte();
            ReportText = message.ReadString();
            if (BugCategory == BugCategory.Map)
            {
                Position = message.ReadPosition();
            }
            else if (BugCategory == BugCategory.Typo)
            {
                SpeakerName = message.ReadString();
                TypoText = message.ReadString();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BugReport);
            message.Write((byte)BugCategory);
            message.Write(ReportText);
            if (BugCategory == BugCategory.Map)
            {
                message.Write(Position);
            }
            else if (BugCategory == BugCategory.Typo)
            {
                message.Write(SpeakerName);
                message.Write(TypoText);
            }
        }
    }
}
