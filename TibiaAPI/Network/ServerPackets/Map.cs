using System.Collections.Generic;

using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Map : ServerPacket
    {
        public List<(Field, Position)> Fields { get; } = new List<(Field, Position)>();
    }
}
