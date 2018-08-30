﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetQuestLog : ClientPacket
    {
        public GetQuestLog()
        {
            Type = ClientPacketType.GetQuestLog;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetQuestLog);
        }
    }
}
