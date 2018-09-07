using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MarketBrowse : ServerPacket
    {
        //public List<Offer> BuyOffers { get; set; }
        //public List<Offer> SellOffers { get; set; }
        public ushort TypeId { get; set; }

        public MarketBrowse()
        {
            PacketType = ServerPacketType.MarketBrowse;
        }

        //public override bool ParseFromNetworkMessage(NetworkMessage message)
        //{
        //    if (message.ReadByte() != (byte)ServerPacketType.MarketBrowse)
        //    {
        //        return false;
        //    }

        //    TypeId = message.ReadUInt16();

        //    var count = message.ReadUInt32();
        //    BuyOffers = new List<Offer>((int)Math.Min(count, int.MaxValue));
        //    for (uint i = 0; i < count; ++i)
        //    {
        //        BuyOffers.Add(message.ReadMarketOffer((int)MarketOfferType.Buy, TypeId));
        //    }

        //    count = message.ReadUInt32();
        //    SellOffers = new List<Offer>((int)Math.Min(count, int.MaxValue));
        //    for (uint i = 0; i < count; ++i)
        //    {
        //        SellOffers.Add(message.ReadMarketOffer((int)MarketOfferType.Sell, TypeId));
        //    }
        //    return true;
        //}

        //public override void AppendToNetworkMessage(NetworkMessage message)
        //{
        //    message.Write((byte)ServerPacketType.MarketBrowse);
        //    message.Write(TypeId);
        //    var count = (uint)Math.Min(BuyOffers.Count, uint.MaxValue);
        //    message.Write(count);
        //    for (var i = 0; i < count; ++i)
        //    {
        //        message.WriteMarketOffer(BuyOffers[i]);
        //    }
        //    count = (uint)Math.Min(SellOffers.Count, uint.MaxValue);
        //    message.Write(count);
        //    for (var i = 0; i < count; ++i)
        //    {
        //        message.WriteMarketOffer(SellOffers[i]);
        //    }
        //}
    }
}
