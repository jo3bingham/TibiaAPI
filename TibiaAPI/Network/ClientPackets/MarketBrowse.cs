using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketBrowse : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public MarketBrowse()
        {
            Type = ClientPacketType.MarketBrowse;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            ObjectId = message.ReadUInt16();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketBrowse);
            message.Write(ObjectId);
        }
    }
}
