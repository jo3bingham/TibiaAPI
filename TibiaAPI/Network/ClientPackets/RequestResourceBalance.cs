using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RequestResourceBalance : ClientPacket
    {
        public ResourceType ResourceType { get; set; }

        public RequestResourceBalance()
        {
            Type = ClientPacketType.RequestResourceBalance;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            ResourceType = (ResourceType)message.ReadByte();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RequestResourceBalance);
            message.Write((byte)ResourceType);
        }
    }
}
