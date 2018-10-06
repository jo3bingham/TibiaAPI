﻿using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LootContainer : ClientPacket
    {
        Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte ContainerId { get; set; }
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }

        public LootContainer()
        {
            PacketType = ClientPacketType.LootContainer;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.LootContainer)
            {
                return false;
            }

            // TODO: Figure out these unknowns.
            Unknown1 = message.ReadByte();
            ContainerId = message.ReadByte();
            Position = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            Unknown2 = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LootContainer);
            message.Write(Unknown1);
            message.Write(ContainerId);
            message.Write(Position);
            message.Write(ObjectId);
            message.Write(Unknown2);
        }
    }
}
