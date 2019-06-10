using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ExivaSuppressed : ServerPacket
    {
        public bool IsSuppressed { get; set; }

        public ExivaSuppressed(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ExivaSuppressed;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            IsSuppressed = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ExivaSuppressed);
            message.Write(IsSuppressed);
        }
    }
}
