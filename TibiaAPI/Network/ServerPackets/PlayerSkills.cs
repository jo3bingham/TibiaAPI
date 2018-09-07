using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerSkills : ServerPacket
    {
        public (ushort Level, ushort Base, byte Progress) AxeFighting { get; set; }
        public (ushort Level, ushort Base, byte Progress) ClubFighting { get; set; }
        public (ushort Level, ushort Base, byte Progress) DistanceFighting { get; set; }
        public (ushort Level, ushort Base, byte Progress) Fishing { get; set; }
        public (ushort Level, ushort Base, byte Progress) FistFighting { get; set; }
        public (ushort Level, ushort Base, byte Progress) Shielding { get; set; }
        public (ushort Level, ushort Base, byte Progress) SwordFighting { get; set; }

        public (ushort Level, ushort Base) CriticalHitChance { get; set; }
        public (ushort Level, ushort Base) CriticalHitDamage { get; set; }
        public (ushort Level, ushort Base) LifeLeechAmount { get; set; }
        public (ushort Level, ushort Base) LifeLeechChance { get; set; }
        public (ushort Level, ushort Base) ManaLeechAmount { get; set; }
        public (ushort Level, ushort Base) ManaLeechChance { get; set; }

    public uint BonusCapacity { get; set; }
        public uint MaxCapacity { get; set; }

        public PlayerSkills()
        {
            PacketType = ServerPacketType.PlayerSkills;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PlayerSkills)
            {
                return false;
            }

            FistFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
            ClubFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
            SwordFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
            AxeFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
            DistanceFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
            Shielding = (message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
            Fishing = (message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());

            CriticalHitChance = (message.ReadUInt16(), message.ReadByte());
            CriticalHitDamage = (message.ReadUInt16(), message.ReadByte());
            LifeLeechChance = (message.ReadUInt16(), message.ReadByte());
            LifeLeechAmount = (message.ReadUInt16(), message.ReadByte());
            ManaLeechChance = (message.ReadUInt16(), message.ReadByte());
            ManaLeechAmount = (message.ReadUInt16(), message.ReadByte());

            MaxCapacity = message.ReadUInt32();
            BonusCapacity = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerSkills);
            // TODO
            message.Write(MaxCapacity);
            message.Write(BonusCapacity);
        }
    }
}
