using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaRace : ServerPacket
    {
        public List<string> Locations { get; } = new List<string>();
        public List<(ushort ObjectId, byte Rarity, string Name, bool IsCumulative)> Loot { get; } =
            new List<(ushort ObjectId, byte Rarity, string Name, bool IsCumulative)>();
        public List<(byte Id, ushort Percentage)> Stats { get; } = new List<(byte Id, ushort Percentage)>();

        public string Name { get; set; }

        public uint CharmPoints { get; set; }
        public uint Experience { get; set; }
        public uint Hitpoints { get; set; }
        public uint TotalKillCount { get; set; }

        public ushort Armor { get; set; }
        public ushort Id { get; set; }
        public ushort KillsToCompleteFirstStage { get; set; }
        public ushort KillsToCompleteSecondStage { get; set; }
        public ushort KillsToCompleteThirdStage { get; set; }
        public ushort Speed { get; set; }
        public ushort UnknownUShort { get; set; }

        public byte CurrentKillStage { get; set; }
        public byte UnknownByte { get; set; }

        public MonsterCyclopediaRace()
        {
            PacketType = ServerPacketType.MonsterCyclopediaRace;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MonsterCyclopediaRace)
            {
                return false;
            }

            Id = message.ReadUInt16();
            Name = message.ReadString();
            CurrentKillStage = message.ReadByte();
            TotalKillCount = message.ReadUInt32();
            KillsToCompleteFirstStage = message.ReadUInt16();
            KillsToCompleteSecondStage = message.ReadUInt16();
            KillsToCompleteThirdStage = message.ReadUInt16();
            UnknownByte = message.ReadByte();

            Loot.Capacity = message.ReadByte();
            for (var i = 0; i < Loot.Capacity; ++i)
            {
                var objectId = message.ReadUInt16();
                var rarity = message.ReadByte();
                var name = string.Empty;
                var isCumulative = false;
                if (objectId > 0)
                {
                    name = message.ReadString();
                    isCumulative = message.ReadBool();
                }
                Loot.Add((objectId, rarity, name, isCumulative));
            }

            if (UnknownByte == 3 || (UnknownByte == 2 && CurrentKillStage > 1))
            {
                CharmPoints = message.ReadUInt32();
                Hitpoints = message.ReadUInt32();
                Experience = message.ReadUInt32();
                Speed = message.ReadUInt16();
                Armor = message.ReadUInt16();
            }

            if ((UnknownByte == 3 && CurrentKillStage > 2) || (UnknownByte == 2 && CurrentKillStage > 2))
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

                if ((UnknownByte == 3 && CurrentKillStage != 3) || (UnknownByte == 2 && CurrentKillStage > 1))
                {
                    UnknownUShort = message.ReadUInt16();
                }
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopediaRace);
            message.Write(Id);
            message.Write(Name);
            message.Write(CurrentKillStage);
            message.Write(TotalKillCount);
            message.Write(KillsToCompleteFirstStage);
            message.Write(KillsToCompleteSecondStage);
            message.Write(KillsToCompleteThirdStage);
            message.Write(UnknownByte);

            var count = Math.Min(Loot.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (objectId, rarity, name, isCumulative) = Loot[i];
                message.Write(objectId);
                message.Write(rarity);
                if (objectId > 0)
                {
                    message.Write(name);
                    message.Write(isCumulative);
                }
            }

            if (UnknownByte == 3 || (UnknownByte == 2 && CurrentKillStage > 1))
            {
                message.Write(CharmPoints);
                message.Write(Hitpoints);
                message.Write(Experience);
                message.Write(Speed);
                message.Write(Armor);
            }

            if ((UnknownByte == 3 && CurrentKillStage > 2) || (UnknownByte == 2 && CurrentKillStage > 2))
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

                if ((UnknownByte == 3 && CurrentKillStage != 3) || (UnknownByte == 2 && CurrentKillStage > 1))
                {
                    message.Write(UnknownUShort);
                }
            }
        }
    }
}
