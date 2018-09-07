using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaBonusEffects : ServerPacket
    {
        public MonsterCyclopediaBonusEffects()
        {
            PacketType = ServerPacketType.MonsterCyclopediaBonusEffects;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MonsterCyclopediaBonusEffects)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopediaBonusEffects);
        }
    }
}
