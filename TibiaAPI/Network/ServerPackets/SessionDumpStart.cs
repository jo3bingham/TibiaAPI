using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SessionDumpStart : ServerPacket
    {
        public SessionDumpStart(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SessionDumpStart;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.SessionDumpStart);
        }
    }
}
