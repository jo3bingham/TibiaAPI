using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Dead : ServerPacket
    {
        public DeathType DeathType { get; set; }

        public byte FairFightFactor { get; set; }

        public bool IsSubsequentBlessingApplicable { get; set; }

        public Dead(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Dead;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            DeathType = (DeathType)message.ReadByte();
            if (DeathType == DeathType.Regular)
            {
                FairFightFactor = message.ReadByte();
            }
            IsSubsequentBlessingApplicable = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Dead);
            message.Write((byte)DeathType);
            if (DeathType == DeathType.Regular)
            {
                message.Write(FairFightFactor);
            }
            message.Write(IsSubsequentBlessingApplicable);
        }
    }
}
