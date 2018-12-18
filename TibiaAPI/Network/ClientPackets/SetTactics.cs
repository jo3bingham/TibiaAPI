using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SetTactics : ClientPacket
    {
        public byte AttackMode { get; set; }
        public byte ChaseMode { get; set; }
        public byte PvpMode { get; set; }
        public byte SecureMode { get; set; }

        public SetTactics(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SetTactics;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.SetTactics)
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
            message.Write((byte)ClientPacketType.SetTactics);
            message.Write(AttackMode);
            message.Write(ChaseMode);
            message.Write(SecureMode);
            message.Write(PvpMode);
        }
    }
}
