using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TeamFinderTeamLeader : ServerPacket
    {
        public List<(uint Id, string Name, ushort Level, byte Vocation, byte Status)> Members { get; } =
            new List<(uint Id, string Name, ushort Level, byte Vocation, byte Status)>();

        public uint StartTime { get; set; }

        public ushort FreeSlots { get; set; }
        public ushort MaxLevel { get; set; }
        public ushort MinLevel { get; set; }
        public ushort TeamSize { get; set; }

        public byte Vocations { get; set; } // Bit Flag

        public TeamFinderTeamLeader(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TeamFinderTeamLeader;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            var unknown = message.ReadByte(); // Possibly a boolean flag; IsUpdated?
            if (unknown == 1)
            {
            }
            else if (unknown == 0)
            {
                MinLevel = message.ReadUInt16();
                MaxLevel = message.ReadUInt16();
                Vocations = message.ReadByte();
                TeamSize = message.ReadUInt16();
                FreeSlots = message.ReadUInt16();
                StartTime = message.ReadUInt32();
                var count = message.ReadByte();
                for (var i = 0; i < count; i++)
                {
                    var _ = message.ReadUInt16(); // 19 00; 47 00
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
            // TODO
            // message.Write((byte)ServerPacketType.TeamFinderTeamLeader);
        }
    }
}
