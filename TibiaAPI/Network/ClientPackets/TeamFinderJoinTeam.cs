using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TeamFinderJoinTeam : ClientPacket
    {
        public byte Type { get; set; }

        public TeamFinderJoinTeam(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TeamFinderJoinTeam;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            Type = message.ReadByte();
            if (Type == 0)
            {
            }
            else if (Type == 1)
            {
                message.ReadBytes(4); // 0E 00 00 00
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.TeamFinderJoinTeam);
        }
    }
}
