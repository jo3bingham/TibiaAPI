using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SetTactics : ClientPacket
    {
        public byte AttackMode { get; set; }
        public byte ChaseMode { get; set; }
        public byte PvpMode { get; set; }
        public byte SecureMode { get; set; }

        public SetTactics()
        {
            Type = ClientPacketType.SetTactics;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            AttackMode = message.ReadByte();
            ChaseMode = message.ReadByte();
            SecureMode = message.ReadByte();
            PvpMode = message.ReadByte();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SetTactics);
            message.Write(AttackMode);
            message.Write(ChaseMode);
            message.Write(SecureMode);
            message.Write(PvpMode);
        }
    }
}
