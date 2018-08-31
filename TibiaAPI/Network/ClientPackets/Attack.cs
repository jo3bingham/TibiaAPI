using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Attack : ClientPacket
    {
        public uint CreatureId { get; set; }

        public Attack()
        {
            Type = ClientPacketType.Attack;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.Attack)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            message.ReadUInt32(); // Creature id again.
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Attack);
            message.Write(CreatureId);
            message.Write(CreatureId);
        }
    }
}
