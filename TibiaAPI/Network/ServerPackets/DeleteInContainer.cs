using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DeleteInContainer : ServerPacket
    {
        public ObjectInstance Item { get; set; }

        public ushort Index { get; set; }

        public byte ContainerId { get; set; }

        public DeleteInContainer()
        {
            PacketType = ServerPacketType.DeleteInContainer;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.DeleteInContainer)
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
            message.Write((byte)ServerPacketType.DeleteInContainer);
            message.Write(ContainerId);
            message.Write(Index);
            message.Write(Item);
        }
    }
}
