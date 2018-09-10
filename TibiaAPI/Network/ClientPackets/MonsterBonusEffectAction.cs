﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MonsterBonusEffectAction : ClientPacket
    {
        public MonsterBonusEffectAction()
        {
            PacketType = ClientPacketType.MonsterBonusEffectAction;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.MonsterBonusEffectAction)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MonsterBonusEffectAction);
        }
    }
}
