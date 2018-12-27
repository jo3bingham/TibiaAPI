using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class QuestLine : ServerPacket
    {
        public List<(ushort Id, string Name, string Description)> QuestFlags { get; } = new List<(ushort Id, string Name, string Description)>();

        public ushort QuestLineId { get; set; }

        public QuestLine(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.QuestLine;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.QuestLine)
            {
                return false;
            }

            QuestLineId = message.ReadUInt16();
            QuestFlags.Capacity = message.ReadByte();
            for (var i = 0; i < QuestFlags.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var name = message.ReadString();
                var description = message.ReadString();
                QuestFlags.Add((id, name, description));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.QuestLine);
            message.Write(QuestLineId);
            var count = Math.Min(QuestFlags.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var questFlag = QuestFlags[i];
                message.Write(questFlag.Id);
                message.Write(questFlag.Name);
                message.Write(questFlag.Description);
            }
        }
    }
}
