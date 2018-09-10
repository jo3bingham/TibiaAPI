﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LookNpcTrade : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public byte Data { get; set; }

        public LookNpcTrade()
        {
            PacketType = ClientPacketType.LookNpcTrade;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.LookNpcTrade)
            {
                return false;
            }

            ObjectId = message.ReadUInt16();
            Data = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LookNpcTrade);
            message.Write(ObjectId);
            message.Write(Data);
        }
    }
}
