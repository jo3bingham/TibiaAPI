using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopedia : ServerPacket
    {
        public byte UnknownByte1 { get; set; }

        public List<(byte Id, string Name, string Description, byte Type, ushort CharmPoints, bool IsPurchased, bool IsAssigned, ushort RaceId, uint RemovalCost)> Charms { get; } =
            new List<(byte Id, string Name, string Description, byte Type, ushort CharmPoints, bool IsPurchased, bool IsAssigned, ushort RaceId, uint RemovalCost)>();
        public List<ushort> CharmAssignableRaceIds { get; } = new List<ushort>();
        public List<(string Name, ushort Total, ushort Known)> RaceCollections { get; } =
            new List<(string Name, ushort Total, ushort Known)>();

        public uint CharmPoints { get; set; }

        public byte UnassignedCharms { get; set; }

        public MonsterCyclopedia(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopedia;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            RaceCollections.Capacity = message.ReadUInt16();
            for (var i = 0; i < RaceCollections.Capacity; ++i)
            {
                var name = message.ReadString();
                var total = message.ReadUInt16();
                var known = message.ReadUInt16();
                RaceCollections.Add((name, total, known));
            }

            // TODO
            UnknownByte1 = message.ReadByte(); // Always 216?
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

            CharmAssignableRaceIds.Capacity = message.ReadUInt16();
            for (var i = 0; i < CharmAssignableRaceIds.Capacity; ++i)
            {
                CharmAssignableRaceIds.Add(message.ReadUInt16());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.Write((byte)ServerPacketType.MonsterCyclopedia);

            var count = Math.Min(RaceCollections.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (Name, Total, Known) = RaceCollections[i];
                message.Write(Name);
                message.Write(Total);
                message.Write(Known);
            }

            message.Write(UnknownByte1);
            message.Write(CharmPoints);

            count = Math.Min(Charms.Count, byte.MaxValue);
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

            count = Math.Min(CharmAssignableRaceIds.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(CharmAssignableRaceIds[i]);
            }
        }
    }
}
