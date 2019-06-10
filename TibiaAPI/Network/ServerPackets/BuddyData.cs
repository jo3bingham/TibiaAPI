using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BuddyData : ServerPacket
    {
        public List<byte> GroupsIds { get; } = new List<byte>();

        public string Description { get; set; }
        public string PlayerName { get; set; }

        public uint Icon { get; set; }
        public uint PlayerId { get; set; }

        public byte ConnectionStatus { get; set; }

        public bool NotifyOnLogin { get; set; }

        public BuddyData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BuddyData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
            PlayerName = message.ReadString();
            Description = message.ReadString();
            Icon = message.ReadUInt32();
            NotifyOnLogin = message.ReadBool();
            ConnectionStatus = message.ReadByte();

            GroupsIds.Capacity = message.ReadByte();
            for (var i = 0; i < GroupsIds.Capacity; ++i)
            {
                GroupsIds.Add(message.ReadByte());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BuddyData);
            message.Write(PlayerId);
            message.Write(PlayerName);
            message.Write(Description);
            message.Write(Icon);
            message.Write(NotifyOnLogin);
            message.Write(ConnectionStatus);

            var count = Math.Min(GroupsIds.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(GroupsIds[i]);
            }
        }
    }
}
