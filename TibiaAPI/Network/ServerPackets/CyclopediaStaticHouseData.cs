using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaStaticHouseData : ServerPacket
    {
        public CyclopediaStaticHouseData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaStaticHouseData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            message.ReadBytes(11); // 00 00 00 00 00 00 00 03 03 01 00 
            var count = message.ReadUInt16();
            for (var i = 0; i < count; ++i)
            {
                message.ReadUInt32(); //house id
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CyclopediaStaticHouseData);
        }
    }
}
