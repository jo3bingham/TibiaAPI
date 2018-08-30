using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarkGameNewsAsRead : ClientPacket
    {
        public uint NewsId { get; set; }

        public bool WasRead { get; set; }

        public MarkGameNewsAsRead()
        {
            Type = ClientPacketType.MarkGameNewsAsRead;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            NewsId = message.ReadUInt32();
            WasRead = message.ReadBool();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarkGameNewsAsRead);
            message.Write(NewsId);
            message.Write(WasRead);
        }
    }
}
