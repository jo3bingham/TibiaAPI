using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Mount : ClientPacket
    {
        public bool EnableMount { get; set; }

        public Mount()
        {
            PacketType = ClientPacketType.Mount;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.Mount)
            {
                return false;
            }

            EnableMount = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Mount);
            message.Write(EnableMount);
        }
    }
}
