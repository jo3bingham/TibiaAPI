using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BuddyGroupData : ServerPacket
    {
        public List<(byte Id, string Name, bool IsModifiable)> Groups { get; } =
            new List<(byte Id, string Name, bool IsModifiable)>();

        public byte NumberOfUserCreatedGroupsLeft { get; set; }

        public BuddyGroupData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BuddyGroupData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Groups.Capacity = message.ReadByte();
            for (var i = 0; i < Groups.Capacity; ++i)
            {
                var groupId = message.ReadByte();
                var groupName = message.ReadString();
                var isModifiable = message.ReadBool();
                Groups.Add((groupId, groupName, isModifiable));
            }
            NumberOfUserCreatedGroupsLeft = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BuddyGroupData);
            var count = Math.Min(Groups.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, Name, IsModifiable) = Groups[i];
                message.Write(Id);
                message.Write(Name);
                message.Write(IsModifiable);
            }
            message.Write(NumberOfUserCreatedGroupsLeft);
        }
    }
}
