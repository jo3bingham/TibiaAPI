using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyData : ServerPacket
    {
        public List<(string Name, AppearanceInstance Outfit)> Preys { get; } =
            new List<(string Name, AppearanceInstance Outfit)>();
        public List<ushort> RaceIds { get; } = new List<ushort>();

        public AppearanceInstance Outfit { get; set; }

        public PreyDataState State { get; set; }

        public string Name { get; set; }

        public uint TimeLeftUntilFreeListReroll { get; set; }

        public ushort BonusPercentage { get; set; }
        public ushort TimeLeft { get; set; }

        public byte BonusRarity { get; set; }
        public byte BonusType { get; set; }
        public byte Index { get; set; }
        public byte Option { get; set; }
        public byte UnlockOption { get; set; }

        public PreyData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PreyData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Index = message.ReadByte();
            State = (PreyDataState)message.ReadByte();
            switch (State)
            {
                case PreyDataState.Locked:
                    {
                        UnlockOption = message.ReadByte(); // 0 = temporary and permanent, 1 = permanent
                    }
                    break;
                case PreyDataState.Inactive:
                    break;
                case PreyDataState.Active:
                    {
                        Name = message.ReadString();
                        Outfit = message.ReadCreatureOutfit();
                        BonusType = message.ReadByte(); // 0 = damage, 1 = defense, 2 = exp, 3 = loot
                        BonusPercentage = message.ReadUInt16();
                        BonusRarity = message.ReadByte();
                        TimeLeft = message.ReadUInt16();
                    }
                    break;
                case PreyDataState.Selection:
                    {
                        Preys.Capacity = message.ReadByte();
                        for (var i = 0; i < Preys.Capacity; i++)
                        {
                            var name = message.ReadString();
                            var outfit = message.ReadCreatureOutfit();
                            Preys.Add((name, outfit));
                        }
                    }
                    break;
                case PreyDataState.SelectionChangeMonster:
                    {
                        BonusType = message.ReadByte(); // 0 = damage, 1 = defense, 2 = exp, 3 = loot
                        BonusPercentage = message.ReadUInt16();
                        BonusRarity = message.ReadByte();

                        Preys.Capacity = message.ReadByte();
                        for (var i = 0; i < Preys.Capacity; i++)
                        {
                            var name = message.ReadString();
                            var outfit = message.ReadCreatureOutfit();
                            Preys.Add((name, outfit));
                        }
                    }
                    break;
                case PreyDataState.ListSelection:
                    {
                        RaceIds.Capacity = message.ReadUInt16();
                        for (var i = 0; i < RaceIds.Capacity; ++i)
                        {
                            RaceIds.Add(message.ReadUInt16());
                        }
                    }
                    break;
                case PreyDataState.WildcardSelection:
                    {
                        BonusType = message.ReadByte(); // 0 = damage, 1 = defense, 2 = exp, 3 = loot
                        BonusPercentage = message.ReadUInt16();
                        BonusRarity = message.ReadByte();

                        RaceIds.Capacity = message.ReadUInt16();
                        for (var i = 0; i < RaceIds.Capacity; ++i)
                        {
                            RaceIds.Add(message.ReadUInt16());
                        }
                    }
                    break;
                default:
                    {
                        throw new Exception($"[PreyData.ParseFromNetworkMessage] Unknown state: {State}");
                    }
            }
            if (Client.VersionNumber >= 125110194)
            {
                TimeLeftUntilFreeListReroll = message.ReadUInt32();
            }
            else
            {
                TimeLeftUntilFreeListReroll = message.ReadUInt16();
            }
            if (Client.VersionNumber > 11606457)
            {
                Option = message.ReadByte(); // 0 = none, 1 = automatic reroll, 2 = locked
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyData);
            message.Write(Index);
            message.Write((byte)State);
            switch (State)
            {
                case PreyDataState.Locked:
                    {
                        message.Write(UnlockOption);
                    }
                    break;
                case PreyDataState.Inactive:
                    break;
                case PreyDataState.Active:
                    {
                        message.Write(Name);
                        if (Outfit is OutfitInstance)
                        {
                            message.Write((OutfitInstance)Outfit);
                        }
                        else
                        {
                            message.Write((ushort)0);
                            message.Write((ushort)Outfit.Id);
                        }
                        message.Write(BonusType);
                        message.Write(BonusPercentage);
                        message.Write(BonusRarity);
                        message.Write(TimeLeft);
                    }
                    break;
                case PreyDataState.Selection:
                    {
                        var count = Math.Min(Preys.Count, byte.MaxValue);
                        message.Write((byte)count);
                        for (var i = 0; i < count; ++i)
                        {
                            var prey = Preys[i];
                            message.Write(prey.Name);
                            if (prey.Outfit is OutfitInstance)
                            {
                                message.Write((OutfitInstance)prey.Outfit);
                            }
                            else
                            {
                                message.Write((ushort)0);
                                message.Write((ushort)prey.Outfit.Id);
                            }
                        }
                    }
                    break;
                case PreyDataState.SelectionChangeMonster:
                    {
                        message.Write(BonusType);
                        message.Write(BonusPercentage);
                        message.Write(BonusRarity);

                        var count = Math.Min(Preys.Count, byte.MaxValue);
                        message.Write((byte)count);
                        for (var i = 0; i < count; ++i)
                        {
                            var prey = Preys[i];
                            message.Write(prey.Name);
                            if (prey.Outfit is OutfitInstance)
                            {
                                message.Write((OutfitInstance)prey.Outfit);
                            }
                            else
                            {
                                message.Write((ushort)0);
                                message.Write((ushort)prey.Outfit.Id);
                            }
                        }
                    }
                    break;
                case PreyDataState.ListSelection:
                    {
                        var count = Math.Min(RaceIds.Count, ushort.MaxValue);
                        message.Write((ushort)count);
                        for (var i = 0; i < count; ++i)
                        {
                            message.Write(RaceIds[i]);
                        }
                    }
                    break;
                case PreyDataState.WildcardSelection:
                    {
                        message.Write(BonusType);
                        message.Write(BonusPercentage);
                        message.Write(BonusRarity);

                        var count = Math.Min(RaceIds.Count, ushort.MaxValue);
                        message.Write((ushort)count);
                        for (var i = 0; i < count; ++i)
                        {
                            message.Write(RaceIds[i]);
                        }
                    }
                    break;
                default:
                    {
                        throw new Exception($"[PreyData.AppendToNetworkMessage] Unknown state: {State}");
                    }
            }
            if (Client.VersionNumber >= 125110194)
            {
                message.Write(TimeLeftUntilFreeListReroll);
            }
            else
            {
                message.Write((ushort)TimeLeftUntilFreeListReroll);
            }
            if (Client.VersionNumber > 11606457)
            {
                message.Write(Option);
            }
        }
    }
}
