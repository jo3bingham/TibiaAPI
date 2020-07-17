using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ClaimTournamentReward : ClientPacket
    {
        public ClaimTournamentReward(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.ClaimTournamentReward;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.ClaimTournamentReward);
        }
    }
}
