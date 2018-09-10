using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarkGameNewsAsRead : ClientPacket
    {
        public uint NewsId { get; set; }

        public bool WasRead { get; set; }

        public MarkGameNewsAsRead()
        {
            PacketType = ClientPacketType.MarkGameNewsAsRead;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.MarkGameNewsAsRead)
            {
                return false;
            }

            NewsId = message.ReadUInt32();
            WasRead = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarkGameNewsAsRead);
            message.Write(NewsId);
            message.Write(WasRead);
        }
    }
}
