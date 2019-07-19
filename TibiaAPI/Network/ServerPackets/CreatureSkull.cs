using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureSkull : ServerPacket
    {
        public uint CreatureId { get; set; }

        public byte Skull { get; set; }

        public CreatureSkull(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureSkull;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            Skull = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureSkull);
            message.Write(CreatureId);
            message.Write(Skull);
        }
    }
}
