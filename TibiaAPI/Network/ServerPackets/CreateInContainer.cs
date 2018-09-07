using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreateInContainer : ServerPacket
    {
        public ushort Index { get; set; }

        public byte ContainerId { get; set; }

        public CreateInContainer()
        {
            PacketType = ServerPacketType.CreateInContainer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreateInContainer)
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
            message.Write((byte)ServerPacketType.CreateInContainer);
            message.Write(ContainerId);
            message.Write(Index);
            // TODO
            //message.WriteObjectInstance(Item);
        }
    }
}
