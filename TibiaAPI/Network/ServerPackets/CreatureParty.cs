using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureParty : ServerPacket
    {
        public uint CreatureId { get; set; }

        public byte PartyShield { get; set; }

        public CreatureParty()
        {
            PacketType = ServerPacketType.CreatureParty;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreatureParty)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            PartyShield = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureParty);
            message.Write(CreatureId);
            message.Write(PartyShield);
        }
    }
}
