using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SecondaryLogin : ClientPacket
    {
        public SecondaryLogin(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SecondaryLogin;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SecondaryLogin);
        }
    }
}
