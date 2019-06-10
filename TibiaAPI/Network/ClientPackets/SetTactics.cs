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

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            AttackMode = message.ReadByte();
            ChaseMode = message.ReadByte();
            SecureMode = message.ReadByte();
            PvpMode = message.ReadByte();
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
