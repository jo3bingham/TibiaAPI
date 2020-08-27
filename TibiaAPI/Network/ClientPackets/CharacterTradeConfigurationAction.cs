using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CharacterTradeConfigurationAction : ClientPacket
    {
        public List<ushort> DisplayItemIds { get; } = new List<ushort>();
        public List<ushort> SelectedSalesArgumentIds { get; } = new List<ushort>();
        public List<ushort> SelectedStoreItemIds { get; } = new List<ushort>();

        public uint AuctionEnd { get; set; }
        public uint StartingBid { get; set; }

        public byte Type { get; set; }
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }
        public byte Unknown3 { get; set; }

        public CharacterTradeConfigurationAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.CharacterTradeConfigurationAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            if (Type == 2) // Review
            {
                StartingBid = message.ReadUInt32();
                AuctionEnd = message.ReadUInt32();
                DisplayItemIds.Capacity = message.ReadByte();
                for (var i = 0; i < DisplayItemIds.Capacity; i++)
                {
                    DisplayItemIds.Add(message.ReadUInt16());
                }
                SelectedStoreItemIds.Capacity = message.ReadByte();
                for (var i = 0; i < SelectedStoreItemIds.Capacity; i++)
                {
                    SelectedStoreItemIds.Add(message.ReadUInt16());
                }
                SelectedSalesArgumentIds.Capacity = message.ReadByte();
                for (var i = 0; i < SelectedSalesArgumentIds.Capacity; i++)
                {
                    SelectedSalesArgumentIds.Add(message.ReadUInt16());
                }
            }
            else if (Type == 3) // Confirm
            {
                StartingBid = message.ReadUInt32();
                AuctionEnd = message.ReadUInt32();
                Unknown1 = message.ReadByte(); // 00, 01
                Unknown2 = message.ReadByte(); // 00, 1B
                Unknown3 = message.ReadByte(); // 00, 01
                if (Unknown3 == 1)
                {
                    message.ReadBytes(4); // 00 01 0C 00
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CharacterTradeConfigurationAction);
            message.Write(Type);
            if (Type == 2) // Review
            {
                message.Write(StartingBid);
                message.Write(AuctionEnd);
                var count = Math.Min(DisplayItemIds.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; i++)
                {
                    message.Write(DisplayItemIds[i]);
                }
                count = Math.Min(SelectedStoreItemIds.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; i++)
                {
                    message.Write(SelectedStoreItemIds[i]);
                }
                count = Math.Min(SelectedSalesArgumentIds.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; i++)
                {
                    message.Write(SelectedSalesArgumentIds[i]);
                }
            }
            else if (Type == 3) // Confirm
            {
                message.Write(StartingBid);
                message.Write(AuctionEnd);
                message.Write(Unknown1);
                message.Write(Unknown2);
                message.Write(Unknown3);
            }
        }
    }
}
