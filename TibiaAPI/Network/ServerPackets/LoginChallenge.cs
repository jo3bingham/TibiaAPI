using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginChallenge : ServerPacket
    {
        public uint Timestamp { get; set; }

        public byte Random { get; set; }

        public LoginChallenge(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.LoginChallenge;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Timestamp = message.ReadUInt32();
            Random = message.ReadByte();
            Client.Connection.ConnectionState = ConnectionState.ConnectingStage2;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LoginChallenge);
            message.Write(Timestamp);
            message.Write(Random);
        }
    }
}
