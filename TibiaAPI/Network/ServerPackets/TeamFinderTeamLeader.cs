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

            // 01

            // 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00

            // 00
            // 52 00
            // 7B 00
            // 02 //vocations
            // 02 00
            // 01 00
            // 01 00 00 00
            // 02 19 00 4B 00 01 00 16 CB DA 02
            // 0C 00 <hidden> //player name
            // 67 00 03 03

            // 00
            // 51 00 //min level
            // 7A 00 //max level
            // 1E //vocations
            // 04 00 //max team size
            // 03 00 //free slots
            // 58 94 0E 5F //start time
            // 02 //activity type?
            // 19 00 47 00 //activity id?
            // 01 00 //number of members
            // 16 CB DA 02 //player id
            // 0C 00 <hidden> // player name
            // 66 00 //level
            // 03 //vocation id
            // 03 //status (3 = online)

            // 00
            // 51 00
            // 7A 00
            // 00
            // 03 00
            // 02 00
            // 01 00 00 00
            // 02 00 00 00 00 01 00
            // 16 CB DA 02
            // 0C 00 <hidden>
            // 66 00 03 03

        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.TeamFinderTeamLeader);
        }
    }
}
