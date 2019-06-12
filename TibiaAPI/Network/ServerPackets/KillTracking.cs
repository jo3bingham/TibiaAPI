using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class KillTracking : ServerPacket
    {
        public List<ObjectInstance> Loot { get; } = new List<ObjectInstance>();

        public AppearanceInstance CreatureOutfit { get; set; }

        public string CreatureName { get; set; }

        public KillTracking(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.KillTracking;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureName = message.ReadString();
            CreatureOutfit = message.ReadCreatureOutfit();
            Loot.Capacity = message.ReadByte();
            for (var i = 0; i < Loot.Capacity; ++i)
            {
                Loot.Add(message.ReadObjectInstance());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.KillTracking);
            message.Write(CreatureName);

            if (CreatureOutfit is OutfitInstance)
            {
                message.Write((OutfitInstance)CreatureOutfit);
            }
            else
            {
                message.Write((ushort)0);
                message.Write(CreatureOutfit.Id);
            }

            var count = Math.Min(Loot.Capacity, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Loot[i]);
            }
        }
    }
}
