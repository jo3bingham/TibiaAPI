using System;
using System.Collections.Generic;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Outfit : ServerPacket
    {
        public List<(ushort Id, string Name, byte Addons, bool EnableStoreLink, uint StoreOfferId)> Outfits { get; } =
            new List<(ushort Id, string Name, byte Addons, bool EnableStoreLink, uint StoreOfferId)>();
        public List<(ushort Id, string Name, bool EnableStoreLink, uint StoreOfferId)> Mounts { get; } =
            new List<(ushort Id, string Name, bool EnableStoreLink, uint StoreOfferId)>();

        public OutfitWindowType WindowType { get; set; }

        public ushort MountId { get; set; }
        public ushort OutfitId { get; set; }

        public byte Addons { get; set; }
        public byte DetailColor { get; set; }
        public byte HeadColor { get; set; }
        public byte LegsColor { get; set; }
        public byte TorsoColor { get; set; }

        public Outfit(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Outfit;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Outfit)
            {
                return false;
            }

            OutfitId = message.ReadUInt16();
            HeadColor = message.ReadByte();
            TorsoColor = message.ReadByte();
            LegsColor = message.ReadByte();
            DetailColor = message.ReadByte();
            Addons = message.ReadByte();
            MountId = message.ReadUInt16();

            Outfits.Capacity = Client.VersionNumber >= 11750000 ? message.ReadUInt16() : message.ReadByte();
            for (var i = 0; i < Outfits.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var name = message.ReadString();
                var addons = message.ReadByte();
                var enableStoreLink = Client.VersionNumber >= 11750000 ? message.ReadBool() : false;
                uint storeOfferId = 0;
                if (enableStoreLink)
                {
                    storeOfferId = message.ReadUInt32();
                }
                Outfits.Add((id, name, addons, enableStoreLink, storeOfferId));
            }

            Mounts.Capacity = Client.VersionNumber >= 11750000 ? message.ReadUInt16() : message.ReadByte();
            for (var i = 0; i < Mounts.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var name = message.ReadString();
                var enableStoreLink = Client.VersionNumber >= 11750000 ? message.ReadBool(): false;
                uint storeOfferId = 0;
                if (enableStoreLink)
                {
                    storeOfferId = message.ReadUInt32();
                }
                Mounts.Add((id, name, enableStoreLink, storeOfferId));
            }

            WindowType = Client.VersionNumber >= 11750000 ? (OutfitWindowType)message.ReadUInt16() : OutfitWindowType.SelectOutfit;
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Outfit);
            message.Write(OutfitId);
            message.Write(HeadColor);
            message.Write(TorsoColor);
            message.Write(LegsColor);
            message.Write(DetailColor);
            message.Write(Addons);
            message.Write(MountId);

            var count = 0;
            if (Client.VersionNumber >= 11750000)
            {
                count = Math.Min(Outfits.Count, ushort.MaxValue);
                message.Write((ushort)count);
            }
            else
            {
                count = Math.Min(Outfits.Count, byte.MaxValue);
                message.Write((byte)count);
            }

            for (var i = 0; i < count; ++i)
            {
                var outfit = Outfits[i];
                message.Write(outfit.Id);
                message.Write(outfit.Name);
                message.Write(outfit.Addons);
                if (Client.VersionNumber >= 11750000)
                {
                    message.Write(outfit.EnableStoreLink);
                    if (outfit.EnableStoreLink)
                    {
                        message.Write(outfit.StoreOfferId);
                    }
                }
            }

            if (Client.VersionNumber >= 11750000)
            {
                count = Math.Min(Mounts.Count, ushort.MaxValue);
                message.Write((ushort)count);
            }
            else
            {
                count = Math.Min(Mounts.Count, byte.MaxValue);
                message.Write((byte)count);
            }

            for (var i = 0; i < count; ++i)
            {
                var (Id, Name, EnableStoreLink, StoreOfferId) = Mounts[i];
                message.Write(Id);
                message.Write(Name);
                if (Client.VersionNumber >= 11750000)
                {
                    message.Write(EnableStoreLink);
                    if (EnableStoreLink)
                    {
                        message.Write(StoreOfferId);
                    }
                }
            }

            if (Client.VersionNumber >= 11750000)
            {
                message.Write((ushort)WindowType);
            }
        }
    }
}
