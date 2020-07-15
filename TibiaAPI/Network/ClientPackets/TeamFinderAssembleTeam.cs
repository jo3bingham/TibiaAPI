using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TeamFinderAssembleTeam : ClientPacket
    {
        public TeamFinderAssembleTeam(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TeamFinderAssembleTeam;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO

            // 03 52 00 7B 00 02 02 00 01 00 00 01 00 00 00 02 19 00 4B 00

            // 03 51 00 7A 00 00 03 00 02 00 00 01 00 00 00 02 00 00 00 00

            // 03 //type
            // 51 00 //min level
            // 7A 00 //max level
            // 1E //vocations
            // 04 00 //team size
            // 03 00 //free slots
            // 00 //?
            // 58 94 0E 5F //start time
            // 02 19 00 47 00

            // This is more than likely an action type, but I don't have premium to confirm.
            var unknown = message.ReadByte(); // 00
            Client.Logger.Error($"[TeamFinderAssembleTeam] Unknown: {unknown}");
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.TeamFinderAssembleTeam);
        }
    }
}
