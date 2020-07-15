using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaHouseActionResult : ServerPacket
    {
        public CyclopediaHouseActionResult(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaHouseActionResult;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO

            // 3D 28 00 00 01 11
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.CyclopediaHouseActionResult);
        }
    }
}
