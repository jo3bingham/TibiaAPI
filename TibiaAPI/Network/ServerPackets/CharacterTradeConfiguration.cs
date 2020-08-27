using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CharacterTradeConfiguration : ServerPacket
    {
        public List<(ushort Id, uint Count)> Items { get; } =
            new List<(ushort Id, uint Count)>();

        public List<(string Requirement, bool IsSatisfied)> Requirements { get; } =
            new List<(string Requirement, bool IsSatisfied)>();

        public List<(byte Id, string Title, List<(ushort Id, string Description)> Entries)> SalesArguments { get; } =
            new List<(byte Id, string Title, List<(ushort Id, string Description)> Entries)>();

        public string CharacterName { get; set; }

        public uint AuctionEnd { get; set; }
        public uint AuctionStart { get; set; }

        public ushort AuctionFee { get; set; }
        public ushort Level { get; set; }
        public ushort StartingBid { get; set; }

        public byte Sex { get; set; }
        public byte Type { get; set; }
        public byte VocationId { get; set; }

        public byte Unknown1 { get; set; }

        public CharacterTradeConfiguration(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CharacterTradeConfiguration;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            Type = message.ReadByte();
            if (Type == 0)
            {
                CharacterName = message.ReadString();
                Level = message.ReadUInt16();
                VocationId = message.ReadByte();
                Sex = message.ReadByte();
                AuctionStart = message.ReadUInt32();
                AuctionEnd = message.ReadUInt32();
                StartingBid = message.ReadUInt16();
                Requirements.Capacity = message.ReadByte();
                for (var i = 0; i < Requirements.Capacity; i++)
                {
                    var requirement = message.ReadString();
                    var isSatisfied = message.ReadBool();
                    Requirements.Add((requirement, isSatisfied));
                }
            }
            else if (Type == 1 || Type == 2) // 1 = inventory/depot items, 2 = store inbox items
            {
                Items.Capacity = message.ReadUInt16();
                for (var i = 0; i < Items.Capacity; i++)
                {
                    var id = message.ReadUInt16();
                    var count = message.ReadUInt32();
                    Items.Add((id, count));
                }
            }
            else if (Type == 3)
            {
                SalesArguments.Capacity = message.ReadByte();
                for (var i = 0; i < SalesArguments.Capacity; i++)
                {
                    var id = message.ReadByte();
                    var title = message.ReadString();
                    var entries = new List<(ushort, string)>(message.ReadUInt16());
                    for (var j = 0; j < entries.Capacity; j++)
                    {
                        var index = message.ReadUInt16();
                        var description = message.ReadString();
                        entries.Add((index, description));
                    }
                    SalesArguments.Add((id, title, entries));
                }
            }
            else if (Type == 4)
            {
                AuctionFee = message.ReadUInt16();
                // Changing this to 1 crashed the client, so it could be a boolean,
                // or some other identifier, and the client expects more data if not 0.
                Unknown1 = message.ReadByte(); // 00
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CharacterTradeConfiguration);
            message.Write(Type);
            if (Type == 0)
            {
                message.Write(CharacterName);
                message.Write(Level);
                message.Write(VocationId);
                message.Write(Sex);
                message.Write(AuctionStart);
                message.Write(AuctionEnd);
                message.Write(StartingBid);
                var count = Math.Min(Requirements.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; i++)
                {
                    var (Requirement, IsSatisfied) = Requirements[i];
                    message.Write(Requirement);
                    message.Write(IsSatisfied);
                }
            }
            else if (Type == 1 || Type == 2) // 1 = inventory/depot items, 2 = store inbox items
            {
                var count = Math.Min(Items.Count, ushort.MaxValue);
                message.Write((ushort)count);
                for (var i = 0; i < count; i++)
                {
                    var (Id, Count) = Items[i];
                    message.Write(Id);
                    message.Write(Count);
                }
            }
            else if (Type == 3)
            {
                var count = Math.Min(SalesArguments.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; i++)
                {
                    var (Id, Title, Entries) = SalesArguments[i];
                    message.Write(Id);
                    message.Write(Title);
                    var subCount = Math.Min(Entries.Count, ushort.MaxValue);
                    message.Write((ushort)subCount);
                    for (var j = 0; j < subCount; j++)
                    {
                        var (Index, Description) = Entries[j];
                        message.Write(Index);
                        message.Write(Description);
                    }
                }
            }
            else if (Type == 4)
            {
                message.Write(AuctionFee);
                // Changing this to 1 crashed the client, so it could be a boolean,
                // or some other identifier, and the client expects more data if not 0.
                message.Write(Unknown1); // 00
            }
        }
    }
}
