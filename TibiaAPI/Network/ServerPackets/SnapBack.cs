using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SnapBack : ServerPacket
    {
        public Direction Direction { get; set; }

        public SnapBack()
        {
            PacketType = ServerPacketType.SnapBack;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.SnapBack)
            {
                return false;
            }

            Direction = (Direction)message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SnapBack);
            message.Write((byte)Direction);
        }
    }
}
