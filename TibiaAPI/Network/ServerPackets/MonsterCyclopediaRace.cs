using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaRace : ServerPacket
    {
        public ushort UnknownUShort1 { get; set; }
        public byte UnknownByte1 { get; set; }

        public List<string> Locations { get; } = new List<string>();
        public List<(ushort ObjectId, byte Rarity, bool IsFromSpecialEvent, string Name, bool IsCumulative)> Loot { get; } =
            new List<(ushort ObjectId, byte Rarity, bool IsFromSpecialEvent, string Name, bool IsCumulative)>();
        public List<(byte Id, ushort Percentage)> Stats { get; } = new List<(byte Id, ushort Percentage)>();

        public string Name { get; set; }

        public uint CharmPoints { get; set; }
        public uint Experience { get; set; }
        public uint Hitpoints { get; set; }
        public uint CharmRemovalCost { get; set; }
        public uint TotalKillCount { get; set; }

        public ushort Armor { get; set; }
        public ushort Id { get; set; }
        public ushort KillsToCompleteFirstStage { get; set; }
        public ushort KillsToCompleteSecondStage { get; set; }
        public ushort KillsToCompleteThirdStage { get; set; }
        public ushort Speed { get; set; }

        public byte CharmId { get; set; }
        public byte CurrentKillStage { get; set; }
        public byte Difficulty { get; set; }
        public byte Occurrence { get; set; }

        public bool HasCharmAssigned { get; set; }

        public MonsterCyclopediaRace(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopediaRace;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Id = message.ReadUInt16();
            Name = message.ReadString();
            CurrentKillStage = message.ReadByte();
            TotalKillCount = message.ReadUInt32();
            KillsToCompleteFirstStage = message.ReadUInt16();
            KillsToCompleteSecondStage = message.ReadUInt16();
            KillsToCompleteThirdStage = message.ReadUInt16();
            Difficulty = message.ReadByte();
            if (Client.VersionNumber >= 11807048)
            {
                Occurrence = message.ReadByte();
            }

            Loot.Capacity = message.ReadByte();
            for (var i = 0; i < Loot.Capacity; ++i)
            {
                var objectId = message.ReadUInt16();
                var rarity = message.ReadByte();
                var isFromSpecialEvent = false;
                if (Client.VersionNumber >= 11807048)
                {
                    isFromSpecialEvent = message.ReadBool();
                }
                var name = string.Empty;
                var isCumulative = false;
                if (objectId > 0)
                {
                    name = message.ReadString();
                    isCumulative = message.ReadBool();
                }
                Loot.Add((objectId, rarity, isFromSpecialEvent, name, isCumulative));
            }

            if (Client.VersionNumber >= 11807048)
            {
                if (CurrentKillStage > 1)
                {
                    CharmPoints = message.ReadUInt32();
                    Hitpoints = message.ReadUInt32();
                    Experience = message.ReadUInt32();
                    Speed = message.ReadUInt16();
                    Armor = message.ReadUInt16();

                    if (CurrentKillStage > 2)
                    {
                        Stats.Capacity = message.ReadByte();
                        for (var i = 0; i < Stats.Capacity; ++i)
                        {
                            var statId = message.ReadByte();
                            var percentage = message.ReadUInt16();
                            Stats.Add((statId, percentage));
                        }

                        Locations.Capacity = message.ReadUInt16();
                        for (var i = 0; i < Locations.Capacity; ++i)
                        {
                            Locations.Add(message.ReadString());
                        }

                        if (CurrentKillStage > 3)
                        {
                            HasCharmAssigned = message.ReadBool();
                            if (HasCharmAssigned)
                            {
                                CharmId = message.ReadByte();
                                CharmRemovalCost = message.ReadUInt32();
                            }
                            else
                            {
                                // TODO
                                UnknownByte1 = message.ReadByte(); // always 1?
                            }
                        }
                    }
                }
            }
            else
            {
                if (Difficulty == 3 || (Difficulty == 2 && CurrentKillStage > 1))
                {
                    CharmPoints = message.ReadUInt32();
                    Hitpoints = message.ReadUInt32();
                    Experience = message.ReadUInt32();
                    Speed = message.ReadUInt16();
                    Armor = message.ReadUInt16();
                }

                if ((Difficulty == 3 && CurrentKillStage > 2) || (Difficulty == 2 && CurrentKillStage > 2))
                {
                    Stats.Capacity = message.ReadByte();
                    for (var i = 0; i < Stats.Capacity; ++i)
                    {
                        var statId = message.ReadByte();
                        var percentage = message.ReadUInt16();
                        Stats.Add((statId, percentage));
                    }

                    Locations.Capacity = message.ReadUInt16();
                    for (var i = 0; i < Locations.Capacity; ++i)
                    {
                        Locations.Add(message.ReadString());
                    }

                    if ((Difficulty == 3 && CurrentKillStage != 3) || (Difficulty == 2 && CurrentKillStage > 1))
                    {
                        // TODO
                        UnknownUShort1 = message.ReadUInt16();
                    }
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.Write((byte)ServerPacketType.MonsterCyclopediaRace);
            message.Write(Id);
            message.Write(Name);
            message.Write(CurrentKillStage);
            message.Write(TotalKillCount);
            message.Write(KillsToCompleteFirstStage);
            message.Write(KillsToCompleteSecondStage);
            message.Write(KillsToCompleteThirdStage);
            message.Write(Difficulty);
            if (Client.VersionNumber >= 11807048)
            {
                message.Write(Occurrence);
            }

            var count = Math.Min(Loot.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (ObjectId, Rarity, IsFromSpecialEvent, Name, IsCumulative) = Loot[i];
                message.Write(ObjectId);
                message.Write(Rarity);
                if (Client.VersionNumber >= 11807048)
                {
                    message.Write(IsFromSpecialEvent);
                }
                if (ObjectId > 0)
                {
                    message.Write(Name);
                    message.Write(IsCumulative);
                }
            }

            if (Client.VersionNumber >= 11807048)
            {
                if (CurrentKillStage > 1)
                {
                    message.Write(CharmPoints);
                    message.Write(Hitpoints);
                    message.Write(Experience);
                    message.Write(Speed);
                    message.Write(Armor);

                    if (CurrentKillStage > 2)
                    {
                        count = Math.Min(Stats.Count, byte.MaxValue);
                        message.Write((byte)count);
                        for (var i = 0; i < count; ++i)
                        {
                            var (StatId, Percentage) = Stats[i];
                            message.Write(StatId);
                            message.Write(Percentage);
                        }

                        count = Math.Min(Locations.Count, ushort.MaxValue);
                        message.Write((ushort)count);
                        for (var i = 0; i < count; ++i)
                        {
                            message.Write(Locations[i]);
                        }

                        if (CurrentKillStage > 3)
                        {
                            message.Write(HasCharmAssigned);
                            if (HasCharmAssigned)
                            {
                                message.Write(CharmId);
                                message.Write(CharmRemovalCost);
                            }
                            else
                            {
                                message.Write(UnknownByte1);
                            }
                        }
                    }
                }
            }
            else
            {
                if (Difficulty == 3 || (Difficulty == 2 && CurrentKillStage > 1))
                {
                    message.Write(CharmPoints);
                    message.Write(Hitpoints);
                    message.Write(Experience);
                    message.Write(Speed);
                    message.Write(Armor);
                }

                if ((Difficulty == 3 && CurrentKillStage > 2) || (Difficulty == 2 && CurrentKillStage > 2))
                {
                    count = Math.Min(Stats.Count, byte.MaxValue);
                    message.Write((byte)count);
                    for (var i = 0; i < count; ++i)
                    {
                        var (statId, percentage) = Stats[i];
                        message.Write(statId);
                        message.Write(percentage);
                    }

                    count = Math.Min(Locations.Count, ushort.MaxValue);
                    message.Write((ushort)count);
                    for (var i = 0; i < count; ++i)
                    {
                        message.Write(Locations[i]);
                    }

                    if ((Difficulty == 3 && CurrentKillStage != 3) || (Difficulty == 2 && CurrentKillStage > 1))
                    {
                        message.Write(UnknownUShort1);
                    }
                }
            }
        }
    }
}
