using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SubscribeToUpdates : ClientPacket
    {
        public SubscribeToUpdates(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SubscribeToUpdates;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.SubscribeToUpdates);
        }
    }
}
