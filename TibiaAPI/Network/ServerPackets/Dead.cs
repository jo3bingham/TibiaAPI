using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Dead : ServerPacket
    {
        public DeathType DeathType { get; set; }

        public byte FairFightFactor { get; set; }

        public Dead()
        {
            PacketType = ServerPacketType.Dead;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Dead)
            {
                return false;
            }

            DeathType = (DeathType)message.ReadByte();
            if (DeathType == DeathType.Regular)
            {
                FairFightFactor = message.ReadByte();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Dead);
            message.Write((byte)DeathType);
            if (DeathType == DeathType.Regular)
            {
                message.Write(FairFightFactor);
            }
        }
    }
}
