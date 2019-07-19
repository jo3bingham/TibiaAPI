using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenMonsterCyclopedia : ClientPacket
    {
        public OpenMonsterCyclopedia(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenMonsterCyclopedia;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenMonsterCyclopedia);
        }
    }
}
