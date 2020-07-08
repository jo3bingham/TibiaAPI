using System;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FriendSystemData : ServerPacket
    {
        public FriendSystemDataType DataType { get; set; }

        public FriendSystemData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.FriendSystemData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO: Figure out others DataTypes (if there are any).
            // 0x09 and above seem to have no purpose.
            DataType = (FriendSystemDataType)message.ReadByte();
            if (DataType == FriendSystemDataType.SpecialEvent)
            {
                var eventType = message.ReadByte();
                if (eventType == 0)
                {
                    // Error: You need to set a main character to use this feature. [Cancel]
                }
                else if (eventType == 1)
                {
                    // Error: text [Ok]
                    var text = message.ReadString();
                }
                else if (eventType == 2)
                {
                    // Display "No results." in the search tab
                }
                else if (eventType >= 3)
                {
                    // Clears the search tab and shows the default text if no results are displayed
                    // Enables the Invite & Blacklist button if results are displayed
                }
            }
            else if (DataType == FriendSystemDataType.InvitationPending)
            {
            }
            else if (DataType == FriendSystemDataType.Friends)
            {
                var count = message.ReadUInt16();
                for (var i = 0; i < count; ++i)
                {
                    var accountId = message.ReadUInt32();
                    var playerId = message.ReadUInt32();
                    var name = message.ReadString();
                    var title = message.ReadString();
                    var world = message.ReadString();
                    var vocation = message.ReadString();
                    var level = message.ReadUInt16();
                    var outfit = message.ReadCreatureOutfit();
                    var friendshipLevel = (FriendGroup)message.ReadByte();
                    var timestamp = message.ReadUInt32();
                }
            }
            else if (DataType == FriendSystemDataType.Invitations)
            {
                var count = message.ReadUInt16();
                for (var i = 0; i < count; ++i)
                {
                    var isSentInvitation = message.ReadBool();
                    var accountId = message.ReadUInt32();
                    var invitedName = message.ReadString();
                    var invitedTitle = message.ReadString();
                    var inviteeName = message.ReadString();
                    var timestamp = message.ReadUInt32();
                }
            }
            else if (DataType == FriendSystemDataType.Blacklist)
            {
                var count = message.ReadUInt16();
                for (var i = 0; i < count; ++i)
                {
                    var accountId = message.ReadUInt32();
                    var playerName = message.ReadString();
                    var timestamp = message.ReadUInt32();
                }
            }
            else if (DataType == FriendSystemDataType.CharacterSearch)
            {
                var accountId = message.ReadUInt32();
                var hasPendingInvitation = message.ReadBool();
                var isBlacklisted = message.ReadBool();
                var isFriend = message.ReadBool();
                if (isFriend)
                {
                    var isOnline = message.ReadBool();
                    var lastGameLogin = message.ReadUInt32();
                    var isPremium = message.ReadBool();
                    var loyalityTitle = message.ReadString();
                    var badges = message.ReadByte();
                    for (var i = 0; i < badges; ++i)
                    {
                        var badgeId = message.ReadUInt32();
                        var badgeName = message.ReadString();
                    }
                }
                var characters = message.ReadByte();
                for (var i = 0; i < characters; ++i)
                {
                    var playerId = message.ReadUInt32();
                    var name = message.ReadString();
                    var isMainCharacter = message.ReadBool();
                    var title = message.ReadString();
                    var world = message.ReadString();
                    var vocation = message.ReadString();
                    var level = message.ReadUInt16();
                    var isOnline = message.ReadBool();
                    var outfit = message.ReadCreatureOutfit();
                }
            }
            else if (DataType == FriendSystemDataType.Badges)
            {
                var count = message.ReadByte();
                for (var i = 0; i < count; ++i)
                {
                    var id = message.ReadUInt32();
                    var name = message.ReadString();
                    var description = message.ReadString();
                    var isUnlocked = message.ReadBool();
                    var displayBadge = message.ReadBool();
                }
            }
            else if (DataType == FriendSystemDataType.NewFriend)
            {
            }
            else if (DataType == FriendSystemDataType.Config)
            {
                var count = message.ReadByte();
                for (var i = 0; i < count; ++i)
                {
                    var configId = (FriendGroup)message.ReadByte();
                    var showCharacterInfo = message.ReadBool();
                    var showAccountInfo = message.ReadBool();
                    var allowInspect = message.ReadBool();
                }
            }
            else
            {
                throw new Exception($"Invalid Friend System Data: {DataType}");
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.FriendSystemData);
            // message.Write((byte)DataType);
        }
    }
}
