using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Store;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class StoreOffers : ServerPacket
    {
        public List<Banner> Banners { get; } = new List<Banner>();
        public List<Offer> Offers { get; } = new List<Offer>();
        public List<string> Collections { get; } = new List<string>();

        public string CategoryName { get; set; }
        public string DisplaySubCategory { get; set; }

        public uint DisplayOfferId { get; set; }

        public byte BannerSwitchDelay { get; set; }
        public byte WindowType { get; set; }

        public bool TooManySearchResults { get; set; }

        public StoreOffers(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.StoreOffers;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CategoryName = message.ReadString();
            DisplayOfferId = message.ReadUInt32();
            WindowType = message.ReadByte();

            Collections.Capacity = message.ReadByte();
            for (var i = 0; i < Collections.Capacity; ++i)
            {
                Collections.Add(message.ReadString());
            }

            if (Client.VersionNumber >= 11900000)
            {
                DisplaySubCategory = message.ReadString();
            }

            Offers.Capacity = message.ReadUInt16();
            for (var i = 0; i < Offers.Capacity; ++i)
            {
                var offer = new Offer
                {
                    Name = message.ReadString(),
                    IsFeatured = WindowType == 3
                };

                offer.Details.Capacity = message.ReadByte();
                for (var j = 0; j < offer.Details.Capacity; ++j)
                {
                    var details = new OfferDetails
                    {
                        Id = message.ReadUInt32(),
                        Amount = message.ReadUInt16(),
                        Price = message.ReadUInt32()
                    };

                    if (Client.VersionNumber >= 11900000)
                    {
                        details.IsConfirmedPrice = message.ReadBool();
                    }

                    details.IsDisabled = message.ReadBool();
                    if (details.IsDisabled)
                    {
                        details.DisabledReasons.Capacity = message.ReadByte();
                        for (var x = 0; x < details.DisabledReasons.Capacity; ++x)
                        {
                            var disabledReason = message.ReadString();
                            details.DisabledReasons.Add(disabledReason);
                        }
                    }

                    details.HighlightState = message.ReadByte();
                    if (details.HighlightState == (int)StoreOfferHighlightState.Sale)
                    {
                        // This may actually be wrong, but since there aren't currently
                        // any items on sale I can't confirm it.
                        var saleValidUntilTimestamp = message.ReadUInt32();
                        var basePrice = message.ReadUInt32();
                    }

                    offer.Details.Add(details);
                }

                offer.DisplayType = message.ReadByte();
                if (offer.DisplayType == 0) // image (.png)
                {
                    offer.DisplayImage = message.ReadString();
                }
                else if (offer.DisplayType == 1) // mount id
                {
                    offer.DisplayMountId = message.ReadUInt16();
                }
                else if (offer.DisplayType == 2) // outfit
                {
                    offer.DisplayLooktype = message.ReadUInt16();
                    offer.DisplayColorHead = message.ReadByte();
                    offer.DisplayColorTorso = message.ReadByte();
                    offer.DisplayColorLegs = message.ReadByte();
                    offer.DisplayColorDetail = message.ReadByte();
                }
                else if (offer.DisplayType == 3) // item id
                {
                    offer.DisplayItemId = message.ReadUInt16();
                }
                else if (offer.DisplayType == 4) // male/female outfit
                {
                    offer.GenderToShow = message.ReadByte();
                    offer.DisplayFemaleLooktype = message.ReadUInt16();
                    offer.DisplayMaleLooktype = message.ReadUInt16();
                    offer.DisplayColorHead = message.ReadByte();
                    offer.DisplayColorTorso = message.ReadByte();
                    offer.DisplayColorLegs = message.ReadByte();
                    offer.DisplayColorDetail = message.ReadByte();
                }

                offer.TryType = message.ReadByte(); // 0 = disabled, 1 = mounts/outfits, 2 = hireling dresses
                offer.Collection = message.ReadString();
                offer.PopularityScore = message.ReadUInt16();
                offer.NewUntilTimestamp = message.ReadUInt32();
                offer.NeedsUserConfigurationBeforeBuying = message.ReadBool();

                offer.Products.Capacity = message.ReadUInt16();
                for (var j = 0; j < offer.Products.Capacity; ++j)
                {
                    var subOffer = new Offer
                    {
                        Name = message.ReadString(),
                        DisplayType = message.ReadByte()
                    };

                    if (subOffer.DisplayType == 0) // image (.png)
                    {
                        subOffer.DisplayImage = message.ReadString();
                    }
                    else if (subOffer.DisplayType == 1) // mount id
                    {
                        subOffer.DisplayMountId = message.ReadUInt16();
                    }
                    else if (subOffer.DisplayType == 2) // outfit
                    {
                        subOffer.DisplayLooktype = message.ReadUInt16();
                        subOffer.DisplayColorHead = message.ReadByte();
                        subOffer.DisplayColorTorso = message.ReadByte();
                        subOffer.DisplayColorLegs = message.ReadByte();
                        subOffer.DisplayColorDetail = message.ReadByte();
                    }
                    else if (subOffer.DisplayType == 3) // item sprite
                    {
                        subOffer.DisplayItemId = message.ReadUInt16();
                    }
                    else if (subOffer.DisplayType == 4) // male/female outfit
                    {
                        subOffer.GenderToShow = message.ReadByte();
                        subOffer.DisplayFemaleLooktype = message.ReadUInt16();
                        subOffer.DisplayMaleLooktype = message.ReadUInt16();
                        subOffer.DisplayColorHead = message.ReadByte();
                        subOffer.DisplayColorTorso = message.ReadByte();
                        subOffer.DisplayColorLegs = message.ReadByte();
                        subOffer.DisplayColorDetail = message.ReadByte();
                    }
                    offer.Products.Add(subOffer);
                }
                Offers.Add(offer);
            }

            if (CategoryName.Equals("Search", StringComparison.CurrentCultureIgnoreCase))
            {
                TooManySearchResults = message.ReadBool();
            }

            if (WindowType == 3) // Home
            {
                Banners.Capacity = message.ReadByte();
                for (var i = 0; i < Banners.Capacity; ++i)
                {
                    var banner = new Banner
                    {
                        Image = message.ReadString(),
                        Type = message.ReadByte()
                    };

                    if (banner.Type == 2)
                    {
                        banner.Category = message.ReadString();
                        banner.Collection = message.ReadString();
                    }
                    else if (banner.Type == 4)
                    {
                        banner.OfferId = message.ReadUInt32();
                    }

                    banner.Unknown = message.ReadUInt16(); // Always 0x0200 (512)?
                    Banners.Add(banner);
                }
                BannerSwitchDelay = message.ReadByte();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.StoreOffers);
            message.Write(CategoryName);
            message.Write(DisplayOfferId);
            message.Write(WindowType);

            var count = Math.Min(Collections.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Collections[i]);
            }

            if (Client.VersionNumber >= 11900000)
            {
                message.Write(DisplaySubCategory);
            }

            count = Math.Min(Offers.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var offer = Offers[i];
                message.Write(offer.Name);

                var detailCount = Math.Min(offer.Details.Count, byte.MaxValue);
                message.Write((byte)detailCount);
                for (var j = 0; j < detailCount; ++j)
                {
                    var details = offer.Details[j];
                    message.Write(details.Id);
                    message.Write(details.Amount);
                    message.Write(details.Price);
                    if (Client.VersionNumber >= 11900000)
                    {
                        message.Write(details.IsConfirmedPrice);
                    }
                    message.Write(details.IsDisabled);
                    if (details.IsDisabled)
                    {
                        var reasonCount = Math.Min(details.DisabledReasons.Count, byte.MaxValue);
                        message.Write((byte)reasonCount);
                        for (var x = 0; x < reasonCount; ++x)
                        {
                            message.Write(details.DisabledReasons[x]);
                        }
                    }
                    message.Write(details.HighlightState);
                }

                message.Write(offer.DisplayType);
                if (offer.DisplayType == 0) // image (.png)
                {
                    message.Write(offer.DisplayImage);
                }
                else if (offer.DisplayType == 1) // mount id
                {
                    message.Write(offer.DisplayMountId);
                }
                else if (offer.DisplayType == 2) // outfit
                {

                    message.Write(offer.DisplayLooktype);
                    message.Write(offer.DisplayColorHead);
                    message.Write(offer.DisplayColorTorso);
                    message.Write(offer.DisplayColorLegs);
                    message.Write(offer.DisplayColorDetail);
                }
                else if (offer.DisplayType == 3) // item id
                {
                    message.Write(offer.DisplayItemId);
                }
                else if (offer.DisplayType == 4) // male/female outfit
                {
                    message.Write(offer.GenderToShow);
                    message.Write(offer.DisplayFemaleLooktype);
                    message.Write(offer.DisplayMaleLooktype);
                    message.Write(offer.DisplayColorHead);
                    message.Write(offer.DisplayColorTorso);
                    message.Write(offer.DisplayColorLegs);
                    message.Write(offer.DisplayColorDetail);
                }

                message.Write(offer.TryType);
                message.Write(offer.Collection);
                message.Write(offer.PopularityScore);
                message.Write(offer.NewUntilTimestamp);
                message.Write(offer.NeedsUserConfigurationBeforeBuying);

                var productCount = Math.Min(offer.Products.Count, ushort.MaxValue);
                message.Write((ushort)productCount);
                for (var j = 0; j < productCount; ++j)
                {
                    var subOffer = offer.Products[j];
                    message.Write(subOffer.Name);
                    message.Write(subOffer.DisplayType);
                    if (subOffer.DisplayType == 0) // image (.png)
                    {
                        message.Write(subOffer.DisplayImage);
                    }
                    else if (subOffer.DisplayType == 1) // mount id
                    {
                        message.Write(subOffer.DisplayMountId);
                    }
                    else if (subOffer.DisplayType == 2) // outfit
                    {

                        message.Write(subOffer.DisplayLooktype);
                        message.Write(subOffer.DisplayColorHead);
                        message.Write(subOffer.DisplayColorTorso);
                        message.Write(subOffer.DisplayColorLegs);
                        message.Write(subOffer.DisplayColorDetail);
                    }
                    else if (subOffer.DisplayType == 3) // item id
                    {
                        message.Write(subOffer.DisplayItemId);
                    }
                    else if (subOffer.DisplayType == 4) // male/female outfit
                    {
                        message.Write(subOffer.GenderToShow);
                        message.Write(subOffer.DisplayFemaleLooktype);
                        message.Write(subOffer.DisplayMaleLooktype);
                        message.Write(subOffer.DisplayColorHead);
                        message.Write(subOffer.DisplayColorTorso);
                        message.Write(subOffer.DisplayColorLegs);
                        message.Write(subOffer.DisplayColorDetail);
                    }
                }
            }

            if (CategoryName.Equals("Search", StringComparison.CurrentCultureIgnoreCase))
            {
                message.Write(TooManySearchResults);
            }

            if (WindowType == 3) // Home
            {
                count = Math.Min(Banners.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    var banner = Banners[i];
                    message.Write(banner.Image);
                    message.Write(banner.Type);
                    if (banner.Type == 2)
                    {
                        message.Write(banner.Category);
                        message.Write(banner.Collection);
                    }
                    else if (banner.Type == 4)
                    {
                        message.Write(banner.OfferId);
                    }
                    message.Write(banner.Unknown);
                }
                message.Write(BannerSwitchDelay);
            }
        }
    }
}
