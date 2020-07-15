using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TeamFinderAssembleTeam : ClientPacket
    {
        public uint StartTime { get; set; }

        public ushort FreeSlots { get; set; }
        public ushort MaxLevel { get; set; }
        public ushort MinLevel { get; set; }
        public ushort TeamSize { get; set; }

        public byte Vocations { get; set; } // Bit Flag
        public byte Type { get; set; }

        public TeamFinderAssembleTeam(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TeamFinderAssembleTeam;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            Type = message.ReadByte();
            if (Type == 0)
            {
            }
            else if (Type == 3)
            {
                MinLevel = message.ReadUInt16();
                MaxLevel = message.ReadUInt16();
                Vocations = message.ReadByte();
                TeamSize = message.ReadUInt16();
                FreeSlots = message.ReadUInt16();
                message.ReadByte(); // 00
                StartTime = message.ReadUInt32();
                var count = message.ReadByte();
                for (var i = 0; i < count; i++)
                {
                    var _ = message.ReadUInt16(); // 19 00 47 00; 19 00 4B 00; 00 00 00 00
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.TeamFinderAssembleTeam);
        }
    }
}
