using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TrackQuestflags : ClientPacket
    {
        public List<ushort> Ids { get; } = new List<ushort>();

        public TrackQuestflags(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TrackQuestflags;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.TrackQuestflags)
            {
                return false;
            }

            Ids.Capacity = message.ReadByte();
            for (var i = 0; i < Ids.Capacity; ++i)
            {
                Ids.Add(message.ReadUInt16());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TrackQuestflags);
            var count = Math.Min(Ids.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Ids[i]);
            }
        }
    }
}
