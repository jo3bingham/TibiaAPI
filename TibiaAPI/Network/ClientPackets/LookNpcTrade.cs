using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LookNpcTrade : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public byte Data { get; set; }

        public LookNpcTrade()
        {
            Type = ClientPacketType.LookNpcTrade;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            ObjectId = message.ReadUInt16();
            Data = message.ReadByte();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LookNpcTrade);
            message.Write(ObjectId);
            message.Write(Data);
        }
    }
}
