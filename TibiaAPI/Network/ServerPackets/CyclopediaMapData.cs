using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaMapData : ServerPacket
    {
        public List<ushort> DiscoveredSubAreas { get; } = new List<ushort>();
        public List<ushort> DiscoverableSubAreas { get; } = new List<ushort>();
        public List<(ushort AreaId, bool HasImprovedSpawnRate, ulong DonatedGold)> Donations { get; } =
            new List<(ushort AreaId, bool HasImprovedSpawnRate, ulong DonatedGold)>();
        public List<(ushort Id, byte Status, byte Progress)> MainAreas { get; } =
            new List<(ushort Id, byte Status, byte Progress)>();
        public List<(Position Position, byte State)> PointsOfInterest { get; } =
            new List<(Position Position, byte State)>();
        public List<(ushort Id, List<(ushort RaceId, bool IsKnown, byte Rarity)> Monsters)> SubAreas { get; } =
            new List<(ushort Id, List<(ushort RaceId, bool IsKnown, byte Rarity)> Monsters)>();

        public Position Position { get; set; }

        public CyclopediaMapDataType DataType { get; set; }

        public string MinimapMarkerDescription { get; set; }

        public ulong MinimumGoldDonation { get; set; }

        public ushort AreaId { get; set; }
        public ushort RaceId { get; set; }

        public byte ActiveRaidState { get; set; }
        public byte MinimapMarkerType { get; set; }
        public byte NumberOfPointsOfInterestToDiscover { get; set; }
        public byte NumberOfRaids { get; set; }
        public byte PassageType { get; set; }

        public bool IsUnlocked { get; set; }

        public CyclopediaMapData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaMapData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            DataType = (CyclopediaMapDataType)message.ReadByte();
            if (DataType == CyclopediaMapDataType.MinimapMarker)
            {
                Position = message.ReadPosition();
                MinimapMarkerType = message.ReadByte();
                MinimapMarkerDescription = message.ReadString();
            }
            else if (DataType == CyclopediaMapDataType.DiscoveryData)
            {
                MainAreas.Capacity = message.ReadUInt16();
                for (var i = 0; i < MainAreas.Capacity; ++i)
                {
                    var areaId = message.ReadUInt16();
                    var status = message.ReadByte(); // 0 = not started, 1 = partial discovery w/o NPCs, 2 = partial discover w/ NPCs, 3 = completed
                    var progress = message.ReadByte(); // 0-100%, or 0xFF (cannot be discovered)
                    MainAreas.Add((AreaId, status, progress));
                }
                DiscoveredSubAreas.Capacity = message.ReadUInt16();
                for (var i = 0; i < DiscoveredSubAreas.Capacity; ++i)
                {
                    var areaId = message.ReadUInt16();
                    DiscoveredSubAreas.Add(areaId);
                }
                DiscoverableSubAreas.Capacity = message.ReadUInt16();
                for (var i = 0; i < DiscoverableSubAreas.Capacity; ++i)
                {
                    var areaId = message.ReadUInt16();
                    DiscoverableSubAreas.Add(areaId);
                }
            }
            else if (DataType == CyclopediaMapDataType.ActiveRaid)
            {
                Position = message.ReadPosition();
                ActiveRaidState = message.ReadByte(); // 0 = active (show exclamation mark on map) 
            }
            else if (DataType == CyclopediaMapDataType.ImminentRaidMainArea)
            {
                AreaId = message.ReadUInt16();
                NumberOfRaids = message.ReadByte();
            }
            else if (DataType == CyclopediaMapDataType.ImminentRaidSubArea)
            {
                AreaId = message.ReadUInt16();
                NumberOfRaids = message.ReadByte();
            }
            else if (DataType == CyclopediaMapDataType.SetDiscoveryArea)
            {
                AreaId = message.ReadUInt16();
                NumberOfPointsOfInterestToDiscover = message.ReadByte();
                PointsOfInterest.Capacity = message.ReadByte();
                for (var i = 0; i < PointsOfInterest.Capacity; ++i)
                {
                    var position = message.ReadPosition();
                    var state = message.ReadByte(); // 0 = hidden, 1 = discovered?
                    PointsOfInterest.Add((position, state));
                }
            }
            else if (DataType == CyclopediaMapDataType.Passage)
            {
                Position = message.ReadPosition();
                PassageType = message.ReadByte(); // 0 = w/o subarea id, 1 = w/ subarea id?
                AreaId = message.ReadUInt16();
            }
            else if (DataType == CyclopediaMapDataType.SubAreaMonsters)
            {
                SubAreas.Capacity = message.ReadByte();
                for (var i = 0; i < SubAreas.Capacity; ++i)
                {
                    var areaId = message.ReadUInt16();
                    var monsters = new List<(ushort, bool, byte)>(message.ReadByte());
                    for (var j = 0; j < monsters.Capacity; ++j)
                    {
                        var raceId = message.ReadUInt16();
                        var isKnown = message.ReadBool();
                        var rarity = message.ReadByte(); // 0 = common, 1 = rare, 2 = varying
                        monsters.Add((raceId, isKnown, rarity));
                    }
                    SubAreas.Add((areaId, monsters));
                }
            }
            else if (DataType == CyclopediaMapDataType.MonsterBestiary)
            {
                RaceId = message.ReadUInt16();
                IsUnlocked = message.ReadBool();
            }
            else if (DataType == CyclopediaMapDataType.Donations)
            {
                MinimumGoldDonation = message.ReadUInt64();
                Donations.Capacity = message.ReadByte();
                for (var i = 0; i < Donations.Capacity; ++i)
                {
                    var areaId = message.ReadUInt16();
                    var hasImprovedSpawnRate = message.ReadBool();
                    var donatedGold = message.ReadUInt64();
                    Donations.Add((areaId, hasImprovedSpawnRate, donatedGold));
                }
            }
            else if (DataType == CyclopediaMapDataType.SetCurrentArea)
            {
                AreaId = message.ReadUInt16();
            }
            else
            {
                throw new Exception($"[CyclopediaMapData.ParseFromNetworkMessage] Unknown type: {DataType}");
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CyclopediaMapData);
            message.Write((byte)DataType);
            if (DataType == CyclopediaMapDataType.MinimapMarker)
            {
                message.Write(Position);
                message.Write(MinimapMarkerType);
                message.Write(MinimapMarkerDescription);
            }
            else if (DataType == CyclopediaMapDataType.DiscoveryData)
            {
                var count = Math.Min(MainAreas.Count, ushort.MaxValue);
                message.Write((ushort)count);
                for (var i = 0; i < count; ++i)
                {
                    var (AreaId, Status, Progress) = MainAreas[i];
                    message.Write(AreaId);
                    message.Write(Status);
                    message.Write(Progress);
                }

                count = Math.Min(DiscoveredSubAreas.Count, ushort.MaxValue);
                message.Write((ushort)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(DiscoveredSubAreas[i]);
                }

                count = Math.Min(DiscoverableSubAreas.Count, ushort.MaxValue);
                message.Write((ushort)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(DiscoverableSubAreas[i]);
                }
            }
            else if (DataType == CyclopediaMapDataType.ActiveRaid)
            {
                message.Write(Position);
                message.Write(ActiveRaidState);
            }
            else if (DataType == CyclopediaMapDataType.ImminentRaidMainArea)
            {
                message.Write(AreaId);
                message.Write(NumberOfRaids);
            }
            else if (DataType == CyclopediaMapDataType.ImminentRaidSubArea)
            {
                message.Write(AreaId);
                message.Write(NumberOfRaids);
            }
            else if (DataType == CyclopediaMapDataType.SetDiscoveryArea)
            {
                message.Write(AreaId);
                message.Write(NumberOfPointsOfInterestToDiscover);

                var count = Math.Min(PointsOfInterest.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    var (Position, State) = PointsOfInterest[i];
                    message.Write(Position);
                    message.Write(State);
                }
            }
            else if (DataType == CyclopediaMapDataType.Passage)
            {
                message.Write(Position);
                message.Write(PassageType);
                message.Write(AreaId);
            }
            else if (DataType == CyclopediaMapDataType.SubAreaMonsters)
            {
                var count = Math.Min(SubAreas.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    var (AreaId, Monsters) = SubAreas[i];
                    message.Write(AreaId);

                    var monsterCount = Math.Min(Monsters.Count, byte.MaxValue);
                    message.Write((byte)monsterCount);
                    for (var j = 0; j < monsterCount; ++j)
                    {
                        var (RaceId, IsKnown, Rarity) = Monsters[j];
                        message.Write(RaceId);
                        message.Write(IsKnown);
                        message.Write(Rarity);
                    }
                }
            }
            else if (DataType == CyclopediaMapDataType.MonsterBestiary)
            {
                message.Write(RaceId);
                message.Write(IsUnlocked);
            }
            else if (DataType == CyclopediaMapDataType.Donations)
            {
                message.Write(MinimumGoldDonation);

                var count = Math.Min(Donations.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    var (AreaId, HasImprovedSpawnRate, DonatedGold) = Donations[i];
                    message.Write(AreaId);
                    message.Write(HasImprovedSpawnRate);
                    message.Write(DonatedGold);
                }
            }
            else if (DataType == CyclopediaMapDataType.SetCurrentArea)
            {
                message.Write(AreaId);
            }
        }
    }
}
