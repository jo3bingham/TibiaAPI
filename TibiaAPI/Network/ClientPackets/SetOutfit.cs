using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SetOutfit : ClientPacket
    {
        public byte UnknownByte1 { get; set; }

        public ushort MountId { get; set; }
        public ushort OutfitId { get; set; }

        public byte Addons { get; set; }
        public byte DetailColor { get; set; }
        public byte HeadColor { get; set; }
        public byte LegsColor { get; set; }
        public byte TorsoColor { get; set; }

        public SetOutfit(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SetOutfit;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            if (Client.VersionNumber >= 12209066)
            {
                // TODO
                UnknownByte1 = message.ReadByte();
            }
            OutfitId = message.ReadUInt16();
            HeadColor = message.ReadByte();
            TorsoColor = message.ReadByte();
            LegsColor = message.ReadByte();
            DetailColor = message.ReadByte();
            Addons = message.ReadByte();
            MountId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SetOutfit);
            if (Client.VersionNumber >= 12209066)
            {
                message.Write(UnknownByte1);
            }
            message.Write(OutfitId);
            message.Write(HeadColor);
            message.Write(TorsoColor);
            message.Write(LegsColor);
            message.Write(DetailColor);
            message.Write(MountId);
        }
    }
}
