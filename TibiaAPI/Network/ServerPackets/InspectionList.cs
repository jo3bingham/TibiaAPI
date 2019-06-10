using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class InspectionList : ServerPacket
    {
        public List<(string Name, byte Slot, ObjectInstance Item, List<ushort> ImbuementIds, List<(string Name, string Description)> Details)> Items { get; } =
            new List<(string Name, byte Slot, ObjectInstance Item, List<ushort> ImbuementIds, List<(string Name, string Description)> Details)>();
        public List<(string Name, string Description)> PlayerDetails { get; } = new List<(string Name, string Description)>();

        public AppearanceInstance PlayerOutfit { get; set; }

        public string PlayerName { get; set; }

        public bool IsPlayer { get; set; }

        public InspectionList(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.InspectionList;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            IsPlayer = message.ReadBool();

            Items.Capacity = message.ReadByte();
            for (var i = 0; i < Items.Capacity; ++i)
            {
                var itemName = message.ReadString();
                var slotId = IsPlayer ? message.ReadByte() : (byte)0;
                var item = message.ReadObjectInstance();

                var imbuementIds = new List<ushort>(message.ReadByte());
                for (var x = 0; x < imbuementIds.Capacity; ++x)
                {
                   imbuementIds.Add(message.ReadUInt16());
                }

                var details = new List<(string, string)>(message.ReadByte());
                for (var n = 0; n < details.Capacity; ++n)
                {
                    var name = message.ReadString();
                    var description = message.ReadString();
                    details.Add((name, description));
                }

                Items.Add((itemName, slotId, item, imbuementIds, details));
            }

            if (IsPlayer)
            {
                PlayerName = message.ReadString();
                PlayerOutfit = message.ReadCreatureOutfit();

                PlayerDetails.Capacity = message.ReadByte();
                for (var n = 0; n < PlayerDetails.Capacity; ++n)
                {
                    var name = message.ReadString();
                    var description = message.ReadString();
                    PlayerDetails.Add((name, description));
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.InspectionList);
            message.Write(IsPlayer);

            var count = Math.Min(Items.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (Name, Slot, Item, ImbuementIds, Details) = Items[i];

                message.Write(Name);

                if (IsPlayer)
                {
                    message.Write(Slot);
                }

                message.Write(Item);

                var size = Math.Min(ImbuementIds.Count, byte.MaxValue);
                message.Write((byte)size);
                for (var j = 0; j < size; ++j)
                {
                    message.Write(ImbuementIds[j]);
                }

                size = Math.Min(Details.Count, byte.MaxValue);
                message.Write((byte)size);
                for (var j = 0; j < size; ++j)
                {
                    message.Write(Details[j].Name);
                    message.Write(Details[j].Description);
                }
            }

            if (IsPlayer)
            {
                message.Write(PlayerName);
                if (PlayerOutfit is OutfitInstance)
                {
                    message.Write((OutfitInstance)PlayerOutfit);
                }
                else
                {
                    message.Write((ushort)0);
                    message.Write((ushort)PlayerOutfit.Id);
                }

                count = Math.Min(PlayerDetails.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var j = 0; j < count; ++j)
                {
                    message.Write(PlayerDetails[j].Name);
                    message.Write(PlayerDetails[j].Description);
                }
            }
        }
    }
}
