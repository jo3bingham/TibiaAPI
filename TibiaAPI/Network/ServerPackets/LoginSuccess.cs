using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginSuccess : ServerPacket
    {
        public string StoreBaseUrl { get; set; }

        public double SpeedA { get; set; }
        public double SpeedB { get; set; }
        public double SpeedC { get; set; }

        public uint PlayerId { get; set; }

        public ushort BeatDuration { get; set; }

        public byte ReactivateAccountsCampaignId { get; set; }
        public byte StoreCreditPackageSize { get; set; }
        public byte TournamentType { get; set; }
        public byte WorldType { get; set; }

        public bool BugReportsAllowed { get; set; }
        public bool CanChangePvpFramingOption { get; set; }
        public bool EnableExpertModeButton { get; set; }

        public LoginSuccess(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.LoginSuccess;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
            if (Client.Player.Id != PlayerId)
            {
                Client.CreatureStorage.Reset();
            }
            Client.Player.Id = PlayerId;
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
            if (Client.VersionNumber >= 12158493)
            {
                TournamentType = message.ReadByte();
            }
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
            if (Client.VersionNumber >= 12158493)
            {
                message.Write(TournamentType);
            }
        }
    }
}
