using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MarketStatistics : ServerPacket
    {
        public List<(ushort ObjectId, uint Price)> MarketObjects { get; } =
            new List<(ushort ObjectId, uint Price)>();

        public MarketStatistics()
        {
            PacketType = ServerPacketType.MarketStatistics;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MarketStatistics)
            {
                return false;
            }

            MarketObjects.Capacity = message.ReadUInt16();
            for (var i = 0; i < MarketObjects.Capacity; ++i)
            {
                var objectId = message.ReadUInt16();
                var price = message.ReadUInt32();
                MarketObjects.Add((objectId, price));
            }
            return true;
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
