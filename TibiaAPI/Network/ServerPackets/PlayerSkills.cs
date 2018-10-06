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

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
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

            CriticalHitChance = (message.ReadUInt16(), message.ReadUInt16());
            CriticalHitDamage = (message.ReadUInt16(), message.ReadUInt16());
            LifeLeechChance = (message.ReadUInt16(), message.ReadUInt16());
            LifeLeechAmount = (message.ReadUInt16(), message.ReadUInt16());
            ManaLeechChance = (message.ReadUInt16(), message.ReadUInt16());
            ManaLeechAmount = (message.ReadUInt16(), message.ReadUInt16());

            MaxCapacity = message.ReadUInt32();
            BonusCapacity = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerSkills);
            message.Write(FistFighting.Level);
            message.Write(FistFighting.Base);
            message.Write(FistFighting.Progress);

            message.Write(ClubFighting.Level);
            message.Write(ClubFighting.Base);
            message.Write(ClubFighting.Progress);

            message.Write(SwordFighting.Level);
            message.Write(SwordFighting.Base);
            message.Write(SwordFighting.Progress);

            message.Write(AxeFighting.Level);
            message.Write(AxeFighting.Base);
            message.Write(AxeFighting.Progress);

            message.Write(DistanceFighting.Level);
            message.Write(DistanceFighting.Base);
            message.Write(DistanceFighting.Progress);

            message.Write(Shielding.Level);
            message.Write(Shielding.Base);
            message.Write(Shielding.Progress);

            message.Write(Fishing.Level);
            message.Write(Fishing.Base);
            message.Write(Fishing.Progress);

            message.Write(CriticalHitChance.Level);
            message.Write(CriticalHitChance.Base);

            message.Write(CriticalHitDamage.Level);
            message.Write(CriticalHitDamage.Base);

            message.Write(LifeLeechChance.Level);
            message.Write(LifeLeechChance.Base);

            message.Write(LifeLeechAmount.Level);
            message.Write(LifeLeechAmount.Base);

            message.Write(ManaLeechChance.Level);
            message.Write(ManaLeechChance.Base);

            message.Write(ManaLeechAmount.Level);
            message.Write(ManaLeechAmount.Base);

            message.Write(MaxCapacity);
            message.Write(BonusCapacity);
        }
    }
}
