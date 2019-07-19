using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureParty : ServerPacket
    {
        public uint CreatureId { get; set; }

        public byte PartyShield { get; set; }

        public CreatureParty(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureParty;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            PartyShield = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureParty);
            message.Write(CreatureId);
            message.Write(PartyShield);
        }
    }
}
