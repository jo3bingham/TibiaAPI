using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetQuestLine : ClientPacket
    {
        public ushort QuestLineId { get; set; }

        public GetQuestLine()
        {
            PacketType = ClientPacketType.GetQuestLine;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GetQuestLine)
            {
                return false;
            }

            QuestLineId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetQuestLine);
            message.Write(QuestLineId);
        }
    }
}
