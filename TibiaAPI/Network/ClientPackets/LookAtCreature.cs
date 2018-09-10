﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LookAtCreature : ClientPacket
    {
        public uint CreatureId { get; set; }

        public LookAtCreature()
        {
            PacketType = ClientPacketType.LookAtCreature;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.LookAtCreature)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LookAtCreature);
            message.Write(CreatureId);
        }
    }
}
