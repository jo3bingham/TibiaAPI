using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TradeObject : ClientPacket
    {
        public Position Position { get; set; }

        public uint TradePartnerId { get; set; }

        public ushort ObjectId { get; set; }

        public byte StackPosition { get; set; }

        public TradeObject(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TradeObject;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPosition = message.ReadByte();
            TradePartnerId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TradeObject);
            message.Write(Position);
            message.Write(ObjectId);
            message.Write(StackPosition);
            message.Write(TradePartnerId);
        }
    }
}
