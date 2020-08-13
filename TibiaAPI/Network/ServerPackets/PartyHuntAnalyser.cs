using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PartyHuntAnalyser : ServerPacket
    {
        public byte UnknownByte1 { get; set; }

        public List<(uint PlayerId, string Name)> Members { get; } =
            new List<(uint PlayerId, string Name)>();
        public List<(uint PlayerId, byte unknown, ulong Loot, ulong Supplies, ulong Damage, ulong Health)> MemberInfo { get; } =
            new List<(uint PlayerId, byte unknown, ulong Loot, ulong Supplies, ulong Damage, ulong Health)>();

        public uint LeaderId { get; set; }
        public uint SessionMinutes { get; set; }

        public bool HasNames { get; set; }

        public PartyHuntAnalyser(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PartyHuntAnalyser;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            SessionMinutes = message.ReadUInt32();
            LeaderId = message.ReadUInt32(); // This is a player ID, but it may not be the leader.
            // TODO
            UnknownByte1 = message.ReadByte();
            MemberInfo.Capacity = message.ReadByte();
            for (var i = 0; i < MemberInfo.Capacity; ++i)
            {
                var playerId = message.ReadUInt32();
                var unknown = message.ReadByte();
                var loot = message.ReadUInt64();
                var supplies = message.ReadUInt64();
                var damage = message.ReadUInt64();
                var health = message.ReadUInt64();
                MemberInfo.Add((playerId, unknown, loot, supplies, damage, health));
            }
            // I'm not sure the purpose of this? Not because it's not always present,
            // but because players who have already been sent previously can appear again.
            HasNames = message.ReadBool();
            if (HasNames)
            {
                Members.Capacity = message.ReadByte();
                for (var i = 0; i < Members.Capacity; ++i)
                {
                    var playerId = message.ReadUInt32();
                    var name = message.ReadString();
                    Members.Add((playerId, name));
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.Write((byte)ServerPacketType.PartyHuntAnalyser);
            message.Write(SessionMinutes);
            message.Write(LeaderId);
            message.Write(UnknownByte1);
            var count = Math.Min(byte.MaxValue, MemberInfo.Count);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (PlayerId, Unknown, Loot, Supplies, Damage, Health) = MemberInfo[i];
                message.Write(PlayerId);
                message.Write(Unknown);
                message.Write(Loot);
                message.Write(Supplies);
                message.Write(Damage);
                message.Write(Health);
            }
            message.Write(HasNames);
            if (HasNames)
            {
                count = Math.Min(byte.MaxValue, Members.Count);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    var (PlayerId, Name) = Members[i];
                    message.Write(PlayerId);
                    message.Write(Name);
                }
            }
        }
    }
}
