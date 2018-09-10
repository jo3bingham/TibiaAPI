using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class OpenRewardWall : ServerPacket
    {
        public string WarningText { get; set; }

        public uint TimeLeftToClaimCurrentReward { get; set; }
        public uint TimeRemainingUntilCanClaimNextReward { get; set; }

        public ushort RewardStreak { get; set; }
        public ushort Unknown { get; set; }

        public byte IndexOfNextReward { get; set; }

        public bool IsRewardShrine { get; set; }
        public bool HasWarningForNextReward { get; set; }

        public OpenRewardWall()
        {
            PacketType = ServerPacketType.OpenRewardWall;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.OpenRewardWall)
            {
                return false;
            }

            IsRewardShrine = message.ReadBool();
            TimeRemainingUntilCanClaimNextReward = message.ReadUInt32();
            IndexOfNextReward = message.ReadByte();
            HasWarningForNextReward = message.ReadBool();
            if (HasWarningForNextReward)
            {
                WarningText = message.ReadString();
            }
            TimeLeftToClaimCurrentReward = message.ReadUInt32();
            RewardStreak = message.ReadUInt16();
            Unknown = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.OpenRewardWall);
            message.Write(IsRewardShrine);
            message.Write(TimeRemainingUntilCanClaimNextReward);
            message.Write(IndexOfNextReward);
            message.Write(HasWarningForNextReward);
            if (HasWarningForNextReward)
            {
                message.Write(WarningText);
            }
            message.Write(TimeLeftToClaimCurrentReward);
            message.Write(RewardStreak);
            message.Write(Unknown);
        }
    }
}
