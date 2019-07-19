using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PassLeadership : ClientPacket
    {
        public uint PlayerId { get; set; }

        public PassLeadership(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PassLeadership;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PassLeadership);
            message.Write(PlayerId);
        }
    }
}
