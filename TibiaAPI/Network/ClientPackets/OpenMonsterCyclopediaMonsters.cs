using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenMonsterCyclopediaMonsters : ClientPacket
    {
        public List<ushort> RaceIds { get; } = new List<ushort>();

        public string RaceName { get; set; }

        public byte RaceType { get; set; }

        public OpenMonsterCyclopediaMonsters()
        {
           PacketType = ClientPacketType.OpenMonsterCyclopediaMonsters;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.OpenMonsterCyclopediaMonsters)
            {
                return false;
            }

            RaceType = message.ReadByte();
            if (RaceType == 0)
            {
                RaceName = message.ReadString();
            }
            else if (RaceType == 1)
            {
                RaceIds.Capacity = message.ReadUInt16();
                for (var i = 0; i < RaceIds.Capacity; ++i)
                {
                    RaceIds.Add(message.ReadUInt16());
                }
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenMonsterCyclopediaMonsters);
            message.Write(RaceType);
            if (RaceType == 0)
            {
                message.Write(RaceName);
            }
            else if (RaceType == 1)
            {
                var count = (ushort)Math.Min(RaceIds.Count, ushort.MaxValue);
                message.Write(count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(RaceIds[i]);
                }
            }
        }
    }
}
