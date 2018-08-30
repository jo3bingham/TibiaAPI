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
            Type = ClientPacketType.TrackQuestFlags;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
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

        public override void AppendToMessage(NetworkMessage message)
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
