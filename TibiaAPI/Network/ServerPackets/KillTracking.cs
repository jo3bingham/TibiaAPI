using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class KillTracking : ServerPacket
    {
        public List<ObjectInstance> Loot { get; } = new List<ObjectInstance>();

        public string CreatureName { get; set; }

        public KillTracking()
        {
            PacketType = ServerPacketType.KillTracking;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.KillTracking)
            {
                return false;
            }

            // TODO
            CreatureName = message.ReadString();
            //message.ReadCreatureOutfit();
            Loot.Capacity = message.ReadByte();
            for (var i = 0; i < Loot.Capacity; ++i)
            {
                Loot.Add(message.ReadObjectInstance());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.KillTracking);
            // TODO
            message.Write(CreatureName);
            //message.Write(CreatureOutfit);
            var count = Math.Min(Loot.Capacity, byte.MaxValue);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Loot[i]);
            }
        }
    }
}
