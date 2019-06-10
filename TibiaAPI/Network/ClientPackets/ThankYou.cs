using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ThankYou : ClientPacket
    {
        public uint StatementId { get; set; }

        public ThankYou(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.ThankYou;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            StatementId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ThankYou);
            message.Write(StatementId);
        }
    }
}
