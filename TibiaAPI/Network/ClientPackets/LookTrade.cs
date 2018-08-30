using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LookTrade : ClientPacket
    {
        public byte Index { get; set; }
        public byte Side { get; set; }

        public LookTrade()
        {
            Type = ClientPacketType.LookTrade;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            Side = message.ReadByte();
            Index = message.ReadByte();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LookTrade);
            message.Write(Side);
            message.Write(Index);
        }
    }
}
