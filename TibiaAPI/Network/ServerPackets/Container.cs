using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Container : ServerPacket
    {
        public string ContainerName { get; set; }

        public ushort IndexOfFirstObject { get; set; }
        public ushort NumberOfTotalObjects { get; set; }

        public byte ContainerId { get; set; }
        public byte NumberOfContentObjects { get; set; }
        public byte NumberOfSlotsPerPage { get; set; }

        public bool IsDragAndDropEnabled { get; set; }
        public bool IsPaginationEnabled { get; set; }
        public bool IsSubContainer { get; set; }

        public Container()
        {
            PacketType = ServerPacketType.Container;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Container)
            {
                return false;
            }

            // TODO
            var containerId = message.ReadByte();
            //message.ReadObjectInstance();
            var containerName = message.ReadString();
            var numberOfSlotsPerPage = message.ReadByte();
            var isSubContainer = message.ReadBool();
            var isDragAndDropEnabled = message.ReadBool();
            var isPaginationEnabled = message.ReadBool();
            var numberOfTotalObjects = message.ReadUInt16();
            var indexOfFirstObject = message.ReadUInt16();
            var numberOfContentObjects = message.ReadByte();
            for (var i = 0; i < numberOfContentObjects; ++i)
            {
                //message.ReadObjectInstance();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.Write((byte)ServerPacketType.Container);
            message.Write(ContainerId);
            //message.WriteObjectInstance(Container);
            message.Write(ContainerName);
            message.Write(NumberOfSlotsPerPage);
            message.Write(IsSubContainer);
            message.Write(IsDragAndDropEnabled);
            message.Write(IsPaginationEnabled);
            message.Write(NumberOfTotalObjects);
            message.Write(IndexOfFirstObject);
            //var count = (byte)Math.Max(ContentObjects.Count byte.MaxValue);
            //message.WriteByte(count);
            //for (var i = 0; i < count; ++i)
            //{
            //    message.WriteObjectInstance(ContentObjects[i]);
            //}
        }
    }
}
