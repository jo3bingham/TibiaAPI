using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class QuestLog : ServerPacket
    {
        public List<(ushort Id, string Name, bool IsCompleted)> Quests { get; } = new List<(ushort Id, string Name, bool IsCompleted)>();

        public QuestLog()
        {
            PacketType = ServerPacketType.QuestLog;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.QuestLog)
            {
                return false;
            }

            Quests.Capacity = message.ReadUInt16();
            for (var i = 0; i < Quests.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var name = message.ReadString();
                var isCompleted = message.ReadBool();
                Quests.Add((id, name, isCompleted));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.QuestLog);
            var count = (ushort)Math.Min(Quests.Count, ushort.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var quest = Quests[i];
                message.Write(quest.Id);
                message.Write(quest.Name);
                message.Write(quest.IsCompleted);
            }
        }
    }
}
