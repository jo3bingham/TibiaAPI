using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ToggleWrapState : ClientPacket
    {
        //public Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte StackPosition { get; set; }

        public ToggleWrapState()
        {
            Type = ClientPacketType.ToggleWrapState;
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
            message.Write((byte)ClientPacketType.ToggleWrapState);
            //message.WritePosition(Position);
            message.Write(ObjectId);
            message.Write(StackPosition);
        }
    }
}
