using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class FeatureEvent : ClientPacket
    {
        public byte EventType { get; set; }

        public bool ShowWindow { get; set; }

        public FeatureEvent()
        {
            PacketType = ClientPacketType.FeatureEvent;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.FeatureEvent)
            {
                return false;
            }

            EventType = message.ReadByte();
            ShowWindow = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.FeatureEvent);
            message.Write(EventType);
            message.Write(ShowWindow);
        }
    }
}
