using System;
using System.Collections.Generic;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Outfit : ServerPacket
    {
        public List<(ushort FemaleLooktype, ushort MaleLooktype)> HirelingDresses { get; } =
            new List<(ushort FemaleLooktype, ushort MaleLooktype)>();
        public List<(ushort Id, string Name, byte Addons, bool EnableStoreLink, uint StoreOfferId)> Outfits { get; } =
            new List<(ushort Id, string Name, byte Addons, bool EnableStoreLink, uint StoreOfferId)>();
        public List<(ushort Id, string Name, bool EnableStoreLink, uint StoreOfferId)> Mounts { get; } =
            new List<(ushort Id, string Name, bool EnableStoreLink, uint StoreOfferId)>();

        public ushort MountId { get; set; }
        public ushort OutfitId { get; set; }
        public ushort Type { get; set; }

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

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
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

            Type = message.ReadUInt16();
            if (Type == 4) // Hireling Dresses
            {
                HirelingDresses.Capacity = message.ReadUInt16();
                for (var i = 0; i < HirelingDresses.Capacity; ++i)
                {
                    var femaleLooktype = message.ReadUInt16();
                    var maleLooktype = message.ReadUInt16();
                    HirelingDresses.Add((femaleLooktype, maleLooktype));
                }
            }
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
                message.Write(Type);
                if (Type == 4) // Hireling Dresses
                {
                    count = Math.Min(HirelingDresses.Count, ushort.MaxValue);
                    message.Write((ushort)count);
                    for (var i = 0; i < count; ++i)
                    {
                        var (FemaleLooktype, MaleLooktype) = HirelingDresses[i];
                        message.Write(FemaleLooktype);
                        message.Write(MaleLooktype);
                    }
                }
            }
        }
    }
}
