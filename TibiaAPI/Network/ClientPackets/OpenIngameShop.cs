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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.OpenIngameShop)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenIngameShop);
        }
    }
}
