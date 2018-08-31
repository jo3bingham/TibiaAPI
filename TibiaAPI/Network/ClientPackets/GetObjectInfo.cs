using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetObjectInfo : ClientPacket
    {
        public List<(ushort ObjectId, byte Data)> Objects { get; } = new List<(ushort ObjectId, byte Data)>();

        public GetObjectInfo()
        {
            Type = ClientPacketType.GetObjectInfo;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
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
            var count = (byte)Math.Min(Objects.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var item = Objects[i];
                message.Write(item.ObjectId);
                message.Write(item.Data);
            }
        }
    }
}
