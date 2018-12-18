using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class GraphicalEffects : ServerPacket
    {
        public Position Position { get; set; }

        public uint Unknown { get; set; }

        public byte Effect { get; set; }

        public GraphicalEffects(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.GraphicalEffects;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.GraphicalEffects)
            {
                return false;
            }

            Position = message.ReadPosition();
            Effect = message.ReadByte();
            Unknown = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.GraphicalEffects);
            message.Write(Position);
            message.Write(Effect);
            message.Write(Unknown);
        }
    }
}
