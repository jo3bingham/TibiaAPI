using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TournamentInformation : ClientPacket
    {
        public TournamentInformation(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TournamentInformation;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TournamentInformation);
        }
    }
}
