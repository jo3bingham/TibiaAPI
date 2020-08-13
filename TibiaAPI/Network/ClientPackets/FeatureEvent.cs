using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class FeatureEvent : ClientPacket
    {
        public uint UnknownUInt1 { get; set; }

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
                UnknownUInt1 = message.ReadUInt32();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.FeatureEvent);
            message.Write(EventType);
            message.Write(ShowWindow);
            if (!ShowWindow)
            {
                message.Write(UnknownUInt1);
            }
        }
    }
}
