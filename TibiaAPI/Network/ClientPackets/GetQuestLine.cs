using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetQuestLine : ClientPacket
    {
        public ushort QuestLineId { get; set; }

        public GetQuestLine()
        {
            Type = ClientPacketType.GetQuestLine;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            QuestLineId = message.ReadUInt16();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetQuestLine);
            message.Write(QuestLineId);
        }
    }
}
