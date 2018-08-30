using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TradeObject : ClientPacket
    {
        //public Position Position { get; set; }

        public uint TradePartnerId { get; set; }

        public ushort ObjectId { get; set; }

        public byte StackPosition { get; set; }

        public TradeObject()
        {
            Type = ClientPacketType.TradeObject;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            //Position = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPosition = message.ReadByte();
            TradePartnerId = message.ReadUInt32();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TradeObject);
            //message.WritePosition(Position);
            message.Write(ObjectId);
            message.Write(StackPosition);
            message.Write(TradePartnerId);
        }
    }
}
