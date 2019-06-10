using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureSpeed : ServerPacket
    {
        public uint CreatureId { get; set; }

        public ushort BaseSpeed { get; set; }
        public ushort Speed { get; set; }

        public CreatureSpeed(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureSpeed;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            BaseSpeed = message.ReadUInt16();
            Speed = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureSpeed);
            message.Write(CreatureId);
            message.Write(BaseSpeed);
            message.Write(Speed);
        }
    }
}
