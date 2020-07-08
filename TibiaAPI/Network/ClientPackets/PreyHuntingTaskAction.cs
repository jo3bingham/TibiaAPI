using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PreyHuntingTaskAction : ClientPacket
    {
        public PreyHuntingTaskAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PreyHuntingTaskAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.PreyHuntingTaskAction);
        }
    }
}
