using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TeamFinderTeamMember : ServerPacket
    {
        public TeamFinderTeamMember(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TeamFinderTeamMember;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.TeamFinderTeamMember);
        }
    }
}
