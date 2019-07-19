using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetTransactionDetails : ClientPacket
    {
        public uint Id { get; set; }

        public GetTransactionDetails(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GetTransactionDetails;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Id = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetTransactionDetails);
            message.Write(Id);
        }
    }
}
