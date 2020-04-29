using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BestiaryTracker : ServerPacket
    {
        public List<(ushort RaceId, uint TotalKills, ushort StageOneKills, ushort StageTwoKills, ushort StageThreeKills, bool IsComplete)> Creatures { get; } =
            new List<(ushort RaceId, uint TotalKills, ushort StageOneKills, ushort StageTwoKills, ushort StageThreeKills, bool IsComplete)>();

        public BestiaryTracker(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BestiaryTracker;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Creatures.Capacity = message.ReadByte();
            for (var i = 0; i < Creatures.Capacity; ++i)
            {
                var raceId = message.ReadUInt16();
                var totalKills = message.ReadUInt32();
                var stageOneKills = message.ReadUInt16();
                var stageTwoKills = message.ReadUInt16();
                var stageThreeKills = message.ReadUInt16();
                var isComplete = message.ReadBool();
                Creatures.Add((raceId, totalKills, stageOneKills, stageTwoKills, stageThreeKills, isComplete));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BestiaryTracker);
            var count = Math.Min(Creatures.Capacity, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (RaceId, TotalKills, StageOneKills, StageTwoKills, StageThreeKills, IsComplete) = Creatures[i];
                message.Write(RaceId);
                message.Write(TotalKills);
                message.Write(StageOneKills);
                message.Write(StageTwoKills);
                message.Write(StageThreeKills);
                message.Write(IsComplete);
            }
        }
    }
}
