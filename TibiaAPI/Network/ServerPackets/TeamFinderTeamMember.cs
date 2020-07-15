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

            // 01

            // 00 01 00 02 00 00 00
            // 0C 00 <hidden> //player name
            // 52 00 7B 00 02 02 00 01 00 01 00 00 00 02 19 00 4B 00 03


            // 00 03 00 0F 00 00 00
            // 14 00 <hidden> //player name
            // 20 42 6C 69 6E 2C 01 F4 01 02 02 00 01 00 01 00 00 00 02 00 00 00 00 00 0E 00 00 00
            // 0E 00 <hidden> //player name
            // 00 00 00 00 00 FF FF 01 00 01 00 00 00 03 2D 00 00 0D 00 00 00
            // 0D 00 <hidden> //player name
            // CA 00 2C 01 02 03 00 02 00 B0 00 0E 5F 02 08 00 8F 00 00
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.TeamFinderTeamMember);
        }
    }
}
