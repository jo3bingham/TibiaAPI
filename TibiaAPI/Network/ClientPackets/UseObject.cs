using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UseObject : ClientPacket
    {
        public Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte Index { get; set; }
        public byte StackPositionOrData { get; set; }

        public UseObject()
        {
            Type = ClientPacketType.UseObject;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.UseObject)
            {
                return false;
            }

            Position = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPositionOrData = message.ReadByte();
            Index = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UseObject);
            message.Write(Position);
            message.Write(ObjectId);
            message.Write(StackPositionOrData);
            message.Write(Index);
        }
    }
}
