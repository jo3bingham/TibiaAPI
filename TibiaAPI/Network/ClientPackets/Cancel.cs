using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Cancel : ClientPacket
    {
        public Cancel(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Cancel;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Cancel);
        }
    }
}
