using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Channels : ServerPacket
    {
        public List<(ushort Id, string Name)> ChannelList { get; } = new List<(ushort Id, string Name)>();

        public Channels()
        {
            PacketType = ServerPacketType.Channels;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Channels)
            {
                return false;
            }

            ChannelList.Capacity = message.ReadByte();
            for (var i = 0; i < ChannelList.Capacity; ++i)
            {
                var channelId = message.ReadUInt16();
                var channelName = message.ReadString();
                ChannelList.Add((channelId, channelName));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Channels);
            var count = (byte)Math.Min(ChannelList.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, Name) = ChannelList[i];
                message.Write(Id);
                message.Write(Name);
            }
        }
    }
}
