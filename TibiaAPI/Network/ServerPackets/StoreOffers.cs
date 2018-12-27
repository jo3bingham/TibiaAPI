using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class StoreOffers : ServerPacket
    {
        public StoreOffers(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.StoreOffers;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.StoreOffers)
            {
                return false;
            }

            // TODO

            var categoryName = message.ReadString();

            message.ReadBytes(4); // unknown, always 0x00000000?

            var type = message.ReadByte();

            var numberOfSubCategories = message.ReadByte();
            for (var i = 0; i < numberOfSubCategories; ++i)
            {
                var subCategoryName = message.ReadString();
            }

            if (Client.VersionNumber >= 11900000)
            {
                message.ReadUInt16();
            }

            var numberOfOffers = message.ReadUInt16();
            for (var i = 0; i < numberOfOffers; ++i)
            {
                var offerName = message.ReadString();
                var numberOfPurchaseOptions = message.ReadByte();
                for (var j = 0; j < numberOfPurchaseOptions; ++j)
                {
                    var offerId = message.ReadUInt32();
                    var offerCount = message.ReadUInt16();
                    var offerPrice = message.ReadUInt32();
                    if (Client.VersionNumber >= 11900000)
                    {
                        message.ReadByte();
                    }
                    var isDisabled = message.ReadBool();
                    if (isDisabled)
                    {
                        var disabledState = message.ReadByte();
                        if (disabledState == (int)StoreOfferDisabledState.Disabled)
                        {
                            var disabledReason = message.ReadString();
                        }
                    }
                    // This may actually be wrong, but since there aren't currently
                    // any items on sale I can't confirm it.
                    var highlightState = message.ReadByte();
                    if (highlightState == (int)StoreOfferHighlightState.Sale)
                    {
                        var saleValidUntilTimestamp = message.ReadUInt32();
                        var basePrice = message.ReadUInt32();
                    }
                }
                var iconType = message.ReadByte();
                if (iconType == 0) // use web image (.png)
                {
                    var image = message.ReadString();
                }
                else if (iconType == 2)
                {
                    message.ReadBytes(6); // unknown
                }
                else if (iconType == 3 || iconType == 1) // use item sprite
                {
                    var itemId = message.ReadUInt16();
                }
                var parentCategory = message.ReadString();
                message.ReadBytes(7); // unknown
                var numberOfSubProducts = message.ReadUInt16();
                for (var j = 0; j < numberOfSubProducts; ++j)
                {
                    var subProductName = message.ReadString();
                    iconType = message.ReadByte();
                    if (iconType == 0) // use web image (.png)
                    {
                        var image = message.ReadString();
                    }
                    else if (iconType == 2)
                    {
                        message.ReadBytes(6); // unknown
                    }
                    else if (iconType == 3 || iconType == 1) // use item sprite
                    {
                        var itemId = message.ReadUInt16();
                    }
                }
            }

            if (type == 3) // Home
            {
                var numberOfBannerImages = message.ReadByte();
                for (var i = 0; i < numberOfBannerImages; ++i)
                {
                    var bannerImage = message.ReadString();
                    var bannerType = message.ReadByte();
                    if (bannerType == 2)
                    {
                        message.ReadString(); // category name?
                    }
                    else if (bannerType == 4)
                    {
                        message.ReadUInt32(); // category id?
                    }
                    message.ReadBytes(2); // unknown
                }
                message.ReadByte(); // unknown
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.StoreOffers);
            // TODO
        }
    }
}
