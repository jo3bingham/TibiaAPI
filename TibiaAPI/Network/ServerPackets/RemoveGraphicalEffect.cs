using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RemoveGraphicalEffect : ServerPacket
    {
        public Position Position { get; set; }

        public byte Id { get; set; }

        public RemoveGraphicalEffect(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.RemoveGraphicalEffect;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
            Id = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RemoveGraphicalEffect);
            message.Write(Position);
            message.Write(Id);
        }
    }
}
