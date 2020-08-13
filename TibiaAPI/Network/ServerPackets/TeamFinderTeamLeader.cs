using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TeamFinderTeamLeader : ServerPacket
    {
        public List<ushort> UnknownList1 { get; } = new List<ushort>();

        public List<(uint Id, string Name, ushort Level, byte Vocation, byte Status)> Members { get; } =
            new List<(uint Id, string Name, ushort Level, byte Vocation, byte Status)>();

        public uint StartTime { get; set; }

        public ushort FreeSlots { get; set; }
        public ushort MaxLevel { get; set; }
        public ushort MinLevel { get; set; }
        public ushort TeamSize { get; set; }

        public byte Vocations { get; set; } // Bit Flag

        public bool IsUpToDate { get; set; }

        public TeamFinderTeamLeader(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TeamFinderTeamLeader;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            IsUpToDate = message.ReadBool();
            if (!IsUpToDate)
            {
                MinLevel = message.ReadUInt16();
                MaxLevel = message.ReadUInt16();
                Vocations = message.ReadByte();
                TeamSize = message.ReadUInt16();
                FreeSlots = message.ReadUInt16();
                StartTime = message.ReadUInt32();
                UnknownList1.Capacity = message.ReadByte();
                for (var i = 0; i < UnknownList1.Capacity; i++)
                {
                    // TODO
                    UnknownList1.Add(message.ReadUInt16()); // 19 00 47 00
                }
                Members.Capacity = message.ReadUInt16();
                for (var i = 0; i < Members.Capacity; i++)
                {
                    var id = message.ReadUInt32();
                    var name = message.ReadString();
                    var level = message.ReadUInt16();
                    var vocation = message.ReadByte();
                    var status = message.ReadByte();
                    Members.Add((id, name, level, vocation, status));
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TeamFinderTeamLeader);
            message.Write(IsUpToDate);
            if (!IsUpToDate)
            {
                message.Write(MinLevel);
                message.Write(MaxLevel);
                message.Write(Vocations);
                message.Write(TeamSize);
                message.Write(FreeSlots);
                message.Write(StartTime);
                // TODO
                var count = Math.Min(UnknownList1.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(UnknownList1[i]);
                }
                count = Math.Min(Members.Count, ushort.MaxValue);
                message.Write((ushort)count);
                for (var i = 0; i < count; ++i)
                {
                    var (Id, Name, Level, Vocation, Status) = Members[i];
                    message.Write(Id);
                    message.Write(Name);
                    message.Write(Level);
                    message.Write(Vocation);
                    message.Write(Status);
                }
            }
        }
    }
}
