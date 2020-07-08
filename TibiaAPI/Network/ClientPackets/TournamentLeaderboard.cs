using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TournamentLeaderboard : ClientPacket
    {
        public string WorldName { get; set; }

        public TournamentLeaderboard(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TournamentLeaderboard;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            message.ReadByte(); // TODO
            WorldName = message.ReadString();
            message.ReadBytes(3); // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.TournamentLeaderboard);
        }
    }
}
