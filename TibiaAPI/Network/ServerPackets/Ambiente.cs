using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Ambiente : ServerPacket
    {
        public byte Brightness { get; set; }
        public byte LightColor { get; set; }

        public Ambiente(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Ambiente;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Brightness = message.ReadByte();
            LightColor = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Ambiente);
            message.Write(Brightness);
            message.Write(LightColor);
        }
    }
}
