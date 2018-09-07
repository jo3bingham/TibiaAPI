using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ShowGameNews : ServerPacket
    {
        public ShowGameNews()
        {
            PacketType = ServerPacketType.ShowGameNews;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ShowGameNews)
            {
                return false;
            }

            // TODO
            message.ReadUInt32(); // Category Id?
            message.ReadByte(); // Page Number?
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ShowGameNews);
            // TODO
        }
    }
}
