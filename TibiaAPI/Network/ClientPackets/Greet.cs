using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Greet : ClientPacket
    {
        public Greet(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Greet;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Greet);
        }
    }
}
