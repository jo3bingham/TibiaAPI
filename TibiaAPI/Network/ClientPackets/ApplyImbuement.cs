using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ApplyImbuement : ClientPacket
    {
        public uint ImbuementId { get; set; }

        public byte Slot { get; set; }

        public bool UseProtectionCharm { get; set; }

        public ApplyImbuement(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.ApplyImbuement;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Slot = message.ReadByte();
            ImbuementId = message.ReadUInt32();
            UseProtectionCharm = message.ReadBool();
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
