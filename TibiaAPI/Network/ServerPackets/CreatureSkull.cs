using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureSkull : ServerPacket
    {
        public uint CreatureId { get; set; }

        public byte Skull { get; set; }

        public CreatureSkull()
        {
            PacketType = ServerPacketType.CreatureSkull;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreatureSkull)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            Skull = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureSkull);
            message.Write(CreatureId);
            message.Write(Skull);
        }
    }
}
