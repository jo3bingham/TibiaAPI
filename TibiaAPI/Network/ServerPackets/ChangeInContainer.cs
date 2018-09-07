using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ChangeInContainer : ServerPacket
    {
        public ushort Index { get; set; }

        public byte ContainerId { get; set; }

        public ChangeInContainer()
        {
            PacketType = ServerPacketType.ChangeInContainer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ChangeInContainer)
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
            message.Write((byte)ServerPacketType.ChangeInContainer);
            message.Write(ContainerId);
            message.Write(Index);
            // TODO
            //message.WriteObjectInstance(Item);
        }
    }
}
