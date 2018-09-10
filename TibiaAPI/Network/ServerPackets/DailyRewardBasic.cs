using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DailyRewardBasic : ServerPacket
    {
        public List<(string Text, byte UnlockStreak)> Bonuses { get; } = new List<(string Text, byte UnlockStreak)>();

        public byte Unknown { get; set; }

        public DailyRewardBasic()
        {
            PacketType = ServerPacketType.DailyRewardBasic;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.DailyRewardBasic)
            {
                return false;
            }

            // TODO
            var numberOfDailyRewards = message.ReadByte();
            for (var i = 0; i < numberOfDailyRewards; ++i)
            {
                message.ReadDailyReward();
                message.ReadDailyReward();
            }

            Bonuses.Capacity = message.ReadByte();
            for (var i = 0; i < Bonuses.Capacity; ++i)
            {
                var text = message.ReadString();
                var unlockStreak = message.ReadByte();
                Bonuses.Add((text, unlockStreak));
            }

            Unknown = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DailyRewardBasic);
            // TODO
            var count = (byte)Math.Min(Bonuses.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var (Text, UnlockStreak) = Bonuses[i];
                message.Write(Text);
                message.Write(UnlockStreak);
            }
            message.Write(Unknown);
        }
    }
}
