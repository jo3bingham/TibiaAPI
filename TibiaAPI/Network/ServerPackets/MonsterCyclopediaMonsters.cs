using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaMonsters : ServerPacket
    {
        public List<(ushort Id, byte CurrentStage, byte Occurrence)> Monsters { get; } =
            new List<(ushort Id, byte CurrentStage, byte Occurrence)>();

        public string Name { get; set; }

        public MonsterCyclopediaMonsters(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopediaMonsters;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Name = message.ReadString();
            Monsters.Capacity = message.ReadUInt16();
            for (var i = 0; i < Monsters.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var currentStage = message.ReadByte();
                var occurrence = byte.MinValue;
                if (currentStage > 0 && Client.VersionNumber >= 11807048)
                {
                    occurrence = message.ReadByte();
                }
                Monsters.Add((id, currentStage, occurrence));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopediaMonsters);
            message.Write(Name);
            var count = Math.Min(Monsters.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, CurrentStage, Occurrence) = Monsters[i];
                message.Write(Id);
                message.Write(CurrentStage);
                if (CurrentStage > 0 && Client.VersionNumber >= 11807048)
                {
                    message.Write(Occurrence);
                }
            }
        }
    }
}
