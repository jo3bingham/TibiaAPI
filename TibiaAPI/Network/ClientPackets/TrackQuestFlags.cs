using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TrackQuestFlags : ClientPacket
    {
        public List<ushort> QuestIds { get; } = new List<ushort>();

        public TrackQuestFlags()
        {
            PacketType = ClientPacketType.TrackQuestFlags;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.TrackQuestFlags)
            {
                return false;
            }

            var count = message.ReadByte();
            for (var i = 0; i < count; ++i)
            {
                QuestIds.Add(message.ReadUInt16());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TrackQuestFlags);
            var count = (byte)Math.Min(QuestIds.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(QuestIds[i]);
            }
        }
    }
}
