using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyTimeLeft : ServerPacket
    {
        public ushort TimeLeft { get; set; }

        public byte PreyArrayIndex { get; set; }

        public PreyTimeLeft(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PreyTimeLeft;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PreyArrayIndex = message.ReadByte();
            TimeLeft = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyTimeLeft);
            message.Write(PreyArrayIndex);
            message.Write(TimeLeft);
        }
    }
}
