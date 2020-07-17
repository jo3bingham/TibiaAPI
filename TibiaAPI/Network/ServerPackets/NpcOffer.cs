using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class NpcOffer : ServerPacket
    {
        public List<(ushort Id, byte Data, string Name, uint Weight, uint BuyPrice, uint SellPrice)> Offers { get; } =
            new List<(ushort Id, byte Data, string Name, uint Weight, uint BuyPrice, uint SellPrice)>();

        public string CurrencyName { get; set; }
        public string NpcName { get; set; }

        public ushort CurrencyItemId { get; set; }

        public NpcOffer(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.NpcOffer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            NpcName = message.ReadString();
            if (Client.VersionNumber >= 12087995)
            {
                CurrencyItemId = message.ReadUInt16();
            }
            if (Client.VersionNumber >= 12409997)
            {
                // If this is set, it will override `CurrencyItemId`.
                CurrencyName = message.ReadString();
            }
            Offers.Capacity = message.ReadUInt16();
            for (var i = 0; i < Offers.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var data = message.ReadByte();
                var name = message.ReadString();
                var weight = message.ReadUInt32();
                var buyPrice = message.ReadUInt32();
                var sellPrice = message.ReadUInt32();
                Offers.Add((id, data, name, weight, buyPrice, sellPrice));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.NpcOffer);
            message.Write(NpcName);
            if (Client.VersionNumber >= 12087995)
            {
                message.Write(CurrencyItemId);
            }
            if (Client.VersionNumber >= 12409997)
            {
                message.Write(CurrencyName);
            }
            var count = Math.Min(Offers.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, Data, Name, Weight, BuyPrice, SellPrice) = Offers[i];
                message.Write(Id);
                message.Write(Data);
                message.Write(Name);
                message.Write(Weight);
                message.Write(BuyPrice);
                message.Write(SellPrice);
            }
        }
    }
}
