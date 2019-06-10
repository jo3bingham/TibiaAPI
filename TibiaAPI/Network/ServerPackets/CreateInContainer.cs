using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreateInContainer : ServerPacket
    {
        public ObjectInstance Item { get; set; }

        public ushort Index { get; set; }

        public byte ContainerId { get; set; }

        public CreateInContainer(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreateInContainer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ContainerId = message.ReadByte();
            Index = message.ReadUInt16();
            Item = message.ReadObjectInstance();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreateInContainer);
            message.Write(ContainerId);
            message.Write(Index);
            message.Write(Item);
        }
    }
}
