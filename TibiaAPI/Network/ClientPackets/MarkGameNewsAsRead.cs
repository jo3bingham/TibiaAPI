using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarkGameNewsAsRead : ClientPacket
    {
        public uint NewsId { get; set; }

        public bool WasRead { get; set; }

        public MarkGameNewsAsRead(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MarkGameNewsAsRead;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            NewsId = message.ReadUInt32();
            WasRead = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarkGameNewsAsRead);
            message.Write(NewsId);
            message.Write(WasRead);
        }
    }
}
