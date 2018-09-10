using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ImpactTracking : ServerPacket
    {
        public uint Amount { get; set; }

        public bool IsDamage { get; set; }

        public ImpactTracking()
        {
            PacketType = ServerPacketType.ImpactTracking;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ImpactTracking)
            {
                return false;
            }

            IsDamage = message.ReadBool();
            Amount = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ImpactTracking);
            message.Write(IsDamage);
            message.Write(Amount);
        }
    }
}
