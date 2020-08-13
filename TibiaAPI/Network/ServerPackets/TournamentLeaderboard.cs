using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TournamentLeaderboard : ServerPacket
    {
        public ushort UnknownUShort1 { get; set; }
        public byte UnknownByte1 { get; set; }

        public List<(uint CurrentRank, uint PreviousRank, string Name, byte Vocation, ulong Points, byte RankChangeDirection, bool IsRankChangeHighlighted, bool IsNameHighlighted)> Characters { get; } =
            new List<(uint CurrentRank, uint PreviousRank, string Name, byte Vocation, ulong Points, byte RankChangeDirection, bool IsRankChangeHighlighted, bool IsNameHighlighted)>();

        public List<string> Worlds { get; } = new List<string>();

        public string Rewards { get; set; }
        public string SelectedWorld { get; set; }

        public ushort CurrentPage { get; set; }
        public ushort NumberOfPages { get; set; }
        public ushort RefreshRate { get; set; }

        public byte EntriesPerPage { get; set; }

        public TournamentLeaderboard(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TournamentLeaderboard;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            UnknownUShort1 = message.ReadUInt16();
            Worlds.Capacity = message.ReadByte();
            for (var i = 0; i < Worlds.Capacity; ++i)
            {
                Worlds.Add(message.ReadString());
            }
            SelectedWorld = message.ReadString();
            RefreshRate = message.ReadUInt16();
            CurrentPage = message.ReadUInt16();
            NumberOfPages = message.ReadUInt16();
            Characters.Capacity = EntriesPerPage = message.ReadByte();
            for (var i = 0; i < EntriesPerPage; ++i)
            {
                var currentRank = message.ReadUInt32();
                var previousRank = message.ReadUInt32();
                var name = message.ReadString();
                var vocation = message.ReadByte();
                var points = message.ReadUInt64();
                var rankChangeDirection = message.ReadByte();
                var isRankChangeHighlighted = message.ReadBool();
                var isNameHighlighted = message.ReadBool();
                Characters.Add((currentRank, previousRank, name, vocation, points, rankChangeDirection, isRankChangeHighlighted, isNameHighlighted));
            }
            // TODO
            UnknownByte1 = message.ReadByte();
            Rewards = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.Write((byte)ServerPacketType.TournamentLeaderboard);
            message.Write(UnknownUShort1);
            var count = Math.Min(Worlds.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Worlds[i]);
            }
            message.Write(SelectedWorld);
            message.Write(RefreshRate);
            message.Write(CurrentPage);
            message.Write(NumberOfPages);
            count = Math.Min(EntriesPerPage, Math.Min(Characters.Count, byte.MaxValue));
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Characters[i].CurrentRank);
                message.Write(Characters[i].PreviousRank);
                message.Write(Characters[i].Name);
                message.Write(Characters[i].Vocation);
                message.Write(Characters[i].Points);
                message.Write(Characters[i].RankChangeDirection);
                message.Write(Characters[i].IsRankChangeHighlighted);
                message.Write(Characters[i].IsNameHighlighted);
            }
            message.Write(UnknownByte1);
            message.Write(Rewards);
        }
    }
}
