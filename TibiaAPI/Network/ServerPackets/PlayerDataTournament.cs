using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerDataTournament : ServerPacket
    {
        uint TimeRemaining { get; set; }

        public PlayerDataTournament(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PlayerDataTournament;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            TimeRemaining = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerDataTournament);
            message.Write(TimeRemaining);
        }
    }
}
