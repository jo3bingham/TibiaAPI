using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SetOutfit : ClientPacket
    {
        public ushort MountId { get; set; }
        public ushort OutfitId { get; set; }

        public byte Addons { get; set; }
        public byte DetailColor { get; set; }
        public byte HeadColor { get; set; }
        public byte LegsColor { get; set; }
        public byte TorsoColor { get; set; }

        public SetOutfit()
        {
            Type = ClientPacketType.SetOutfit;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.SetOutfit)
            {
                return false;
            }

            OutfitId = message.ReadUInt16();
            HeadColor = message.ReadByte();
            TorsoColor = message.ReadByte();
            LegsColor = message.ReadByte();
            DetailColor = message.ReadByte();
            Addons = message.ReadByte();
            MountId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SetOutfit);
            message.Write(OutfitId);
            message.Write(HeadColor);
            message.Write(TorsoColor);
            message.Write(LegsColor);
            message.Write(DetailColor);
            message.Write(MountId);
        }
    }
}
