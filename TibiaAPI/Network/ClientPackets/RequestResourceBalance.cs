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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.RequestResourceBalance)
            {
                return false;
            }

            ResourceType = (ResourceType)message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RequestResourceBalance);
            message.Write((byte)ResourceType);
        }
    }
}
