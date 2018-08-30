using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BuddyGroup : ClientPacket
    {
        public List<string> Groups { get; } = new List<string>();

        public BuddyGroup()
        {
            Type = ClientPacketType.BuddyGroup;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            Groups.Capacity = message.ReadByte();
            for (var i = 0; i < Groups.Capacity; ++i)
            {
                Groups.Add(message.ReadString());
            }
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BuddyGroup);
            var count = (byte)Math.Min(Groups.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Groups[i]);
            }
        }
    }
}
