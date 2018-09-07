using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ToggleWrapState : ClientPacket
    {
        public Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte StackPosition { get; set; }

        public ToggleWrapState()
        {
            PacketType = ClientPacketType.ToggleWrapState;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.ToggleWrapState)
            {
                return false;
            }

            Position = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPosition = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ToggleWrapState);
            message.Write(Position);
            message.Write(ObjectId);
            message.Write(StackPosition);
        }
    }
}
