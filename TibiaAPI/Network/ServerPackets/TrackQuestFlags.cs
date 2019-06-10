using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TrackQuestflags : ServerPacket
    {
        public List<(ushort Id, string ParentQuestName, string Name, string Description)> Questflags { get; } =
            new List<(ushort Id, string ParentQuestName, string Name, string Description)>();

        public string Name { get; set; }
        public string Description { get; set; }

        public ushort Id { get; set; }

        public byte AvailableTrackingSlots { get; set; }

        public bool IsQuestflags { get; set; }

        public TrackQuestflags(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TrackQuestflags;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            IsQuestflags = message.ReadBool();
            if (IsQuestflags)
            {
                AvailableTrackingSlots = message.ReadByte();
                Questflags.Capacity = message.ReadByte();
                for (var i = 0; i < Questflags.Capacity; ++i)
                {
                    var id = message.ReadUInt16();
                    var parentQuestName = message.ReadString();
                    var name = message.ReadString();
                    var description = message.ReadString();
                    Questflags.Add((id, parentQuestName, name, description));
                }
            }
            else
            {
                Id = message.ReadUInt16();
                Name = message.ReadString();
                Description = message.ReadString();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TrackQuestflags);
            message.Write(IsQuestflags);
            if (IsQuestflags)
            {
                message.Write(AvailableTrackingSlots);
                var count = Math.Min(Questflags.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    var quest = Questflags[i];
                    message.Write(quest.Id);
                    message.Write(quest.ParentQuestName);
                    message.Write(quest.Name);
                    message.Write(quest.Description);
                }
            }
            else
            {
                message.Write(Id);
                message.Write(Name);
                message.Write(Description);
            }
        }
    }
}
