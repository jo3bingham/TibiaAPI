using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MoveObject : ClientPacket
    {
        public Position FromPosition { get; set; }
        public Position ToPosition { get; set; }

        public ushort ObjectId { get; set; }

        public byte Amount { get; set; }
        public byte StackPosition { get; set; }

        public MoveObject()
        {
            PacketType = ClientPacketType.MoveObject;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.MoveObject)
            {
                return false;
            }

            FromPosition = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPosition = message.ReadByte();
            ToPosition = message.ReadPosition();
            Amount = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MoveObject);
            message.Write(FromPosition);
            message.Write(ObjectId);
            message.Write(StackPosition);
            message.Write(ToPosition);
            message.Write(Amount);
        }
    }
}
