using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MissileEffect : ServerPacket
    {
        public Position FromPosition { get; set; }
        public Position ToPosition { get; set; }

        public byte Effect { get; set; }

        public MissileEffect()
        {
            PacketType = ServerPacketType.MissileEffect;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MissileEffect)
            {
                return false;
            }

            FromPosition = message.ReadPosition();
            ToPosition = message.ReadPosition();
            Effect = message.ReadByte();
            return true;
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
