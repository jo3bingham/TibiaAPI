using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureMarks : ServerPacket
    {
        public uint CreatureId { get; set; }

        public byte Mark { get; set; }
        public byte MarkType { get; set; }

        public CreatureMarks(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureMarks;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreatureMarks)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            MarkType = message.ReadByte();
            Mark = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureMarks);
            message.Write(CreatureId);
            message.Write(MarkType);
            message.Write(Mark);
        }
    }
}
