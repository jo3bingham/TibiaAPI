using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RequestPurchaseData : ServerPacket
    {
        public uint PurchaseData { get; set; }

        public byte RequestType { get; set; }

        public RequestPurchaseData()
        {
            PacketType = ServerPacketType.RequestPurchaseData;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.RequestPurchaseData)
            {
                return false;
            }

            PurchaseData = message.ReadUInt32();
            RequestType = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RequestPurchaseData);
            message.Write(PurchaseData);
            message.Write(RequestType);
        }
    }
}
