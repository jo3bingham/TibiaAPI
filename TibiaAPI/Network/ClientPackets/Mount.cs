using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Mount : ClientPacket
    {
        public bool EnableMount { get; set; }

        public Mount(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Mount;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            EnableMount = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Mount);
            message.Write(EnableMount);
        }
    }
}
