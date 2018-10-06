using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DailyRewardHistory : ServerPacket
    {
        public List<(uint Timestamp, bool WasAdjustedByCustomerSupport, string Text, ushort StreakCount)> History { get; } =
            new List<(uint Timestamp, bool WasAdjustedByCustomerSupport, string Text, ushort StreakCount)>();

        public DailyRewardHistory()
        {
            PacketType = ServerPacketType.DailyRewardHistory;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.DailyRewardHistory)
            {
                return false;
            }

            History.Capacity = message.ReadByte();
            for (var i = 0; i < History.Capacity; ++i)
            {
                var timestamp = message.ReadUInt32();
                var wasAdjustedByCustomerSupport = message.ReadBool();
                var text = message.ReadString();
                var streakCount = message.ReadUInt16();
                History.Add((timestamp, wasAdjustedByCustomerSupport, text, streakCount));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DailyRewardHistory);
            var count = Math.Min(History.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (Timestamp, WasAdjustedByCustomerSupport, Text, StreakCount) = History[i];
                message.Write(Timestamp);
                message.Write(WasAdjustedByCustomerSupport);
                message.Write(Text);
                message.Write(StreakCount);
            }
        }
    }
}
