using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Container : ServerPacket
    {
        public List<ObjectInstance> ContainerObjects { get; } = new List<ObjectInstance>();

        public ObjectInstance ContainerObject { get; set; }

        public string ContainerName { get; set; }

        public ushort IndexOfFirstObject { get; set; }
        public ushort NumberOfTotalObjects { get; set; }

        public byte ContainerId { get; set; }
        public byte NumberOfSlotsPerPage { get; set; }

        public bool IsDragAndDropEnabled { get; set; }
        public bool IsPaginationEnabled { get; set; }
        public bool IsSubContainer { get; set; }
        public bool ShowDepotSearchButton { get; set; }

        public Container(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Container;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ContainerId = message.ReadByte();
            ContainerObject = message.ReadObjectInstance();
            ContainerName = message.ReadString();
            NumberOfSlotsPerPage = message.ReadByte();
            IsSubContainer = message.ReadBool();
            if (Client.VersionNumber >= 12200000)
            {
                ShowDepotSearchButton = message.ReadBool();
            }
            IsDragAndDropEnabled = message.ReadBool();
            IsPaginationEnabled = message.ReadBool();
            NumberOfTotalObjects = message.ReadUInt16();
            IndexOfFirstObject = message.ReadUInt16();

            ContainerObjects.Capacity = message.ReadByte();
            for (var i = 0; i < ContainerObjects.Capacity; ++i)
            {
                ContainerObjects.Add(message.ReadObjectInstance());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Container);
            message.Write(ContainerId);
            message.Write(ContainerObject);
            message.Write(ContainerName);
            message.Write(NumberOfSlotsPerPage);
            message.Write(IsSubContainer);
            if (Client.VersionNumber >= 12200000)
            {
                message.Write(ShowDepotSearchButton);
            }
            message.Write(IsDragAndDropEnabled);
            message.Write(IsPaginationEnabled);
            message.Write(NumberOfTotalObjects);
            message.Write(IndexOfFirstObject);

            var count = Math.Min(ContainerObjects.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(ContainerObjects[i]);
            }
        }
    }
}
