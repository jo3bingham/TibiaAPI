using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MissileEffect : ServerPacket
    {
        public Position FromPosition { get; set; }
        public Position ToPosition { get; set; }

        public byte Effect { get; set; }

        public MissileEffect(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MissileEffect;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            FromPosition = message.ReadPosition();
            ToPosition = message.ReadPosition();
            Effect = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MissileEffect);
            message.Write(FromPosition);
            message.Write(ToPosition);
            message.Write(Effect);
        }
    }
}
