using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class FeatureEvent : ClientPacket
    {
        public byte EventType { get; set; }

        public bool ShowWindow { get; set; }

        public FeatureEvent(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.FeatureEvent;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            EventType = message.ReadByte();
            ShowWindow = message.ReadBool();
            if (!ShowWindow)
            {
                // TODO
                message.ReadUInt32();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.FeatureEvent);
            // message.Write(EventType);
            // message.Write(ShowWindow);
            // if (!ShowWindow)
            // {
            //     //message.Write(Unknown);
            // }
        }
    }
}
