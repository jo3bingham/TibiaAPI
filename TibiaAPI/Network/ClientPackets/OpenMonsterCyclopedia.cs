using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenMonsterCyclopedia : ClientPacket
    {
        public OpenMonsterCyclopedia()
        {
            PacketType = ClientPacketType.OpenMonsterCyclopedia;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.OpenMonsterCyclopedia)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenMonsterCyclopedia);
        }
    }
}
