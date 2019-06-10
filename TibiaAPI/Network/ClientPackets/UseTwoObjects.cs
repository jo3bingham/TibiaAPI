using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UseTwoObjects : ClientPacket
    {
        public Position FromPosition { get; set; }
        public Position ToPosition { get; set; }

        public ushort FromObjectId { get; set; }
        public ushort ToObjectId { get; set; }

        public byte FromStackPositionOrData { get; set; }
        public byte ToStackPosition { get; set; }

        public UseTwoObjects(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.UseTwoObjects;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            FromPosition = message.ReadPosition();
            FromObjectId = message.ReadUInt16();
            FromStackPositionOrData = message.ReadByte();
            ToPosition = message.ReadPosition();
            ToObjectId = message.ReadUInt16();
            ToStackPosition = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UseTwoObjects);
            message.Write(FromPosition);
            message.Write(FromObjectId);
            message.Write(FromStackPositionOrData);
            message.Write(ToPosition);
            message.Write(ToObjectId);
            message.Write(ToStackPosition);
        }
    }
}
