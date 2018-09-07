using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ApplyImbuement : ClientPacket
    {
        public uint ImbuementId { get; set; }

        public byte Slot { get; set; }

        public bool UseProtectionCharm { get; set; }

        public ApplyImbuement()
        {
            PacketType = ClientPacketType.ApplyImbuement;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.ApplyImbuement)
            {
                return false;
            }

            Slot = message.ReadByte();
            ImbuementId = message.ReadUInt32();
            UseProtectionCharm = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ApplyImbuement);
            message.Write(Slot);
            message.Write(ImbuementId);
            message.Write(UseProtectionCharm);
        }
    }
}
