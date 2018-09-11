using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class NpcOffer : ServerPacket
    {
        public List<(ushort Id, byte Data, string Name, uint Price, uint Weight, uint Amount)> Offers { get; } =
            new List<(ushort Id, byte Data, string Name, uint Price, uint Weight, uint Amount)>();

        public string NpcName { get; set; }

        public NpcOffer()
        {
            PacketType = ServerPacketType.NpcOffer;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.NpcOffer)
            {
                return false;
            }

            NpcName = message.ReadString();
            Offers.Capacity = message.ReadUInt16();
            for (var i = 0; i < Offers.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var data = message.ReadByte();
                var name = message.ReadString();
                var price = message.ReadUInt32();
                var weight = message.ReadUInt32();
                var amount = message.ReadUInt32();
                Offers.Add((id, data, name, price, weight, amount));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.NpcOffer);
            message.Write(NpcName);
            var count = (ushort)Math.Min(Offers.Count, ushort.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var offer = Offers[i];
                message.Write(offer.Id);
                message.Write(offer.Data);
                message.Write(offer.Name);
                message.Write(offer.Price);
                message.Write(offer.Weight);
                message.Write(offer.Amount);
            }
        }
    }
}
