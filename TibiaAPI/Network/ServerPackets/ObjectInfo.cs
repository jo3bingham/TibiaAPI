﻿using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ObjectInfo : ServerPacket
    {
        public List<(ushort Id, byte Data, string Name)> Objects { get; } =
            new List<(ushort Id, byte Data, string Name)>();

        public ObjectInfo()
        {
            PacketType = ServerPacketType.ObjectInfo;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ObjectInfo)
            {
                return false;
            }

            Objects.Capacity = message.ReadByte();
            for (var i = 0; i < Objects.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var data = message.ReadByte();
                var name = message.ReadString();
                Objects.Add((id, data, name));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ObjectInfo);
            var count = Math.Min(Objects.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, Data, Name) = Objects[i];
                message.Write(Id);
                message.Write(Data);
                message.Write(Name);
            }
        }
    }
}
