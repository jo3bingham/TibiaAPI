using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyHuntingTaskBaseData : ServerPacket
    {
        public List<(byte Difficulty, byte Stars, ushort FirstKills, ushort FirstReward, ushort SecondKills, ushort SecondReward)> Options { get; } =
            new List<(byte Difficulty, byte Stars, ushort FirstKills, ushort FirstReward, ushort SecondKills, ushort SecondReward)>();
        public List<(ushort RaceId, byte Difficulty)> Preys { get; } =
            new List<(ushort RaceId, byte Difficulty)>();

        public PreyHuntingTaskBaseData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PreyHuntingTaskBaseData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Preys.Capacity = message.ReadUInt16();
            for (var i = 0; i < Preys.Capacity; ++i)
            {
                var raceId = message.ReadUInt16();
                var difficulty = message.ReadByte();
                Preys.Add((raceId, difficulty));
            }
            Options.Capacity = message.ReadByte();
            for (var i = 0; i < Options.Capacity; ++i)
            {
                var difficulty = message.ReadByte();
                var stars = message.ReadByte();
                var firstKills = message.ReadUInt16();
                var firstReward = message.ReadUInt16();
                var secondKills = message.ReadUInt16();
                var secondReward = message.ReadUInt16();
                Options.Add((difficulty, stars, firstKills, firstReward, secondKills, secondReward));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyHuntingTaskBaseData);
            var count = Math.Min(ushort.MaxValue, Preys.Capacity);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (RaceId, Difficulty) = Preys[i];
                message.Write(RaceId);
                message.Write(Difficulty);
            }
            count = Math.Min(byte.MaxValue, Options.Capacity);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (Difficulty, Stars, FirstKills, FirstReward, SecondKills, SecondReward) = Options[i];
                message.Write(Difficulty);
                message.Write(Stars);
                message.Write(FirstKills);
                message.Write(FirstReward);
                message.Write(SecondKills);
                message.Write(SecondReward);
            }
        }
    }
}
