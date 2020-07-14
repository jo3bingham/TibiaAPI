using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Highscores : ServerPacket
    {
        public List<(byte Id, string Category)> Categories { get; } = new List<(byte Id, string Category)>();
        public List<(uint Rank, string Character, byte Unknown1, byte Unknown2, byte VocationId, string World, ushort Level, byte Unknown3, ulong Points)> Entries { get; } =
            new List<(uint Rank, string Character, byte Unknown1, byte Unknown2, byte VocationId, string World, ushort Level, byte Unknown3, ulong Points)>();
        public List<string> GameWorlds { get; } = new List<string>();
        public List<(uint Id, string Vocation)> VocationOptions { get; } = new List<(uint Id, string Text)>();

        public string SelectedWorld { get; set; }

        public uint LastUpdateTimestamp { get; set; }
        public uint SelectedVocation { get; set; }

        public byte NumberOfPages { get; set; }
        public byte SelectedCategory { get; set; }

        public Highscores(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Highscores;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.ReadByte(); // 00
            GameWorlds.Capacity = message.ReadByte();
            for (var i = 0; i < GameWorlds.Capacity; i++)
            {
                GameWorlds.Add(message.ReadString());
            }
            SelectedWorld = message.ReadString();
            VocationOptions.Capacity = message.ReadByte();
            for (var i = 0; i < VocationOptions.Capacity; i++)
            {
                var id = message.ReadUInt32();
                var vocation = message.ReadString();
                VocationOptions.Add((id, vocation));
            }
            SelectedVocation = message.ReadUInt32();
            Categories.Capacity = message.ReadByte();
            for (var i = 0; i < Categories.Capacity; i++)
            {
                var id = message.ReadByte();
                var category = message.ReadString();
                Categories.Add((id, category));
            }
            SelectedCategory = message.ReadByte();
            message.ReadBytes(2); // 01 00
            NumberOfPages = message.ReadByte();
            message.ReadByte(); // 00 (current page?)
            Entries.Capacity = message.ReadByte();
            for (var i = 0; i < Entries.Capacity; i++)
            {
                var rank = message.ReadUInt32();
                var character = message.ReadString();
                var unknown1 = message.ReadByte(); // 00
                var unknown2 = message.ReadByte(); // 00
                var vocationId = message.ReadByte();
                var world = message.ReadString();
                var level = message.ReadUInt16();
                var unknown3 = message.ReadByte(); // 00
                var points = message.ReadUInt64();
                Entries.Add((rank, character, unknown1, unknown2, vocationId, world, level, unknown3, points));
            }
            message.ReadBytes(3); // FF 00 01
            LastUpdateTimestamp = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.Highscores);
        }
    }
}
