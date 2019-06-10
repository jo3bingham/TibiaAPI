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

        public MoveObject(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MoveObject;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            FromPosition = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPosition = message.ReadByte();
            ToPosition = message.ReadPosition();
            Amount = message.ReadByte();
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
