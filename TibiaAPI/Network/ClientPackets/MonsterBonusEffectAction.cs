using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MonsterBonusEffectAction : ClientPacket
    {
        public ushort RaceId { get; set; }

        public byte CharmId { get; set; }
        public byte Type { get; set; }

        public MonsterBonusEffectAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MonsterBonusEffectAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CharmId = message.ReadByte();
            Type = message.ReadByte();
            if (Type == 1) // Assign (2 = Unassign)
            {
                RaceId = message.ReadUInt16();
            }
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
