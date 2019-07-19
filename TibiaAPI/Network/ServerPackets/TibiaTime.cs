using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TibiaTime : ServerPacket
    {
        public byte Hour { get; set; }
        public byte Minute { get; set; }

        public TibiaTime(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TibiaTime;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Hour = message.ReadByte();
            Minute = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TibiaTime);
            message.Write(Hour);
            message.Write(Minute);
        }
    }
}
