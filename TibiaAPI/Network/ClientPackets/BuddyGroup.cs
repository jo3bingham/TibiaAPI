using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BuddyGroup : ClientPacket
    {
        public List<string> Groups { get; } = new List<string>();

        public BuddyGroup(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.BuddyGroup;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Groups.Capacity = message.ReadByte();
            for (var i = 0; i < Groups.Capacity; ++i)
            {
                Groups.Add(message.ReadString());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BuddyGroup);
            var count = Math.Min(Groups.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Groups[i]);
            }
        }
    }
}
