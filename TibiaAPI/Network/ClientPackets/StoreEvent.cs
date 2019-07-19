using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class StoreEvent : ClientPacket
    {
        public uint OfferId { get; set; }

        public byte EventType { get; set; }

        public StoreEvent(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.StoreEvent;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            EventType = message.ReadByte();
            if (EventType == 0 || EventType == 3)
            {
                OfferId = message.ReadUInt32();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
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
