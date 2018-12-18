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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.TibiaTime)
            {
                return false;
            }

            Hour = message.ReadByte();
            Minute = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TibiaTime);
            message.Write(Hour);
            message.Write(Minute);
        }
    }
}
