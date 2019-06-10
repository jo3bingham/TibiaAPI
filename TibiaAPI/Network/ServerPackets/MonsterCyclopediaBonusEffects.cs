using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaBonusEffects : ServerPacket
    {
        public List<(byte Id, string Name, string Description, byte Type, ushort CharmPoints, bool IsPurchased, bool IsAssigned, ushort RaceId, uint RemovalCost)> Charms { get; } =
            new List<(byte Id, string Name, string Description, byte Type, ushort CharmPoints, bool IsPurchased, bool IsAssigned, ushort RaceId, uint RemovalCost)>();
        public List<ushort> AssignableRaceIds { get; } = new List<ushort>();

        public uint CharmPoints { get; set; }

        public byte UnassignedCharms { get; set; }

        public MonsterCyclopediaBonusEffects(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopediaBonusEffects;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CharmPoints = message.ReadUInt32();

            Charms.Capacity = message.ReadByte();
            for (var i = 0; i < Charms.Capacity; ++i)
            {
                var id = message.ReadByte();
                var name = message.ReadString();
                var description = message.ReadString();
                var type = message.ReadByte();
                var charmPoints = message.ReadUInt16();
                var isPurchased = message.ReadBool();
                var isAssigned = message.ReadBool();
                var raceId = ushort.MinValue;
                var removalCost = uint.MinValue;
                if (isAssigned)
                {
                    raceId = message.ReadUInt16();
                    removalCost = message.ReadUInt32();
                }
                Charms.Add((id, name, description, type, charmPoints, isPurchased, isAssigned, raceId, removalCost));
            }

            UnassignedCharms = message.ReadByte();

            AssignableRaceIds.Capacity = message.ReadUInt16();
            for (var i = 0; i < AssignableRaceIds.Capacity; ++i)
            {
                AssignableRaceIds.Add(message.ReadUInt16());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopediaBonusEffects);
            message.Write(CharmPoints);

            var count = Math.Min(Charms.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var charm = Charms[i];
                message.Write(charm.Id);
                message.Write(charm.Name);
                message.Write(charm.Description);
                message.Write(charm.Type);
                message.Write(charm.CharmPoints);
                message.Write(charm.IsPurchased);
                message.Write(charm.IsAssigned);
                if (charm.IsAssigned)
                {
                    message.Write(charm.RaceId);
                    message.Write(charm.RemovalCost);
                }
            }

            message.Write(UnassignedCharms);

            count = Math.Min(AssignableRaceIds.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(AssignableRaceIds[i]);
            }
        }
    }
}
