﻿using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DepotSearchDetailList : ServerPacket
    {
        public List<(ushort ItemId, byte Amount)> DepotDisplayItems { get; } = new List<(ushort ItemId, byte Amount)>();
        public List<(ushort ItemId, byte Amount)> InboxDisplayItems { get; } = new List<(ushort ItemId, byte Amount)>();

        public (ushort ItemId, uint Amount) SupplyStashItem { get; set; } = (0, 0);

        public uint DepotItemCount { get; set; }
        public uint InboxItemCount { get; set; }

        public ushort ItemId { get; set; }

        public bool ContainsSupplyStashItem { get; set; }

        public DepotSearchDetailList(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.DepotSearchDetailList;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ItemId = message.ReadUInt16();
            DepotItemCount = message.ReadUInt32();
            DepotDisplayItems.Capacity = message.ReadByte();
            for (var i = 0; i < DepotDisplayItems.Capacity; ++i)
            {
                var itemId = message.ReadUInt16();
                var amount = message.ReadByte();
                DepotDisplayItems.Add((itemId, amount));
            }
            InboxItemCount = message.ReadUInt32();
            InboxDisplayItems.Capacity = message.ReadByte();
            for (var i = 0; i < InboxDisplayItems.Capacity; ++i)
            {
                var itemId = message.ReadUInt16();
                var amount = message.ReadByte();
                InboxDisplayItems.Add((itemId, amount));
            }
            ContainsSupplyStashItem = message.ReadBool();
            if (ContainsSupplyStashItem)
            {
                var itemId = message.ReadUInt16();
                var amount = message.ReadUInt32();
                SupplyStashItem = (itemId, amount);
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DepotSearchDetailList);
            message.Write(DepotItemCount);
            var count = Math.Min(DepotDisplayItems.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (itemId, amount) = DepotDisplayItems[i];
                message.Write(itemId);
                message.Write(amount);
            }
            message.Write(InboxItemCount);
            count = Math.Min(InboxDisplayItems.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (itemId, amount) = InboxDisplayItems[i];
                message.Write(itemId);
                message.Write(amount);
            }
            message.Write(ContainsSupplyStashItem);
            if (ContainsSupplyStashItem)
            {
                message.Write(SupplyStashItem.ItemId);
                message.Write(SupplyStashItem.Amount);
            }
        }
    }
}
