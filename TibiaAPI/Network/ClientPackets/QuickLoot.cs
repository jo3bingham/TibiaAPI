using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class QuickLoot : ClientPacket
    {
        public Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte Index { get; set; }

        public QuickLoot(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.QuickLoot;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            Index = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.QuickLoot);
            message.Write(Position);
            message.Write(ObjectId);
            message.Write(Index);
        }
    }
}
