using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LookAtCreature : ClientPacket
    {
        public uint CreatureId { get; set; }

        public LookAtCreature()
        {
            Type = ClientPacketType.LookAtCreature;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LookAtCreature);
            message.Write(CreatureId);
        }
    }
}
