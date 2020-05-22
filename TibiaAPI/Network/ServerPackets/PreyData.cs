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

        public ushort BonusPercentage { get; set; }
        public ushort TimeLeft { get; set; }
        public ushort TimeLeftUntilFreeListReroll { get; set; }

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
                case PreyDataState.Unknown:
                    {
                        // 67 02 1B 04 89 01 83 02 06 02 4B 04 D9 00 D3 00 BD 02 63 03 FB 03 E3 01 0D 03 7B 01 1B 01 85 01 77 06 D1 01 07 02 74 03 84 06 5A 06 0D 06 82 03 76 03 31 02 81 02 E1 02 0C 00 F5 03 DE 02 71 04 F5 05 8A 06 B7 01 2B 01 F7 03 09 02 C7 02 9B 05 8F 02 C2 03 9F 05 A1 02 09 01 79 03 0C 06 73 02 0F 03 06 07 39 00 B9 06 D8 00 F9 00 1A 01 97 03 08 07 3B 00 61 04 CA 01 DF 00 87 01 04 02 48 04 73 03 09 06 C1 03 DD 00 72 00 3F 07 8A 04 74 00 41 07 9E 05 16 04 4A 02 FD 01 09 03 FC 01 1D 01 22 01 12 03 76 04 08 03 8D 06 0F 04 4C 01 C8 01 78 02 09 00 BE 03 0D 02 F9 05 4D 01 D4 03 6F 03 BC 06 E2 05 2F 05 C5 02 21 06 C4 02 28 00 23 01 FB 05 6E 00 C8 04 E6 05 F2 00 CD 01 1C 02 7F 01 0C 01 4B 01 4D 04 77 04 05 00 D3 02 DD 02 CE 03 66 02 A3 05 13 03 03 01 21 00 51 00 1E 07 6D 03 E1 00 CD 02 73 05 0C 02 9D 05 4A 04 70 00 81 06 4D 00 1A 07 EF 00 96 03 A2 02 10 02 52 02 32 02 65 06 E0 05 56 06 85 05 E3 00 C4 06 EC 04 D2 04 77 02 ED 00 54 06 EF 02 84 01 82 01 C9 06 C0 03 B7 02 39 01 FE 00 C6 06 2E 02 45 02 C9 01 AC 04 78 03 F8 00 26 00 49 04 DF 05 42 01 69 02 3E 00 D3 03 82 06 0B 03 D0 03 B3 02 48 07 7B 00 5E 00 2D 05 74 01 72 04 81 03 72 03 7E 03 44 02 FC 05 B9 02 64 05 4B 02 00 01 10 00 E4 05 4A 00 15 01 02 02 79 04 63 00 B5 01 C1 02 7B 06 6F 04 BA 06 43 00 10 07 83 01 4C 04 03 02 98 03 4E 01 2D 02 71 02 DC 00 DC 02 B6 01 83 03 FA 03 01 01 2E 05 D5 00 1C 04 0B 02 47 01 04 00 FF 00 89 04 25 00 02 03 21 03 80 06 44 01 29 00 65 03 C3 03 E8 05 75 04 68 00 D6 00 B8 02 04 03 9C 05 64 03 42 00 0F 07 CC 01 17 00 00 02 D8 05 1F 04 1C 01 D7 00 CA 05 D7 02 18 07 D4 00 1D 07 50 00 F2 04 73 01 1B 03 0E 00 0D 01 48 00 19 00 FC 00 58 07 31 07 64 00 67 02 7B 04 1D 04 22 00 EF 06 1D 03 78 01 E4 02 1B 06 5B 03 87 05 53 00 20 07 EC 03 D8 02 48 01 7A 04 43 01 28 05 94 03 2D 00 0E 04 6A 03 0C 03 79 01 E3 05 0E 01 4A 01 19 01 7A 00 1B 00 40 07 73 00 41 00 0E 07 44 00 7B 03 BB 06 4F 01 BC 02 42 02 0D 07 40 00 76 00 12 00 26 01 F0 00 38 00 C7 01 31 00 D6 03 AE 04 2F 00 8B 06 1D 00 86 06 E4 00 83 06 D5 03 E5 05 E0 00 49 02 1A 00 F1 04 1D 02 2F 02 80 01 79 02 DB 00 12 04 7C 00 49 07 EE 02 E9 02 E1 05 6C 02 6F 00 21 01 11 00 0E 02 42 07 75 00 35 00 30 00 53 06 1C 03 88 01 16 00 6F 06 CE 06 9D 03 CE 01 1B 05 DA 00 11 04 FB 00 36 01 CF 02 06 00 65 00 9C 03 08 02 73 04 41 01 78 04 24 00 A8 02 BE 02 55 04 1E 01 93 03 5E 03 27 00 85 06 53 02 0D 00 03 00 F7 00 5E 04 27 01 92 03 F8 03 6A 02 75 03 C8 06 91 03 49 00 2A 05 BE 06 6E 04 96 04 D9 02 0A 03 07 00 75 06 08 01 CF 01 C0 06 52 00 F6 00 5F 04 28 01 C6 02 EC 00 1C 00 14 04 45 00 D0 01 69 03 32 00 A7 02 33 00 18 03 C7 06 90 03 95 03 CC 06 81 01 9A 05 C5 06 22 05 E9 05 60 04 24 01 2C 02 30 02 C0 02 B5 02 17 01 43 02 F3 00 EE 00 A0 02 FD 00 6C 00 5C 04 2A 01 C3 02 01 02 3D 00 CF 03 36 00 68 03 69 00 17 03 2A 00 5D 03 1F 00 46 02 11 02 20 00 F3 04 FA 00 F5 00 6A 00 6F 02 78 06 0A 00 14 00 02 01 76 06 08 00 D2 03 37 00 CD 03 E8 03 72 05 20 01 A3 02 87 06 5D 07 6E 03 76 02 6D 02 D0 05 6D 00 C2 06 AD 04 14 03 DA 02 8B 04 15 04 DE 00 46 00 E2 02 10 03 05 01 F0 02 60 05 0F 02 71 03 0F 01 F1 00 CD 06 5F 00 BA 02 CD 05 04 01 7A 06 3D 01 CE 05 E3 02 EA 05 15 00 71 00 0B 00 70 02 34 00 78 00 22 06 33 02 45 01 7C 04 FD 03 1E 00 15 02 D5 02 13 00 F6 01 C8 02 9E 03 67 00 C3 06 46 01 7F 06 0A 06 3A 02 2C 00 E5 02 90 02 0B 01 6E 06 16 03 51 04 F7 01 3A 01 BF 03 1E 04 3F 01 F0 04 C9 05 70 03 29 05 D3 04 9A 03 7D 00 4A 07 13 04 5C 03 3A 00 07 07 1C 07 4F 00 BD 06 79 00 3C 00 DB 02 84 05 4C 00 19 07 2B 00 08 06 3F 00 92 01 D6 02 66 03 7A 03 01 03 2B 02 D0 02 77 00 18 00 85 04 06 01 1B 07 4E 00 47 00 55 06 1F 01 F4 00 23 00 F0 06 BE 01 C2 02 F6 03 F4 03 FF 01 D4 02 DF 02 B6 02 02 00 0F 00 47 02 20 04 10 04 C1 06 0E 03 68 02 FE 01 48 02 3E 01 A2 05 49 01 6D 01 00
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

            TimeLeftUntilFreeListReroll = message.ReadUInt16();
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
            message.Write(TimeLeftUntilFreeListReroll);
            if (Client.VersionNumber > 11606457)
            {
                message.Write(Option);
            }
        }
    }
}
