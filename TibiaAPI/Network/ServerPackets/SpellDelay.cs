using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SpellDelay : ServerPacket
    {
        public uint Delay { get; set; }

        public byte SpellId { get; set; }

        public SpellDelay()
        {
            PacketType = ServerPacketType.SpellDelay;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.SpellDelay)
            {
                return false;
            }

            SpellId = message.ReadByte();
            Delay = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SpellDelay);
            message.Write(SpellId);
            message.Write(Delay);
        }
    }
}
