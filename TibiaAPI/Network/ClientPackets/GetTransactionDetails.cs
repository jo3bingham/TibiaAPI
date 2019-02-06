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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GetTransactionDetails)
            {
                return false;
            }

            Id = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetTransactionDetails);
            message.Write(Id);
        }
    }
}
