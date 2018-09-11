using System;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Market
{
    public class Offer
    {
        public OfferId OfferId { get; set; }

        public MarketOfferTerminationReason TerminationReason { get; set; }

        public string Character { get; set; }

        public uint PiecePrice { get; set; }
        public uint TerminationTimestamp { get; set; }
        public uint TotalPrice { get; set; }

        public int Kind { get; set; }

        public ushort Amount { get; set; }
        public ushort TypeId { get; set; }

        public Offer(OfferId offerId, int kind, ushort typeId, ushort amount, uint piecePrice, string character, MarketOfferTerminationReason terminationReason)
        {
            OfferId = offerId ?? throw new ArgumentNullException(nameof(offerId));

            if (kind != (int)MarketOfferType.Buy && kind != (int)MarketOfferType.Sell)
            {
                throw new ArgumentException($"[Offer] Invalid kind: {kind}");
            }

            Kind = kind;
            TypeId = typeId;
            Amount = amount;
            PiecePrice = piecePrice;
            Character = character;
            TerminationReason = terminationReason;
        }
    }
}
