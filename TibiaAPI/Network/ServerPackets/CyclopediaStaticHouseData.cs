using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaStaticHouseData : ServerPacket
    {
        public List<uint> HouseIds { get; } = new List<uint>();

        public CyclopediaStaticHouseData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaStaticHouseData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.ReadBytes(11); // 00 00 00 00 00 00 00 03 03 01 00 
            HouseIds.Capacity = message.ReadUInt16();
            for (var i = 0; i < HouseIds.Capacity; ++i)
            {
                HouseIds.Add(message.ReadUInt32()); // This is an assumption, and hasn't been verified.
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.CyclopediaStaticHouseData);
        }
    }
}
