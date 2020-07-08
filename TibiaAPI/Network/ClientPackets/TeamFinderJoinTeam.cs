using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TeamFinderJoinTeam : ClientPacket
    {
        public TeamFinderJoinTeam(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TeamFinderJoinTeam;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.TeamFinderJoinTeam);
        }
    }
}
