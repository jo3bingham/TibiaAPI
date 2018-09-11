using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreateInContainer : ServerPacket
    {
        public ObjectInstance Item { get; set; }

        public ushort Index { get; set; }

        public byte ContainerId { get; set; }

        public CreateInContainer()
        {
            PacketType = ServerPacketType.CreateInContainer;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreateInContainer)
            {
                return false;
            }

            ContainerId = message.ReadByte();
            Index = message.ReadUInt16();
            Item = message.ReadObjectInstance(client);
            return true;
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
