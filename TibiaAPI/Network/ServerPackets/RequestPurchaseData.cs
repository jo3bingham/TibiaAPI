using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RequestPurchaseData : ServerPacket
    {
        public uint PurchaseData { get; set; }

        public byte RequestType { get; set; }

        public RequestPurchaseData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.RequestPurchaseData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PurchaseData = message.ReadUInt32();
            RequestType = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RequestPurchaseData);
            message.Write(PurchaseData);
            message.Write(RequestType);
        }
    }
}
