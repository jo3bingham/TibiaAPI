using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class UpdateExivaOptions : ServerPacket
    {
        public List<string> WhitelistCharacters { get; } = new List<string>();
        public List<string> WhitelistGuilds { get; } = new List<string>();

        public ushort UnknownUShort1 { get; set; }
        public ushort UnknownUShort2 { get; set; }

        public bool AllowAllCharacters { get; set; }
        public bool AllowCharacterWhitelist { get; set; }
        public bool AllowGuildWhitelist { get; set; }
        public bool AllowMyGuildMembers { get; set; }
        public bool AllowMyPartyMembers { get; set; }
        public bool AllowMyVips { get; set; }

        public UpdateExivaOptions()
        {
            PacketType = ServerPacketType.UpdateExivaOptions;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.UpdateExivaOptions)
            {
                return false;
            }

            AllowAllCharacters = message.ReadBool();
            AllowMyGuildMembers = message.ReadBool();
            AllowMyPartyMembers = message.ReadBool();
            AllowMyVips = message.ReadBool();
            AllowCharacterWhitelist = message.ReadBool();
            AllowGuildWhitelist = message.ReadBool();

            WhitelistCharacters.Capacity = message.ReadUInt16();
            for (var i = 0; i < WhitelistCharacters.Capacity; ++i)
            {
                WhitelistCharacters.Add(message.ReadString());
            }

            UnknownUShort1 = message.ReadUInt16();

            WhitelistGuilds.Capacity = message.ReadUInt16();
            for (var i = 0; i < WhitelistGuilds.Capacity; ++i)
            {
                WhitelistGuilds.Add(message.ReadString());
            }

            UnknownUShort2 = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.UpdateExivaOptions);
            message.Write(AllowAllCharacters);
            message.Write(AllowMyGuildMembers);
            message.Write(AllowMyPartyMembers);
            message.Write(AllowMyVips);
            message.Write(AllowCharacterWhitelist);
            message.Write(AllowGuildWhitelist);

            var count = (ushort)Math.Min(WhitelistCharacters.Count, ushort.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(WhitelistCharacters[i]);
            }

            message.Write(UnknownUShort1);

            count = (ushort)Math.Min(WhitelistGuilds.Count, ushort.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(WhitelistGuilds[i]);
            }

            message.Write(UnknownUShort2);
        }
    }
}
