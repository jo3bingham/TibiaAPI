using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class FeatureEvent : ClientPacket
    {
        public byte EventType { get; set; }

        public bool ShowWindow { get; set; }

        public FeatureEvent()
        {
            Type = ClientPacketType.FeatureEvent;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            EventType = message.ReadByte();
            ShowWindow = message.ReadBool();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.FeatureEvent);
            message.Write(EventType);
            message.Write(ShowWindow);
        }
    }
}
