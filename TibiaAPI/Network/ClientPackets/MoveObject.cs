using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MoveObject : ClientPacket
    {
        //public Position FromPosition { get; set; }
        //public Position ToPosition { get; set; }

        public ushort ObjectId { get; set; }

        public byte Amount { get; set; }
        public byte StackPosition { get; set; }

        public MoveObject()
        {
            Type = ClientPacketType.MoveObject;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            //FromPosition = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPosition = message.ReadByte();
            //ToPosition = message.ReadPosition();
            Amount = message.ReadByte();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MoveObject);
            //message.WritePosition(FromPosition);
            message.Write(ObjectId);
            message.Write(StackPosition);
            //message.WritePosition(ToPosition);
            message.Write(Amount);
        }
    }
}
