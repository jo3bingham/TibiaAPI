using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TournamentRank
    {
        public string WorldName { get; set; }

        public uint Place { get; set; }
        public uint Timestamp { get; set; }
    }

    public class TournamentInformation : ServerPacket
    {
        public byte UnknownByte1 { get; set; }

        public List<(byte Type, uint Cost)> Worlds { get; } = new List<(byte Type, uint Cost)>();

        public List<string> RegularContinents { get; } = new List<string>();
        public List<string> RestrictedContinents { get; } = new List<string>();
        public List<string> StartingTowns { get; } = new List<string>();

        public List<byte> StartingVocations { get; } = new List<byte>();

        public AppearanceInstance RegularOutfit { get; set; }
        public AppearanceInstance RestrictedOutfit { get; set; }

        public TournamentRank RegularHighestRank { get; set; } = new TournamentRank();
        public TournamentRank RestrictedHighestRank { get; set; } = new TournamentRank();

        public string RegularCharacterName { get; set; }
        public string RestrictedCharacterName { get; set; }
        public string Rewards { get; set; }
        public string Scores { get; set; }
        public string SelectedContinent { get; set; }
        public string SelectedTown { get; set; }

        public uint DailyTournamentPlaytime { get; set; }
        public uint Duration { get; set; }
        public uint RegularCharacterId { get; set; }
        public uint RegularDeaths { get; set; }
        public uint RegularPlaytime { get; set; }
        public uint RegularTibiaCoinRewards { get; set; }
        public uint RegularTournamentCoinRewards { get; set; }
        public uint RestrictedCharacterId { get; set; }
        public uint RestrictedDeaths { get; set; }
        public uint RestrictedPlaytime { get; set; }
        public uint RestrictedTibiaCoinRewards { get; set; }
        public uint RestrictedTournamentCoinRewards { get; set; }
        public uint TimeRemaining { get; set; }
        public uint TimestampFinished { get; set; }
        public uint TimestampRunning { get; set; }
        public uint TimestampSignUp { get; set; }

        public ushort CompletedRegularTournaments { get; set; }
        public ushort CompletedRestrictedTournaments { get; set; }
        public ushort DeathPenaltyModifier { get; set; }
        public ushort LootProbability { get; set; }
        public ushort MinimumRequiredLevel { get; set; }
        public ushort RegularTournamentVoucherRewards { get; set; }
        public ushort RestrictedTournamentVoucherRewards { get; set; }
        public ushort SkillMultiplier { get; set; }
        public ushort SpawnRateMultiplier { get; set; }
        public ushort XpMultiplier { get; set; }

        public byte HouseAuctionDurations { get; set; }
        public byte OptionSelection { get; set; }
        public byte PvpType { get; set; }
        public byte RentPercentage { get; set; }
        public byte SelectedVocation { get; set; }
        public byte Type { get; set; }
        public byte Tickets { get; set; }

        public bool HasClaimedVoucher { get; set; }
        public bool HasRegularCharacter { get; set; }
        public bool HasRestrictedCharacter { get; set; }
        public bool HasStarted { get; set; }
        public bool ShowOptionSelection { get; set; }

        public TournamentInformation(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TournamentInformation;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            if (Type == 0)
            {
                Worlds.Add((message.ReadByte(), message.ReadUInt32()));
                Worlds.Add((message.ReadByte(), message.ReadUInt32()));
                ShowOptionSelection = message.ReadBool();
                if (ShowOptionSelection)
                {
                    OptionSelection = message.ReadByte(); // 0 = Create Character Link, 1 = Buy Button
                    Tickets = message.ReadByte();
                    MinimumRequiredLevel = message.ReadUInt16();
                }
            }
            else if (Type == 1)
            {
                TimestampSignUp = message.ReadUInt32();
                TimestampRunning = message.ReadUInt32();
                TimestampFinished = message.ReadUInt32();
                HasStarted = message.ReadBool();
                Duration = message.ReadUInt32();
                TimeRemaining = message.ReadUInt32();
            }
            else if (Type == 2)
            {
                PvpType = message.ReadByte();
                DailyTournamentPlaytime = message.ReadUInt32();
                DeathPenaltyModifier = message.ReadUInt16();
                XpMultiplier = message.ReadUInt16();
                SkillMultiplier = message.ReadUInt16();
                SpawnRateMultiplier = message.ReadUInt16();
                LootProbability = message.ReadUInt16();
                RentPercentage = message.ReadByte();
                HouseAuctionDurations = message.ReadByte();
                Scores = message.ReadString();
                Rewards = message.ReadString();
            }
            else if (Type == 3)
            {
                RegularContinents.Capacity = message.ReadByte();
                for (var i = 0; i < RegularContinents.Capacity; ++i)
                {
                    RegularContinents.Add(message.ReadString());
                }
                RestrictedContinents.Capacity = message.ReadByte();
                for (var i = 0; i < RestrictedContinents.Capacity; ++i)
                {
                    RestrictedContinents.Add(message.ReadString());
                }
                StartingVocations.Capacity = message.ReadByte();
                for (var i = 0; i < StartingVocations.Capacity; ++i)
                {
                    StartingVocations.Add(message.ReadByte());
                }
                StartingTowns.Capacity = message.ReadByte();
                for (var i = 0; i < StartingTowns.Capacity; ++i)
                {
                    StartingTowns.Add(message.ReadString());
                }
                HasClaimedVoucher = message.ReadBool();
                if (HasClaimedVoucher)
                {
                    SelectedContinent = message.ReadString();
                    SelectedVocation = message.ReadByte();
                    SelectedTown = message.ReadString();
                }
            }
            else if (Type == 4)
            {
                HasRegularCharacter = message.ReadBool();
                if (HasRegularCharacter)
                {
                    RegularCharacterId = message.ReadUInt32();
                    RegularCharacterName = message.ReadString();
                    CompletedRegularTournaments = message.ReadUInt16();
                    RegularHighestRank.Place = message.ReadUInt32();
                    RegularHighestRank.Timestamp = message.ReadUInt32();
                    RegularHighestRank.WorldName = message.ReadString();
                    RegularTibiaCoinRewards = message.ReadUInt32();
                    RegularTournamentCoinRewards = message.ReadUInt32();
                    RegularTournamentVoucherRewards = message.ReadUInt16();
                    RegularOutfit = message.ReadCreatureOutfit();
                    RegularPlaytime = message.ReadUInt32();
                    RegularDeaths = message.ReadUInt32();
                }
                HasRestrictedCharacter = message.ReadBool();
                if (HasRestrictedCharacter)
                {
                    RestrictedCharacterId = message.ReadUInt32();
                    RestrictedCharacterName = message.ReadString();
                    CompletedRestrictedTournaments = message.ReadUInt16();
                    RestrictedHighestRank.Place = message.ReadUInt32();
                    RestrictedHighestRank.Timestamp = message.ReadUInt32();
                    RestrictedHighestRank.WorldName = message.ReadString();
                    RestrictedTibiaCoinRewards = message.ReadUInt32();
                    RestrictedTournamentCoinRewards = message.ReadUInt32();
                    RestrictedTournamentVoucherRewards = message.ReadUInt16();
                    RestrictedOutfit = message.ReadCreatureOutfit();
                    RestrictedPlaytime = message.ReadUInt32();
                    RestrictedDeaths = message.ReadUInt32();
                }
            }
            else if (Type == 5)
            {
                // TODO
                UnknownByte1 = message.ReadByte();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TournamentInformation);
            message.Write(Type);
            if (Type == 0)
            {
                message.Write(Worlds[0].Type);
                message.Write(Worlds[0].Cost);
                message.Write(Worlds[1].Type);
                message.Write(Worlds[1].Cost);
                message.Write(ShowOptionSelection);
                if (ShowOptionSelection)
                {
                    message.Write(OptionSelection);
                    message.Write(Tickets);
                    message.Write(MinimumRequiredLevel);
                }
            }
            else if (Type == 1)
            {
                message.Write(TimestampSignUp);
                message.Write(TimestampRunning);
                message.Write(TimestampFinished);
                message.Write(HasStarted);
                message.Write(Duration);
                message.Write(TimeRemaining);
            }
            else if (Type == 2)
            {
                message.Write(PvpType);
                message.Write(DailyTournamentPlaytime);
                message.Write(DeathPenaltyModifier);
                message.Write(XpMultiplier);
                message.Write(SkillMultiplier);
                message.Write(SpawnRateMultiplier);
                message.Write(LootProbability);
                message.Write(RentPercentage);
                message.Write(HouseAuctionDurations);
                message.Write(Scores);
                message.Write(Rewards);
            }
            else if (Type == 3)
            {
                var count = Math.Min(RegularContinents.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(RegularContinents[i]);
                }
                count = Math.Min(RestrictedContinents.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(RestrictedContinents[i]);
                }
                count = Math.Min(StartingVocations.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(StartingVocations[i]);
                }
                count = Math.Min(StartingTowns.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(StartingTowns[i]);
                }
                message.Write(HasClaimedVoucher);
                if (HasClaimedVoucher)
                {
                    message.Write(SelectedContinent);
                    message.Write(SelectedVocation);
                    message.Write(SelectedTown);
                }
            }
            else if (Type == 4)
            {
                message.Write(HasRegularCharacter);
                if (HasRegularCharacter)
                {
                    message.Write(RegularCharacterId);
                    message.Write(RegularCharacterName);
                    message.Write(CompletedRegularTournaments);
                    message.Write(RegularHighestRank.Place);
                    message.Write(RegularHighestRank.Timestamp);
                    message.Write(RegularHighestRank.WorldName);
                    message.Write(RegularTibiaCoinRewards);
                    message.Write(RegularTournamentCoinRewards);
                    message.Write(RegularTournamentVoucherRewards);
                    if (RegularOutfit is OutfitInstance)
                    {
                        message.Write((OutfitInstance)RegularOutfit);
                    }
                    else
                    {
                        message.Write((ushort)0);
                        message.Write((ushort)RegularOutfit.Id);
                    }
                    message.Write(RegularPlaytime);
                    message.Write(RegularDeaths);
                }
                message.Write(HasRestrictedCharacter);
                if (HasRestrictedCharacter)
                {
                    message.Write(RestrictedCharacterId);
                    message.Write(RestrictedCharacterName);
                    message.Write(CompletedRestrictedTournaments);
                    message.Write(RestrictedHighestRank.Place);
                    message.Write(RestrictedHighestRank.Timestamp);
                    message.Write(RestrictedHighestRank.WorldName);
                    message.Write(RestrictedTibiaCoinRewards);
                    message.Write(RestrictedTournamentCoinRewards);
                    message.Write(RestrictedTournamentVoucherRewards);
                    if (RestrictedOutfit is OutfitInstance)
                    {
                        message.Write((OutfitInstance)RestrictedOutfit);
                    }
                    else
                    {
                        message.Write((ushort)0);
                        message.Write((ushort)RestrictedOutfit.Id);
                    }
                    message.Write(RestrictedPlaytime);
                    message.Write(RestrictedDeaths);
                }
            }
            else if (Type == 5)
            {
                // TODO
                message.Write(UnknownByte1);
            }
        }
    }
}
