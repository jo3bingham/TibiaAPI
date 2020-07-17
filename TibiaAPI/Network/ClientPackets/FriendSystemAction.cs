using System;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class FriendSystemAction : ClientPacket
    {
        public string PlayerName { get; set; }

        public uint BadgeId { get; set; }
        public uint PlayerId { get; set; }

        public byte Action { get; set; }
        public byte ConfigId { get; set; }
        public byte FriendshipLevel { get; set; }

        public bool AllowInspect { get; set; }
        public bool DisplayBadge { get; set; }
        public bool ShowAccountInfo { get; set; }
        public bool ShowCharacterInfo { get; set; }

        public FriendSystemAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.FriendSystemAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO: Figure out Action 0x0E (if exists),
            // and others (if there are any).
            Action = message.ReadByte();
            if (Action == 0x00) // Open Window
            {
            }
            else if (Action == 0x01) // Get Invitations
            {
            }
            else if (Action == 0x02) // Get Blacklist
            {
            }
            else if (Action == 0x03) // Send Invitation
            {
                PlayerId = message.ReadUInt32();
            }
            else if (Action == 0x04) // Accept Invitation
            {
                PlayerId = message.ReadUInt32();
                FriendshipLevel = message.ReadByte();
            }
            else if (Action == 0x05) // Decline Invitation
            {
                PlayerId = message.ReadUInt32();
            }
            else if (Action == 0x06) // Cancel Invitation
            {
                PlayerId = message.ReadUInt32();
            }
            else if (Action == 0x07) // Add to Blacklist
            {
                PlayerId = message.ReadUInt32();
            }
            else if (Action == 0x08) // Remove from Blacklist
            {
                PlayerId = message.ReadUInt32();
            }
            else if (Action == 0x09) // Unfriend
            {
                PlayerId = message.ReadUInt32();
            }
            else if (Action == 0x0A) // Change Friendship Level
            {
                PlayerId = message.ReadUInt32();
                FriendshipLevel = message.ReadByte();
            }
            else if (Action == 0x0B) // Search
            {
                PlayerName = message.ReadString();
            }
            else if (Action == 0x0C) // Badges
            {
            }
            else if (Action == 0x0D) // Change Badge Display
            {
                BadgeId = message.ReadUInt32();
                DisplayBadge = message.ReadBool();
            }
            else if (Action == 0x0F) // Friends Config
            {
            }
            else if (Action == 0x10) // Change Config
            {
                ConfigId = message.ReadByte();
                ShowCharacterInfo = message.ReadBool();
                ShowAccountInfo = message.ReadBool();
                AllowInspect = message.ReadBool();
            }
            else
            {
                throw new Exception($"Invalid Friend System Action: {Action}");
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.FriendSystemAction);
            message.Write(Action);
            if (Action == 0x00) // Open Window
            {
            }
            else if (Action == 0x01) // Get Invitations
            {
            }
            else if (Action == 0x02) // Get Blacklist
            {
            }
            else if (Action == 0x03) // Send Invitation
            {
                message.Write(PlayerId);
            }
            else if (Action == 0x04) // Accept Invitation
            {
                message.Write(PlayerId);
                message.Write(FriendshipLevel);
            }
            else if (Action == 0x05) // Decline Invitation
            {
                message.Write(PlayerId);
            }
            else if (Action == 0x06) // Cancel Invitation
            {
                message.Write(PlayerId);
            }
            else if (Action == 0x07) // Add to Blacklist
            {
                message.Write(PlayerId);
            }
            else if (Action == 0x08) // Remove from Blacklist
            {
                message.Write(PlayerId);
            }
            else if (Action == 0x09) // Unfriend
            {
                message.Write(PlayerId);
            }
            else if (Action == 0x0A) // Change Friendship Level
            {
                message.Write(PlayerId);
                message.Write(FriendshipLevel);
            }
            else if (Action == 0x0B) // Search
            {
                message.Write(PlayerName);
            }
            else if (Action == 0x0C) // Badges
            {
            }
            else if (Action == 0x0D) // Change Badge Display
            {
                message.Write(BadgeId);
                message.Write(DisplayBadge);
            }
            else if (Action == 0x0F) // Friends Config
            {
            }
            else if (Action == 0x10) // Change Config
            {
                message.Write(ConfigId);
                message.Write(ShowCharacterInfo);
                message.Write(ShowAccountInfo);
                message.Write(AllowInspect);
            }
            else
            {
                throw new Exception($"Invalid Friend System Action: {Action}");
            }
        }
    }
}
