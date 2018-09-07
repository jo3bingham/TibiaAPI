using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DeleteInContainer : ServerPacket
    {
        public ushort Index { get; set; }

        public byte ContainerId { get; set; }

        public DeleteInContainer()
        {
            PacketType = ServerPacketType.DeleteInContainer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.DeleteInContainer)
            {
                return false;
            }

            ContainerId = message.ReadByte();
            Index = message.ReadUInt16();
            // TODO
            //message.ReadObjectInstance();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DeleteInContainer);
            message.Write(ContainerId);
            message.Write(Index);
            // TODO
            //message.WriteObjectInstance(Item);
        }
    }
}
