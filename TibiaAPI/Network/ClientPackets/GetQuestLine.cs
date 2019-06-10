using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetQuestLine : ClientPacket
    {
        public ushort QuestLineId { get; set; }

        public GetQuestLine(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GetQuestLine;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            QuestLineId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetQuestLine);
            message.Write(QuestLineId);
        }
    }
}
