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
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.CyclopediaHouseActionResult);
        }
    }
}
