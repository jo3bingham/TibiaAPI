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
