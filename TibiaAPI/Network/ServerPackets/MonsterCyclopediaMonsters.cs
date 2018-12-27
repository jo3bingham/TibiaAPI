using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaMonsters : ServerPacket
    {
        public List<(ushort Id, byte CurrentStage)> Monsters { get; } =
            new List<(ushort Id, byte CurrentStage)>();

        public string Name { get; set; }

        public MonsterCyclopediaMonsters(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopediaMonsters;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MonsterCyclopediaMonsters)
            {
                return false;
            }

            Name = message.ReadString();
            Monsters.Capacity = message.ReadUInt16();
            for (var i = 0; i < Monsters.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var currentStage = message.ReadByte();
                Monsters.Add((id, currentStage));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopediaMonsters);
            message.Write(Name);
            var count = Math.Min(Monsters.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, CurrentStage) = Monsters[i];
                message.Write(Id);
                message.Write(CurrentStage);
            }
        }
    }
}
