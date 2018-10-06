﻿using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetObjectInfo : ClientPacket
    {
        public List<(ushort ObjectId, byte Data)> Objects { get; } = new List<(ushort ObjectId, byte Data)>();

        public GetObjectInfo()
        {
            PacketType = ClientPacketType.GetObjectInfo;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GetObjectInfo)
            {
                return false;
            }

            Objects.Capacity = message.ReadByte();
            for (var i = 0; i < Objects.Capacity; ++i)
            {
                var objectId = message.ReadUInt16();
                var data = message.ReadByte();
                Objects.Add((objectId, data));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetObjectInfo);
            var count = Math.Min(Objects.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (ObjectId, Data) = Objects[i];
                message.Write(ObjectId);
                message.Write(Data);
            }
        }
    }
}
