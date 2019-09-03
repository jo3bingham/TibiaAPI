using System;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FriendSystemData : ServerPacket
    {
        public byte Type { get; set; }

        public FriendSystemData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.FriendSystemData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO: Figure out Types 0x00 and 0x07 (if they exist),
            // and others (if there are any).
            Type = message.ReadByte();
            if (Type == 0x01) // New Invitations Pending
            {
            }
            if (Type == 0x02) // Friends
            {
                var count = message.ReadUInt16();
                for (var i = 0; i < count; ++i)
                {
                    var accountId = message.ReadUInt32();
                    var playerId = message.ReadUInt32();
                    var name = message.ReadString();
                    var unknown = message.ReadUInt16();
                    var world = message.ReadString();
                    var vocation = message.ReadString();
                    var level = message.ReadUInt16();
                    var outfit = message.ReadCreatureOutfit();
                    var friendshipLevel = message.ReadByte();
                    var timestamp = message.ReadUInt32();
                }
            }
            else if (Type == 0x03) // Invitations
            {
                var count = message.ReadUInt16();
                for (var i = 0; i < count; ++i)
                {
                    var unknown = message.ReadByte();
                    if (unknown == 1)
                    {
                        var unknown1 = message.ReadUInt32();
                    }
                    var invitedName = message.ReadString();
                    var unknown2 = message.ReadUInt16();
                    var inviteeName = message.ReadString();
                    var timestamp = message.ReadUInt32();
                }
            }
            else if (Type == 0x04) // Blacklist
            {
                var count = message.ReadUInt16();
                for (var i = 0; i < count; ++i)
                {
                    var accountId = message.ReadUInt32();
                    var playerName = message.ReadString();
                    var timestamp = message.ReadUInt32();
                }
            }
            else if (Type == 0x05) // Character Info
            {
                var accountId = message.ReadUInt32();
                var unknown = message.ReadUInt16();
                var isFriend = message.ReadBool();
                if (isFriend)
                {
                    var unknown1 = message.ReadByte();
                    var lastGameLogin = message.ReadUInt32();
                    var unknown2 = message.ReadBytes(3);
                    var badges = message.ReadByte();
                    for (var i = 0; i < badges; ++i)
                    {
                        var badgeId = message.ReadUInt32();
                        var badgeName = message.ReadUInt32();
                    }
                }
                var characters = message.ReadByte();
                for (var i = 0; i < characters; ++i)
                {
                    var playerId = message.ReadUInt32();
                    var name = message.ReadString();
                    var isMainCharacter = message.ReadBool();
                    var unknown3 = message.ReadUInt16();
                    var world = message.ReadString();
                    var vocation = message.ReadString();
                    var level = message.ReadUInt16();
                    var unknown4 = message.ReadByte();
                    var outfit = message.ReadCreatureOutfit();
                }
            }
            else if (Type == 0x06) // Badges
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
            else if (Type == 0x08) // Config
            {
                var count = message.ReadByte();
                for (var i = 0; i < count; ++i)
                {
                    var configId = message.ReadByte();
                    var showCharacterInfo = message.ReadBool();
                    var showAccountInfo = message.ReadBool();
                    var allowInspect = message.ReadBool();
                }
            }
            else
            {
                throw new Exception($"Invalid Friend System Data: {Type}");
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.FriendSystemData);
        }
    }
}
