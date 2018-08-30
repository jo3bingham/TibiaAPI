using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BugReport : ClientPacket
    {
        public BugCategory BugCategory { get; set; }

        //public Position Position { get; set; }

        public string ReportText { get; set; }
        public string SpeakerName { get; set; }
        public string TypoText { get; set; }

        public BugReport()
        {
            Type = ClientPacketType.BugReport;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            BugCategory = (BugCategory)message.ReadByte();
            ReportText = message.ReadString();
            if (BugCategory == BugCategory.Map)
            {
                //Position = message.ReadPosition();
            }
            else if (BugCategory == BugCategory.Typo)
            {
                SpeakerName = message.ReadString();
                TypoText = message.ReadString();
            }
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BugReport);
            message.Write((byte)BugCategory);
            message.Write(ReportText);
            if (BugCategory == BugCategory.Map)
            {
                //message.WritePosition(Position);
            }
            else if (BugCategory == BugCategory.Typo)
            {
                message.Write(SpeakerName);
                message.Write(TypoText);
            }
        }
    }
}
