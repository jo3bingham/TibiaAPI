﻿using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaCharacterInfo : ServerPacket
    {
        public List<(ushort Id, uint Timestamp, bool IsSecret, string Name, string Description, byte Grade)> UnlockedAchievements { get; } =
            new List<(ushort Id, uint Timestamp, bool IsSecret, string Name, string Description, byte Grade)>();
        public List<(byte Type, byte Amount)> DamageReductions { get; } =
            new List<(byte Type, byte Amount)>();
        public List<(uint Timestamp, string Cause)> RecentDeaths { get; } =
            new List<(uint Timestamp, string Cause)>();
        public List<(uint Timestamp, string Description, byte Status)> RecentPvpKills { get; } =
            new List<(uint Timestamp, string Description, byte Status)>();
        public List<(byte Type, ushort Level, ushort Base, ushort Loyalty, ushort Progress)> Skills { get; } =
            new List<(byte Type, ushort Level, ushort Base, ushort Loyalty, ushort Progress)>();

        public AppearanceInstance Outfit { get; set; }

        public string PlayerName { get; set; }
        public string Vocation { get; set; }

        public ulong Experience { get; set; }

        public uint CapacityBonus { get; set; }
        public uint CapacityCurrent { get; set; }
        public uint CapacityMax { get; set; }
        public uint Stamina { get; set; }

        public ushort AchievementPoints { get; set; }
        public ushort Armor { get; set; }
        public ushort Attack { get; set; }
        public ushort BaseXpGain { get; set; }
        public ushort CriticalHitChancePercentBase { get; set; }
        public ushort CriticalHitChancePercentBonus { get; set; }
        public ushort CriticalHitExtraDamagePercentBase { get; set; }
        public ushort CriticalHitExtraDamagePercentBonus { get; set; }
        public ushort Defense { get; set; }
        public ushort Food { get; set; }
        public ushort GrindingAddend { get; set; }
        public ushort HealthCurrent { get; set; }
        public ushort HealthMax { get; set; }
        public ushort HuntingBoostFactor { get; set; }
        public ushort Level { get; set; }
        public ushort LevelDisplay { get; set; }
        public ushort LifeLeechAmountPercentBase { get; set; }
        public ushort LifeLeechAmountPercentBonus { get; set; }
        public ushort LifeLeechChancePercentBase { get; set; }
        public ushort LifeLeechChancePercentBonus { get; set; }
        public ushort ManaCurrent { get; set; }
        public ushort ManaMax { get; set; }
        public ushort ManaLeechAmountPercentBase { get; set; }
        public ushort ManaLeechAmountPercentBonus { get; set; }
        public ushort ManaLeechChancePercentBase { get; set; }
        public ushort ManaLeechChancePercentBonus { get; set; }
        public ushort OfflineTrainingTime { get; set; }
        public ushort RecentDeathsPageCurrent { get; set; }
        public ushort RecentDeathsPageMax { get; set; }
        public ushort RecentPvpKillsPageCurrent { get; set; }
        public ushort RecentPvpKillsPageMax { get; set; }
        public ushort SecretAchievementsMax { get; set; }
        public ushort SpeedBase { get; set; }
        public ushort SpeedCurrent { get; set; }
        public ushort StoreXpBoost { get; set; }
        public ushort Unknown { get; set; }

        public byte AttackType { get; set; }
        public byte BlessingsCurrent { get; set; }
        public byte BlessingsMax { get; set; }
        public byte ConvertedDamagePercent { get; set; }
        public byte ConvertedDamageType { get; set; }
        public byte LevelPercent { get; set; }
        public byte Soul { get; set; }
        public byte Type { get; set; }

        public bool ShowStoreXpBoostButton { get; set; }

        public CyclopediaCharacterInfo(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaCharacterInfo;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CyclopediaCharacterInfo)
            {
                return false;
            }

            Type = message.ReadByte();
            if (Type == 1) // Character Stats
            {
                Experience = message.ReadUInt64();
                Level = message.ReadUInt16();
                LevelPercent = message.ReadByte();
                BaseXpGain = message.ReadUInt16();
                GrindingAddend = message.ReadUInt16();
                StoreXpBoost = message.ReadUInt16();
                HuntingBoostFactor = message.ReadUInt16();
                Food = message.ReadUInt16();
                ShowStoreXpBoostButton = message.ReadBool();
                HealthCurrent = message.ReadUInt16();
                HealthMax = message.ReadUInt16();
                ManaCurrent = message.ReadUInt16();
                ManaMax = message.ReadUInt16();
                Soul = message.ReadByte();
                Stamina = message.ReadUInt32();
                OfflineTrainingTime = message.ReadUInt16();
                SpeedCurrent = message.ReadUInt16();
                SpeedBase = message.ReadUInt16();
                CapacityBonus = message.ReadUInt32();
                CapacityCurrent = message.ReadUInt32();
                CapacityMax = message.ReadUInt32();

                Skills.Capacity = message.ReadByte();
                for (var i = 0; i < Skills.Capacity; ++i)
                {
                    Skills.Add((message.ReadByte(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16()));
                }

                Unknown = message.ReadUInt16(); // Always 218?
                PlayerName = message.ReadString();
                Vocation = message.ReadString();
                LevelDisplay = message.ReadUInt16();
                Outfit = message.ReadCreatureOutfit();
            }
            else if (Type == 2) // Combat Stats
            {
                CriticalHitChancePercentBase = message.ReadUInt16();
                CriticalHitChancePercentBonus = message.ReadUInt16();
                CriticalHitExtraDamagePercentBase = message.ReadUInt16();
                CriticalHitExtraDamagePercentBonus = message.ReadUInt16();
                LifeLeechChancePercentBase = message.ReadUInt16();
                LifeLeechChancePercentBonus = message.ReadUInt16();
                LifeLeechAmountPercentBase = message.ReadUInt16();
                LifeLeechAmountPercentBonus = message.ReadUInt16();
                ManaLeechChancePercentBase = message.ReadUInt16();
                ManaLeechChancePercentBonus = message.ReadUInt16();
                ManaLeechAmountPercentBase = message.ReadUInt16();
                ManaLeechAmountPercentBonus = message.ReadUInt16();
                BlessingsCurrent = message.ReadByte();
                BlessingsMax = message.ReadByte();
                Attack = message.ReadUInt16();
                AttackType = message.ReadByte();
                ConvertedDamagePercent = message.ReadByte();
                ConvertedDamageType = message.ReadByte();
                Armor = message.ReadUInt16();
                Defense = message.ReadUInt16();

                DamageReductions.Capacity = message.ReadByte();
                for (var i = 0; i < DamageReductions.Capacity; ++i)
                {
                    DamageReductions.Add((message.ReadByte(), message.ReadByte()));
                }
            }
            else if (Type == 3) // Recent Deaths
            {
                RecentDeathsPageCurrent = message.ReadUInt16();
                RecentDeathsPageMax = message.ReadUInt16();

                RecentDeaths.Capacity = message.ReadUInt16();
                for (var i = 0; i < RecentDeaths.Capacity; ++i)
                {
                    RecentDeaths.Add((message.ReadUInt32(), message.ReadString()));
                }
            }
            else if (Type == 4) // Recent PvP Kills
            {
                RecentPvpKillsPageCurrent = message.ReadUInt16();
                RecentPvpKillsPageMax = message.ReadUInt16();

                RecentPvpKills.Capacity = message.ReadUInt16();
                for (var i = 0; i < RecentPvpKills.Capacity; ++i)
                {
                    RecentPvpKills.Add((message.ReadUInt32(), message.ReadString(), message.ReadByte()));
                }
            }
            else if (Type == 5) // Achievements
            {
                AchievementPoints = message.ReadUInt16();
                SecretAchievementsMax = message.ReadUInt16();

                UnlockedAchievements.Capacity = message.ReadUInt16();
                for (var i = 0; i < UnlockedAchievements.Capacity; ++i)
                {
                    var id = message.ReadUInt16();
                    var timestamp = message.ReadUInt32();
                    var name = string.Empty;
                    var description = string.Empty;
                    var grade = byte.MinValue;
                    var isSecret = message.ReadBool();
                    if (isSecret)
                    {
                        name = message.ReadString();
                        description = message.ReadString();
                        grade = message.ReadByte();
                    }
                    UnlockedAchievements.Add((id, timestamp, isSecret, name, description, grade));
                }
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CyclopediaCharacterInfo);
            message.Write(Type);
            if (Type == 1)
            {
                message.Write(Experience);
                message.Write(Level);
                message.Write(LevelPercent);
                message.Write(BaseXpGain);
                message.Write(GrindingAddend);
                message.Write(StoreXpBoost);
                message.Write(HuntingBoostFactor);
                message.Write(Food);
                message.Write(ShowStoreXpBoostButton);
                message.Write(HealthCurrent);
                message.Write(HealthMax);
                message.Write(ManaCurrent);
                message.Write(ManaMax);
                message.Write(Soul);
                message.Write(Stamina);
                message.Write(OfflineTrainingTime);
                message.Write(SpeedCurrent);
                message.Write(SpeedBase);
                message.Write(CapacityBonus);
                message.Write(CapacityCurrent);
                message.Write(CapacityMax);

                var count = Math.Min(Skills.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(Skills[i].Type);
                    message.Write(Skills[i].Level);
                    message.Write(Skills[i].Base);
                    message.Write(Skills[i].Loyalty);
                    message.Write(Skills[i].Progress);
                }

                message.Write(Unknown);
                message.Write(PlayerName);
                message.Write(Vocation);
                message.Write(LevelDisplay);

                if (Outfit is OutfitInstance)
                {
                    message.Write((OutfitInstance)Outfit);
                }
                else
                {
                    message.Write((ushort)0);
                    message.Write((ushort)Outfit.Id);
                }
            }
            else if (Type == 2)
            {
                message.Write(CriticalHitChancePercentBase);
                message.Write(CriticalHitChancePercentBonus);
                message.Write(CriticalHitExtraDamagePercentBase);
                message.Write(CriticalHitExtraDamagePercentBonus);
                message.Write(LifeLeechChancePercentBase);
                message.Write(LifeLeechChancePercentBonus);
                message.Write(LifeLeechAmountPercentBase);
                message.Write(LifeLeechAmountPercentBonus);
                message.Write(ManaLeechChancePercentBase);
                message.Write(ManaLeechChancePercentBonus);
                message.Write(LifeLeechAmountPercentBase);
                message.Write(LifeLeechAmountPercentBonus);
                message.Write(BlessingsCurrent);
                message.Write(BlessingsMax);
                message.Write(Attack);
                message.Write(AttackType);
                message.Write(ConvertedDamagePercent);
                message.Write((byte)1);
                message.Write(Armor);
                message.Write(Defense);

                var count = Math.Min(DamageReductions.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(DamageReductions[i].Type);
                    message.Write(DamageReductions[i].Amount);
                }
            }
            else if (Type == 3)
            {
                message.Write(RecentDeathsPageCurrent);
                message.Write(RecentDeathsPageMax);

                var count = Math.Min(RecentDeaths.Count, ushort.MaxValue);
                message.Write((ushort)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(RecentDeaths[i].Timestamp);
                    message.Write(RecentDeaths[i].Cause);
                }
            }
            else if (Type == 4)
            {
                message.Write(RecentPvpKillsPageCurrent);
                message.Write(RecentPvpKillsPageMax);

                var count = Math.Min(RecentPvpKills.Count, ushort.MaxValue);
                message.Write((ushort)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(RecentPvpKills[i].Timestamp);
                    message.Write(RecentPvpKills[i].Description);
                    message.Write(RecentPvpKills[i].Status);
                }
            }
            else if (Type == 5)
            {
                message.Write(AchievementPoints);
                message.Write(SecretAchievementsMax);

                var count = Math.Min(UnlockedAchievements.Count, ushort.MaxValue);
                message.Write((ushort)count);
                for (var i = 0; i < count; ++i)
                {
                    var (Id, Timestamp, IsSecret, Name, Description, Grade) = UnlockedAchievements[i];
                    message.Write(Id);
                    message.Write(Timestamp);
                    message.Write(IsSecret);
                    if (IsSecret)
                    {
                        message.Write(Name);
                        message.Write(Description);
                        message.Write(Grade);
                    }
                }
            }
        }
    }
}
