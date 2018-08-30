using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class StoreEvent : ClientPacket
    {
        public uint OfferId { get; set; }

        public byte EventType { get; set; }

        public StoreEvent()
        {
            Type = ClientPacketType.StoreEvent;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            EventType = message.ReadByte();
            if (EventType == 0 || EventType == 3)
            {
                OfferId = message.ReadUInt32();
            }
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.StoreEvent);
            message.Write(EventType);
            if (EventType == 0 || EventType == 3)
            {
                message.Write(OfferId);
            }
        }
    }
}
