using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MarketEnter : ServerPacket
    {
        public List<(ushort ObjectId, ushort Count)> DepotObjects { get; } =
            new List<(ushort ObjectId, ushort Count)>();

        public long AccountBalance { get; set; }

        public byte ActiveOffers { get; set; }

        public MarketEnter(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MarketEnter;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            if (Client.VersionNumber < 125000000)
            {
                AccountBalance = message.ReadInt64();
            }

            ActiveOffers = message.ReadByte();

            DepotObjects.Capacity = message.ReadUInt16();
            for (var i = 0; i < DepotObjects.Capacity; ++i)
            {
                var objectId = message.ReadUInt16();
                var count = message.ReadUInt16();
                DepotObjects.Add((objectId, count));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MarketEnter);

            if (Client.VersionNumber < 125000000)
            {
                message.Write(AccountBalance);
            }

            message.Write(ActiveOffers);

            var count = Math.Min(DepotObjects.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (ObjectId, Count) = DepotObjects[i];
                message.Write(ObjectId);
                message.Write(Count);
            }
        }
    }
}
