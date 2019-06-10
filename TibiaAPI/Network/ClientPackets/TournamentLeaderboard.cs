using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TournamentLeaderboard : ClientPacket
    {
        private byte[] unknown;

        public TournamentLeaderboard(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TournamentLeaderboard;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            unknown = message.ReadBytes(6);
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TournamentLeaderboard);
            message.Write(unknown);
        }
    }
}
