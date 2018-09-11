﻿using System;
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

        public Container()
        {
            PacketType = ServerPacketType.Container;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Container)
            {
                return false;
            }

            ContainerId = message.ReadByte();
            ContainerObject = message.ReadObjectInstance(client);
            ContainerName = message.ReadString();
            NumberOfSlotsPerPage = message.ReadByte();
            IsSubContainer = message.ReadBool();
            IsDragAndDropEnabled = message.ReadBool();
            IsPaginationEnabled = message.ReadBool();
            NumberOfTotalObjects = message.ReadUInt16();
            IndexOfFirstObject = message.ReadUInt16();

            ContainerObjects.Capacity = message.ReadByte();
            for (var i = 0; i < ContainerObjects.Capacity; ++i)
            {
                ContainerObjects.Add(message.ReadObjectInstance(client));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Container);
            message.Write(ContainerId);
            message.Write(ContainerObject);
            message.Write(ContainerName);
            message.Write(NumberOfSlotsPerPage);
            message.Write(IsSubContainer);
            message.Write(IsDragAndDropEnabled);
            message.Write(IsPaginationEnabled);
            message.Write(NumberOfTotalObjects);
            message.Write(IndexOfFirstObject);

            var count = (byte)Math.Min(ContainerObjects.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(ContainerObjects[i]);
            }
        }
    }
}
