using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class QuickLoot : ClientPacket
    {
        //public Position Position { get; set; }

        public ushort CorpseId { get; set; }

        public byte Unknown { get; set; }

        public QuickLoot()
        {
            Type = ClientPacketType.QuickLoot;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.QuickLoot)
            {
                return false;
            }

            //Position = message.ReadPosition();
            CorpseId = message.ReadUInt16();
            Unknown = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.QuickLoot);
            //message.WritePosition(Position);
            message.Write(CorpseId);
            message.Write(Unknown);
        }
    }
}
