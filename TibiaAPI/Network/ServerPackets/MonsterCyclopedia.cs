using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopedia : ServerPacket
    {
        public List<(string Name, string Description, byte UnknownByte, ushort CharmPoints, ushort UnknownShort, byte Id)> Charms { get; } =
            new List<(string Name, string Description, byte UnknownByte, ushort CharmPoints, ushort UnknownShort, byte Id)>();
        public List<ushort> CompletedMonsterIds { get; } = new List<ushort>();
        public List<(string Name, ushort Total, ushort Known)> MonsterRaces { get; } =
            new List<(string Name, ushort Total, ushort Known)>();

        public uint CharmPoints { get; set; }

        public byte Unknown1 { get; set; }

        public MonsterCyclopedia(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopedia;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MonsterCyclopedia)
            {
                return false;
            }

            MonsterRaces.Capacity = message.ReadUInt16();
            for (var i = 0; i < MonsterRaces.Capacity; ++i)
            {
                var name = message.ReadString();
                var total = message.ReadUInt16();
                var known = message.ReadUInt16();
                MonsterRaces.Add((name, total, known));
            }

            // Todo: Figure out this unknown.
            Unknown1 = message.ReadByte(); // Always 216?
            CharmPoints = message.ReadUInt32();

            Charms.Capacity = message.ReadUInt16();
            for (var i = 0; i < Charms.Capacity; ++i)
            {
                var name = message.ReadString();
                var description = message.ReadString();
                var unknownByte = message.ReadByte();
                var charmPoints = message.ReadUInt16();
                var unknownShort = message.ReadUInt16();
                var id = message.ReadByte();
                Charms.Add((name, description, unknownByte, charmPoints, unknownShort, id));
            }

            CompletedMonsterIds.Capacity = message.ReadUInt16();
            for (var i = 0; i < CompletedMonsterIds.Capacity; ++i)
            {
                CompletedMonsterIds.Add(message.ReadUInt16());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopedia);

            var count = (ushort)Math.Min(MonsterRaces.Count, ushort.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var (Name, Total, Known) = MonsterRaces[i];
                message.Write(Name);
                message.Write(Total);
                message.Write(Known);
            }

            message.Write(Unknown1);
            message.Write(CharmPoints);

            count = (ushort)Math.Min(Charms.Count, ushort.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var charm = Charms[i];
                message.Write(charm.Name);
                message.Write(charm.Description);
                message.Write(charm.UnknownByte);
                message.Write(charm.CharmPoints);
                message.Write(charm.UnknownShort);
                message.Write(charm.Id);
            }

            count = (ushort)Math.Min(CompletedMonsterIds.Count, ushort.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(CompletedMonsterIds[i]);
            }
        }
    }
}
