using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SnapBack : ServerPacket
    {
        public Direction Direction { get; set; }

        public SnapBack(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SnapBack;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Direction = (Direction)message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SnapBack);
            message.Write((byte)Direction);
        }
    }
}
