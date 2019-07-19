using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MarketStatistics : ServerPacket
    {
        public List<(ushort ObjectId, uint Price)> MarketObjects { get; } =
            new List<(ushort ObjectId, uint Price)>();

        public MarketStatistics(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MarketStatistics;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            MarketObjects.Capacity = message.ReadUInt16();
            for (var i = 0; i < MarketObjects.Capacity; ++i)
            {
                var objectId = message.ReadUInt16();
                var price = message.ReadUInt32();
                MarketObjects.Add((objectId, price));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MarketStatistics);
            var count = Math.Min(MarketObjects.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (ObjectId, Price) = MarketObjects[i];
                message.Write(ObjectId);
                message.Write(Price);
            }
        }
    }
}
