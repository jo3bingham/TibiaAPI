using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SpellGroupDelay : ServerPacket
    {
        public uint Delay { get; set; }

        public byte SpellId { get; set; }

        public SpellGroupDelay(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SpellGroupDelay;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            SpellId = message.ReadByte();
            Delay = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SpellGroupDelay);
            message.Write(SpellId);
            message.Write(Delay);
        }
    }
}
