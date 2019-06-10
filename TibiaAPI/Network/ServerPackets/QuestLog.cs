using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class QuestLog : ServerPacket
    {
        public List<(ushort Id, string Name, bool IsCompleted)> Quests { get; } =
            new List<(ushort Id, string Name, bool IsCompleted)>();

        public QuestLog(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.QuestLog;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Quests.Capacity = message.ReadUInt16();
            for (var i = 0; i < Quests.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var name = message.ReadString();
                var isCompleted = message.ReadBool();
                Quests.Add((id, name, isCompleted));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.QuestLog);
            var count = Math.Min(Quests.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, Name, IsCompleted) = Quests[i];
                message.Write(Id);
                message.Write(Name);
                message.Write(IsCompleted);
            }
        }
    }
}
