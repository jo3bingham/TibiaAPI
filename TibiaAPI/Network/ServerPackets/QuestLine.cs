using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class QuestLine : ServerPacket
    {
        public List<(ushort Id, string Name, string Description)> Questflags { get; } = new List<(ushort Id, string Name, string Description)>();

        public ushort QuestId { get; set; }

        public QuestLine(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.QuestLine;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            QuestId = message.ReadUInt16();
            Questflags.Capacity = message.ReadByte();
            for (var i = 0; i < Questflags.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var name = message.ReadString();
                var description = message.ReadString();
                Questflags.Add((id, name, description));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.QuestLine);
            message.Write(QuestId);
            var count = Math.Min(Questflags.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, Name, Description) = Questflags[i];
                message.Write(Id);
                message.Write(Name);
                message.Write(Description);
            }
        }
    }
}
