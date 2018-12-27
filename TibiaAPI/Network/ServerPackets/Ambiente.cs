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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Ambiente)
            {
                return false;
            }

            Brightness = message.ReadByte();
            LightColor = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Ambiente);
            message.Write(Brightness);
            message.Write(LightColor);
        }
    }
}
