using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TournamentInformation : ServerPacket
    {
        public List<(byte Type, uint Cost)> Worlds { get; } = new List<(byte Type, uint Cost)>();

        public List<string> RegularContinents { get; } = new List<string>();
        public List<string> RestrictedContinents { get; } = new List<string>();
        public List<string> StartingTowns { get; } = new List<string>();

        public List<byte> StartingVocations { get; } = new List<byte>();

        public string RegularCharacterName { get; set; }
        public string RestrictedCharacterName { get; set; }
        public string Rewards { get; set; }
        public string Scores { get; set; }
        public string SelectedContinent { get; set; }
        public string SelectedTown { get; set; }

        public uint DailyTournamentPlaytime { get; set; }
        public uint Duration { get; set; }
        public uint RegularCharacterId { get; set; }
        public uint RestrictedCharacterId { get; set; }
        public uint TimeRemaining { get; set; }
        public uint TimestampFinished { get; set; }
        public uint TimestampRunning { get; set; }
        public uint TimestampSignUp { get; set; }

        public ushort DeathPenaltyModifier { get; set; }
        public ushort LootProbability { get; set; }
        public ushort SkillMultiplier { get; set; }
        public ushort SpawnRateMultiplier { get; set; }
        public ushort XpMultiplier { get; set; }

        public byte HouseAuctionDurations { get; set; }
        public byte PvpType { get; set; }
        public byte RentPercentage { get; set; }
        public byte SelectedVocation { get; set; }
        public byte Type { get; set; }
        public byte Unknown1 { get; set; }
        public byte Unknown5 { get; set; }

        public bool HasClaimedVoucher { get; set; }
        public bool HasRegularCharacter { get; set; }
        public bool HasRestrictedCharacter { get; set; }
        public bool HasStarted { get; set; }
        
        private byte[] unknown6;
        private byte[] unknown7;

        public TournamentInformation(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TournamentInformation;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            Client.Logger.Debug($"Type: {Type}");
            if (Type == 0)
            {
                Worlds.Add((message.ReadByte(), message.ReadUInt32()));
                Worlds.Add((message.ReadByte(), message.ReadUInt32()));
                Unknown1 = message.ReadByte();
                Client.Logger.Debug($"Worlds: {Worlds[0].Type}:{Worlds[0].Cost}, {Worlds[1].Type}:{Worlds[1].Cost}, Unknown: {Unknown1}");
            }
            else if (Type == 1)
            {
                TimestampSignUp = message.ReadUInt32();
                TimestampRunning = message.ReadUInt32();
                TimestampFinished = message.ReadUInt32();
                HasStarted = message.ReadBool();
                if (HasStarted)
                {
                    Duration = message.ReadUInt32();
                    TimeRemaining = message.ReadUInt32();
                }
            }
            else if (Type == 2)
            {
                Unknown5 = message.ReadByte();
                Client.Logger.Debug($"Unknown: {Unknown5}");
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
                    unknown6 = message.ReadBytes(34);
                    Client.Logger.Debug($"Regular character unknown data: {BitConverter.ToString(unknown6).Replace('-', ' ')}");
                }
                HasRestrictedCharacter = message.ReadBool();
                if (HasRestrictedCharacter)
                {
                    RestrictedCharacterId = message.ReadUInt32();
                    RestrictedCharacterName = message.ReadString();
                    unknown7 = message.ReadBytes(34);
                    Client.Logger.Debug($"Restricted character unknown data: {BitConverter.ToString(unknown7).Replace('-', ' ')}");
                }
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
                message.Write(Unknown1);
            }
            else if (Type == 1)
            {
                message.Write(TimestampSignUp);
                message.Write(TimestampRunning);
                message.Write(TimestampFinished);
                message.Write(HasStarted);
                if (HasStarted)
                {
                    message.Write(Duration);
                    message.Write(TimeRemaining);
                }
            }
            else if (Type == 2)
            {
                message.Write(Unknown5);
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
                    message.Write(unknown6);
                }
                message.Write(HasRestrictedCharacter);
                if (HasRestrictedCharacter)
                {
                    message.Write(RestrictedCharacterId);
                    message.Write(RestrictedCharacterName);
                    message.Write(unknown7);
                }
            }
        }
    }
}
