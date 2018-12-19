using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaCharacterInfo : ServerPacket
    {
        public CyclopediaCharacterInfo(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaCharacterInfo;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CyclopediaCharacterInfo)
            {
                return false;
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CyclopediaCharacterInfo);
        }
    }
}
