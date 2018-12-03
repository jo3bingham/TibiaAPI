using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class QuickLoot : ClientPacket
    {
        public Position Position { get; set; }

        public ushort CorpseId { get; set; }

        public byte Unknown { get; set; }

        public QuickLoot()
        {
            PacketType = ClientPacketType.QuickLoot;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.QuickLoot)
            {
                return false;
            }

            Position = message.ReadPosition();
            CorpseId = message.ReadUInt16();
            // TODO: Figure out this unknown.
            Unknown = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.QuickLoot);
            message.Write(Position);
            message.Write(CorpseId);
            message.Write(Unknown);
        }
    }
}
