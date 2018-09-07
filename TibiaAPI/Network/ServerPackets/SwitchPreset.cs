using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SwitchPreset : ServerPacket
    {
        public uint Profession { get; set; }

        public SwitchPreset()
        {
            PacketType = ServerPacketType.SwitchPreset;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.SwitchPreset)
            {
                return false;
            }

            Profession = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SwitchPreset);
            message.Write(Profession);
        }
    }
}
