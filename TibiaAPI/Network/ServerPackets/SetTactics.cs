using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SetTactics : ServerPacket
    {
        public byte AttackMode { get; set; }
        public byte ChaseMode { get; set; }
        public byte PvpMode { get; set; }
        public byte SecureMode { get; set; }

        public SetTactics()
        {
            PacketType = ServerPacketType.SetTactics;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.SetTactics)
            {
                return false;
            }

            AttackMode = message.ReadByte();
            ChaseMode = message.ReadByte();
            SecureMode = message.ReadByte();
            PvpMode = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SetTactics);
            message.Write(AttackMode);
            message.Write(ChaseMode);
            message.Write(SecureMode);
            message.Write(PvpMode);
        }
    }
}
