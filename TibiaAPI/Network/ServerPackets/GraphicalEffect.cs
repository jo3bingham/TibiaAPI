using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class GraphicalEffect : ServerPacket
    {
        public Position Position { get; set; }

        public byte Effect { get; set; }

        public GraphicalEffect()
        {
            PacketType = ServerPacketType.GraphicalEffect;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.GraphicalEffect)
            {
                return false;
            }

            Position = message.ReadPosition();
            Effect = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.GraphicalEffect);
            message.Write(Position);
            message.Write(Effect);
        }
    }
}
