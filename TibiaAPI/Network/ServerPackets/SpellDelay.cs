using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SpellDelay : ServerPacket
    {
        public uint Delay { get; set; }

        public byte SpellId { get; set; }

        public SpellDelay(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SpellDelay;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            SpellId = message.ReadByte();
            Delay = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SpellDelay);
            message.Write(SpellId);
            message.Write(Delay);
        }
    }
}
