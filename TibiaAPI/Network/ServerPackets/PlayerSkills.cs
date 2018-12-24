using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerSkills : ServerPacket
    {
        public (ushort Level, ushort Base, ushort Unknown, ushort Progress) AxeFighting { get; set; }
        public (ushort Level, ushort Base, ushort Unknown, ushort Progress) ClubFighting { get; set; }
        public (ushort Level, ushort Base, ushort Unknown, ushort Progress) DistanceFighting { get; set; }
        public (ushort Level, ushort Base, ushort Unknown, ushort Progress) Fishing { get; set; }
        public (ushort Level, ushort Base, ushort Unknown, ushort Progress) FistFighting { get; set; }
        public (ushort Level, ushort Base, ushort Unknown, ushort Progress) Magic { get; set; }
        public (ushort Level, ushort Base, ushort Unknown, ushort Progress) Shielding { get; set; }
        public (ushort Level, ushort Base, ushort Unknown, ushort Progress) SwordFighting { get; set; }

        public (ushort Level, ushort Base) CriticalHitChance { get; set; }
        public (ushort Level, ushort Base) CriticalHitDamage { get; set; }
        public (ushort Level, ushort Base) LifeLeechAmount { get; set; }
        public (ushort Level, ushort Base) LifeLeechChance { get; set; }
        public (ushort Level, ushort Base) ManaLeechAmount { get; set; }
        public (ushort Level, ushort Base) ManaLeechChance { get; set; }

        public uint BonusCapacity { get; set; }
        public uint MaxCapacity { get; set; }

        public PlayerSkills(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PlayerSkills;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PlayerSkills)
            {
                return false;
            }

            if (Client.VersionNumber < 12000000)
            {
                FistFighting = (message.ReadUInt16(), message.ReadUInt16(), 0, message.ReadUInt16());
                ClubFighting = (message.ReadUInt16(), message.ReadUInt16(), 0, message.ReadUInt16());
                SwordFighting = (message.ReadUInt16(), message.ReadUInt16(), 0, message.ReadUInt16());
                AxeFighting = (message.ReadUInt16(), message.ReadUInt16(), 0, message.ReadUInt16());
                DistanceFighting = (message.ReadUInt16(), message.ReadUInt16(), 0, message.ReadUInt16());
                Shielding = (message.ReadUInt16(), message.ReadUInt16(), 0, message.ReadUInt16());
                Fishing = (message.ReadUInt16(), message.ReadUInt16(), 0, message.ReadUInt16());
            }
            else
            {
                Magic = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                FistFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                ClubFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                SwordFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                AxeFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                DistanceFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                Shielding = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                Fishing = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
            }

            CriticalHitChance = (message.ReadUInt16(), message.ReadUInt16());
            CriticalHitDamage = (message.ReadUInt16(), message.ReadUInt16());
            LifeLeechChance = (message.ReadUInt16(), message.ReadUInt16());
            LifeLeechAmount = (message.ReadUInt16(), message.ReadUInt16());
            ManaLeechChance = (message.ReadUInt16(), message.ReadUInt16());
            ManaLeechAmount = (message.ReadUInt16(), message.ReadUInt16());

            if (Client.VersionNumber >= 11506055)
            {
                MaxCapacity = message.ReadUInt32();
                BonusCapacity = message.ReadUInt32();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerSkills);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(Magic.Level);
                message.Write(Magic.Base);
                message.Write(Magic.Unknown);
                message.Write(Magic.Progress);
            }

            message.Write(FistFighting.Level);
            message.Write(FistFighting.Base);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(FistFighting.Unknown);
            }
            message.Write(FistFighting.Progress);

            message.Write(ClubFighting.Level);
            message.Write(ClubFighting.Base);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(ClubFighting.Unknown);
            }
            message.Write(ClubFighting.Progress);

            message.Write(SwordFighting.Level);
            message.Write(SwordFighting.Base);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(SwordFighting.Unknown);
            }
            message.Write(SwordFighting.Progress);

            message.Write(AxeFighting.Level);
            message.Write(AxeFighting.Base);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(AxeFighting.Unknown);
            }
            message.Write(AxeFighting.Progress);

            message.Write(DistanceFighting.Level);
            message.Write(DistanceFighting.Base);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(DistanceFighting.Unknown);
            }
            message.Write(DistanceFighting.Progress);

            message.Write(Shielding.Level);
            message.Write(Shielding.Base);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(Shielding.Unknown);
            }
            message.Write(Shielding.Progress);

            message.Write(Fishing.Level);
            message.Write(Fishing.Base);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(Fishing.Unknown);
            }
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

            if (Client.VersionNumber >= 11506055)
            {
                message.Write(MaxCapacity);
                message.Write(BonusCapacity);
            }
        }
    }
}
