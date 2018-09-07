using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureLight : ServerPacket
    {
        public uint CreatureId { get; set; }

        public byte Brightness { get; set; }
        public byte LightColor { get; set; }

        public CreatureLight()
        {
            PacketType = ServerPacketType.CreatureLight;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreatureLight)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            Brightness = message.ReadByte();
            LightColor = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureLight);
            message.Write(CreatureId);
            message.Write(Brightness);
            message.Write(LightColor);
        }
    }
}
