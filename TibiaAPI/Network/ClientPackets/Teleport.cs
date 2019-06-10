using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Teleport : ClientPacket
    {
        public Position Position { get; set; }

        public Teleport(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Teleport;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Teleport);
            message.Write(Position);
        }
    }
}
