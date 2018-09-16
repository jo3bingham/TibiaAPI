﻿using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UseTwoObjects : ClientPacket
    {
        public Position FromPosition { get; set; }
        public Position ToPosition { get; set; }

        public ushort FromObjectId { get; set; }
        public ushort ToObjectId { get; set; }

        public byte FromStackPositionOrData { get; set; }
        public byte ToStackPosition { get; set; }

        public UseTwoObjects()
        {
            PacketType = ClientPacketType.UseTwoObjects;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.UseTwoObjects)
            {
                return false;
            }

            FromPosition = message.ReadPosition();
            FromObjectId = message.ReadUInt16();
            FromStackPositionOrData = message.ReadByte();
            ToPosition = message.ReadPosition();
            ToObjectId = message.ReadUInt16();
            ToStackPosition = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UseTwoObjects);
            message.Write(FromPosition);
            message.Write(FromObjectId);
            message.Write(FromStackPositionOrData);
            message.Write(ToPosition);
            message.Write(ToObjectId);
            message.Write(ToStackPosition);
        }
    }
}
