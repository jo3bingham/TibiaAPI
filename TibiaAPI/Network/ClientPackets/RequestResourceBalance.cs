using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RequestResourceBalance : ClientPacket
    {
        public ResourceType ResourceType { get; set; }

        public RequestResourceBalance(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RequestResourceBalance;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ResourceType = (ResourceType)message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RequestResourceBalance);
            message.Write((byte)ResourceType);
        }
    }
}
