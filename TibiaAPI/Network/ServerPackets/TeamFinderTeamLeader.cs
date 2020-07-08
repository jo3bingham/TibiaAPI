using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TeamFinderTeamLeader : ServerPacket
    {
        public TeamFinderTeamLeader(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TeamFinderTeamLeader;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.TeamFinderTeamLeader);
        }
    }
}
