using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.DailyRewards;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DailyRewardBasic : ServerPacket
    {
        public List<(string Text, byte UnlockStreak)> Bonuses { get; } =
            new List<(string Text, byte UnlockStreak)>();
        public List<(DailyReward FreeReward, DailyReward PremiumReward)> DailyRewards { get; } =
            new List<(DailyReward FreeReward, DailyReward PremiumReward)>();

        public byte Unknown { get; set; }

        public DailyRewardBasic(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.DailyRewardBasic;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.DailyRewardBasic)
            {
                return false;
            }

            DailyRewards.Capacity = message.ReadByte();
            for (var i = 0; i < DailyRewards.Capacity; ++i)
            {
                var freeReward = message.ReadDailyReward();
                var premiumReward = message.ReadDailyReward();
                DailyRewards.Add((freeReward, premiumReward));
            }

            Bonuses.Capacity = message.ReadByte();
            for (var i = 0; i < Bonuses.Capacity; ++i)
            {
                var text = message.ReadString();
                var unlockStreak = message.ReadByte();
                Bonuses.Add((text, unlockStreak));
            }

            // TODO: Figure out this unknown.
            Unknown = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DailyRewardBasic);
            var count = Math.Min(DailyRewards.Capacity, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (FreeReward, PremiumReward) = DailyRewards[i];
                message.Write(FreeReward);
                message.Write(PremiumReward);
            }

            count = Math.Min(Bonuses.Count, byte.MaxValue);
            message.Write((byte)count);
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
