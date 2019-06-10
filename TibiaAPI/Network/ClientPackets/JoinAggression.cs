using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class JoinAggression : ClientPacket
    {
        public uint PlayerId { get; set; }

        public JoinAggression(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.JoinAggression;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.JoinAggression);
            message.Write(PlayerId);
        }
    }
}
