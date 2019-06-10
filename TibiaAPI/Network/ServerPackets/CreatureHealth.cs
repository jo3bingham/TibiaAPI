using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureHealth : ServerPacket
    {
        public uint CreatureId { get; set; }

        public byte HealthPercent { get; set; }

        public CreatureHealth(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureHealth;
        }
        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            HealthPercent = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureHealth);
            message.Write(CreatureId);
            message.Write(HealthPercent);
        }
    }
}
