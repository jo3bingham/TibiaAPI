using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SpellGroupDelay : ServerPacket
    {
        public uint Delay { get; set; }

        public byte SpellId { get; set; }

        public SpellGroupDelay()
        {
            PacketType = ServerPacketType.SpellGroupDelay;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.SpellGroupDelay)
            {
                return false;
            }

            SpellId = message.ReadByte();
            Delay = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SpellGroupDelay);
            message.Write(SpellId);
            message.Write(Delay);
        }
    }
}
