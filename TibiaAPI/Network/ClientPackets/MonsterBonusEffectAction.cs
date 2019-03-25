using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MonsterBonusEffectAction : ClientPacket
    {
        public ushort RaceId { get; set; }

        public byte Type { get; set; }
        public byte CharmId { get; set; }

        public MonsterBonusEffectAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MonsterBonusEffectAction;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.MonsterBonusEffectAction)
            {
                return false;
            }

            CharmId = message.ReadByte();
            Type = message.ReadByte();
            if (Type == 1) // Assign (2 = Unassign)
            {
                RaceId = message.ReadUInt16();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MonsterBonusEffectAction);
            message.Write(CharmId);
            message.Write(Type);
            if (Type == 1)
            {
                message.Write(RaceId);
            }
        }
    }
}
