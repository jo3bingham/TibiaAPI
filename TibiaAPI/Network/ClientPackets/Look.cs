using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Look : ClientPacket
    {
        //public Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte StackPosition { get; set; }

        public Look()
        {
            Type = ClientPacketType.Look;
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
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Look);
            //message.WritePosition(Position);
            message.Write(ObjectId);
            message.Write(StackPosition);
        }
    }
}
