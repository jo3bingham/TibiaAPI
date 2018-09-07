using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TrackQuestFlags : ServerPacket
    {
        public List<(ushort Id, string Log, string Line, string Description)> TrackedQuests { get; } =
            new List<(ushort Id, string Log, string Line, string Description)>();

        public string QuestLog { get; set; }
        public string QuestLine { get; set; }

        public ushort QuestId { get; set; }

        public byte AvailableTrackingSlots { get; set; }

        public bool IsPremium { get; set; }

        public TrackQuestFlags()
        {
            PacketType = ServerPacketType.TrackQuestFlags;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.TrackQuestFlags)
            {
                return false;
            }

            IsPremium = message.ReadBool();
            if (IsPremium)
            {
                AvailableTrackingSlots = message.ReadByte();
                TrackedQuests.Capacity = message.ReadByte();
                for (var i = 0; i < TrackedQuests.Capacity; ++i)
                {
                    var id = message.ReadUInt16();
                    var log = message.ReadString();
                    var line = message.ReadString();
                    var description = message.ReadString();
                    TrackedQuests.Add((id, log, line, description));
                }
            }
            else
            {
                QuestId = message.ReadUInt16();
                QuestLog = message.ReadString();
                QuestLine = message.ReadString();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TrackQuestFlags);
            message.Write(IsPremium);
            if (IsPremium)
            {
                message.Write(AvailableTrackingSlots);
                var count = (byte)Math.Min(TrackedQuests.Count, byte.MaxValue);
                message.Write(count);
                for (var i = 0; i < count; ++i)
                {
                    var quest = TrackedQuests[i];
                    message.Write(quest.Id);
                    message.Write(quest.Log);
                    message.Write(quest.Line);
                    message.Write(quest.Description);
                }
            }
            else
            {
                message.Write(QuestId);
                message.Write(QuestLog);
                message.Write(QuestLine);
            }
        }
    }
}
