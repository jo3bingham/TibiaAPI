using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class OpenOwnChannel : ServerPacket
    {
        public List<string> InvitedPlayers { get; } = new List<string>();
        public List<string> JoinedPlayers { get; } = new List<string>();

        public string ChannelName { get; set; }

        public ushort ChannelId { get; set; }

        public OpenOwnChannel()
        {
            PacketType = ServerPacketType.OpenOwnChannel;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.OpenOwnChannel)
            {
                return false;
            }

            ChannelId = message.ReadUInt16();
            ChannelName = message.ReadString();

            JoinedPlayers.Capacity = message.ReadUInt16();
            for (var i = 0; i < JoinedPlayers.Capacity; ++i)
            {
                JoinedPlayers.Add(message.ReadString());
            }

            InvitedPlayers.Capacity = message.ReadUInt16();
            for (var i = 0; i < InvitedPlayers.Capacity; ++i)
            {
                InvitedPlayers.Add(message.ReadString());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.OpenOwnChannel);
            message.Write(ChannelId);
            message.Write(ChannelName);

            var count = Math.Min(JoinedPlayers.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(JoinedPlayers[i]);
            }

            count = Math.Min(InvitedPlayers.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(InvitedPlayers[i]);
            }
        }
    }
}
