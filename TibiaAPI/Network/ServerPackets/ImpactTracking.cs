using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ImpactTracking : ServerPacket
    {
        public uint Amount { get; set; }

        public bool IsDamage { get; set; }

        public ImpactTracking(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ImpactTracking;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            IsDamage = message.ReadBool();
            Amount = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ImpactTracking);
            message.Write(IsDamage);
            message.Write(Amount);
        }
    }
}
