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

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.TrackQuestFlags)
            {
                return false;
            }

            QuestIds.Capacity = message.ReadByte();
            for (var i = 0; i < QuestIds.Capacity; ++i)
            {
                QuestIds.Add(message.ReadUInt16());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TrackQuestFlags);
            var count = Math.Min(QuestIds.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(QuestIds[i]);
            }
        }
    }
}
