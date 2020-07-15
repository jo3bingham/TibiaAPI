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

            // 01 0E 00 00 00

            // This is more than likely an action type, but I don't have premium to confirm.
            var unknown = message.ReadByte(); // 00
            Client.Logger.Error($"[TeamFinderJoinTeam] Unknown: {unknown}");
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.TeamFinderJoinTeam);
        }
    }
}
