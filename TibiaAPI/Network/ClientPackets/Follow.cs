using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Follow : ClientPacket
    {
        public uint CreatureId { get; set; }

        public Follow()
        {
            Type = ClientPacketType.Follow;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            message.ReadUInt32(); // Creature id again.
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Follow);
            message.Write(CreatureId);
            message.Write(CreatureId);
        }
    }
}
