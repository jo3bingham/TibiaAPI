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

        public KillTracking()
        {
            PacketType = ServerPacketType.KillTracking;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.KillTracking)
            {
                return false;
            }

            CreatureName = message.ReadString();
            CreatureOutfit = message.ReadCreatureOutfit(client);
            Loot.Capacity = message.ReadByte();
            for (var i = 0; i < Loot.Capacity; ++i)
            {
                Loot.Add(message.ReadObjectInstance(client));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.KillTracking);
            message.Write(CreatureName);

            if (CreatureOutfit is OutfitInstance)
            {
                message.Write((OutfitInstance)CreatureOutfit);
            }
            else if (CreatureOutfit is ObjectInstance)
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
