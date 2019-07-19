using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetQuestLog : ClientPacket
    {
        public GetQuestLog(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GetQuestLog;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetQuestLog);
        }
    }
}
