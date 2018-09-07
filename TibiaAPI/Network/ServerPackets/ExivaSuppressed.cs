using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ExivaSuppressed : ServerPacket
    {
        public bool IsSuppressed { get; set; }

        public ExivaSuppressed()
        {
            PacketType = ServerPacketType.ExivaSuppressed;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ExivaSuppressed)
            {
                return false;
            }

            IsSuppressed = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ExivaSuppressed);
            message.Write(IsSuppressed);
        }
    }
}
