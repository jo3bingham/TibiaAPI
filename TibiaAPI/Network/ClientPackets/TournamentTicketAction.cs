using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TournamentTicketAction : ClientPacket
    {
        public TournamentTicketAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TournamentTicketAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TournamentTicketAction);
        }
    }
}
