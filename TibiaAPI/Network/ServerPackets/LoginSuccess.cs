using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginSuccess : ServerPacket
    {
        public string StoreBaseUrl { get; set; }

        public double SpeedA { get; set; }
        public double SpeedB { get; set; }
        public double SpeedC { get; set; }

        public int PlayerId { get; set; }

        public ushort BeatDuration { get; set; }

        public byte ReactivateAccountsCampaignId { get; set; }
        public byte StoreCreditPackageSize { get; set; }
        public byte WorldType { get; set; }

        public bool BugReportsAllowed { get; set; }
        public bool CanChangePvpFramingOption { get; set; }
        public bool EnableExpertModeButton { get; set; }

        public LoginSuccess(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.LoginSuccess;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.LoginSuccess)
            {
                return false;
            }

            PlayerId = message.ReadInt32();
            BeatDuration = message.ReadUInt16();
            SpeedA = message.ReadDouble();
            SpeedB = message.ReadDouble();
            SpeedC = message.ReadDouble();
            BugReportsAllowed = message.ReadBool();
            CanChangePvpFramingOption = message.ReadBool();
            EnableExpertModeButton = message.ReadBool();
            StoreBaseUrl = message.ReadString();
            StoreCreditPackageSize = message.ReadByte();
            ReactivateAccountsCampaignId = message.ReadByte();
            WorldType = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LoginSuccess);
            message.Write(PlayerId);
            message.Write(BeatDuration);
            message.Write(SpeedA);
            message.Write(SpeedB);
            message.Write(SpeedC);
            message.Write(BugReportsAllowed);
            message.Write(CanChangePvpFramingOption);
            message.Write(EnableExpertModeButton);
            message.Write(StoreBaseUrl);
            message.Write(StoreCreditPackageSize);
            message.Write(ReactivateAccountsCampaignId);
            message.Write(WorldType);
        }
    }
}
