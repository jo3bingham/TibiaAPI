using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CollectDailyReward : ClientPacket
    {
        public List<(ushort ObjectId, byte Amount)> Rewards { get; } = new List<(ushort ObjectId, byte Amount)>();

        public byte Unknown { get; set; }

        public CollectDailyReward()
        {
            PacketType = ClientPacketType.CollectDailyReward;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.CollectDailyReward)
            {
                return false;
            }

            Unknown = message.ReadByte();
            Rewards.Capacity = message.ReadByte();
            for (var i = 0; i < Rewards.Capacity; ++i)
            {
                var itemId = message.ReadUInt16();
                var amount = message.ReadByte();
                Rewards.Add((itemId, amount));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CollectDailyReward);
            message.Write(Unknown);
            var count = (byte)Math.Min(Rewards.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var reward = Rewards[i];
                message.Write(reward.ObjectId);
                message.Write(reward.Amount);
            }
        }
    }
}
