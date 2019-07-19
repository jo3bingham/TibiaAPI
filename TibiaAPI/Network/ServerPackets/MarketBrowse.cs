using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Market;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MarketBrowse : ServerPacket
    {
        public List<Offer> BuyOffers { get; } = new List<Offer>();
        public List<Offer> SellOffers { get; } = new List<Offer>();

        public ushort TypeId { get; set; }

        public MarketBrowse(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MarketBrowse;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
           TypeId = message.ReadUInt16();

           BuyOffers.Capacity = (int)message.ReadUInt32();
           for (uint i = 0; i < BuyOffers.Capacity; ++i)
           {
               BuyOffers.Add(message.ReadMarketOffer((int)MarketOfferType.Buy, TypeId));
           }

           SellOffers.Capacity = (int)message.ReadUInt32();
           for (uint i = 0; i < SellOffers.Capacity; ++i)
           {
               SellOffers.Add(message.ReadMarketOffer((int)MarketOfferType.Sell, TypeId));
           }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
           message.Write((byte)ServerPacketType.MarketBrowse);
           message.Write(TypeId);

           var count = Math.Min(BuyOffers.Count, uint.MaxValue);
           message.Write((uint)count);
           for (var i = 0; i < count; ++i)
           {
               message.Write(BuyOffers[i], TypeId);
           }

           count = Math.Min(SellOffers.Count, uint.MaxValue);
           message.Write((uint)count);
           for (var i = 0; i < count; ++i)
           {
               message.Write(SellOffers[i], TypeId);
           }
        }
    }
}
