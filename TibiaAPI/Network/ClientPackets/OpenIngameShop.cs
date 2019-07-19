using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenIngameShop : ClientPacket
    {
        public OpenIngameShop(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenIngameShop;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenIngameShop);
        }
    }
}
