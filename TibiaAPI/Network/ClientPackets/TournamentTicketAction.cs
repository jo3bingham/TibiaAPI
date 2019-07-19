using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TournamentTicketAction : ClientPacket
    {
        public string Continent { get; set; }
        public string Town { get; set; }

        public byte Vocation { get; set; }
        public byte Type { get; set; }

        public TournamentTicketAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TournamentTicketAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            Continent = message.ReadString();
            Vocation = message.ReadByte();
            Town = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TournamentTicketAction);
            message.Write(Type);
            message.Write(Continent);
            message.Write(Vocation);
            message.Write(Town);
        }
    }
}
