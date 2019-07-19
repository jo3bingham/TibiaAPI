using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class StoreCategories : ServerPacket
    {
        public List<(string Name, byte HighlightState, List<string> Icons, string ParentName)> Categories { get; } =
            new List<(string Name, byte HighlightState, List<string> Icons, string ParentName)>();

        public StoreCategories(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.StoreCategories;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Categories.Capacity = message.ReadUInt16();
            for (var i = 0; i < Categories.Capacity; ++i)
            {
                var name = message.ReadString();
                var highlightState = message.ReadByte();

                var icons = new List<string>(message.ReadByte());
                for (var j = 0; j < icons.Capacity; ++j)
                {
                    icons.Add(message.ReadString());
                }

                var parentName = message.ReadString();

                Categories.Add((name, highlightState, icons, parentName));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.StoreCategories);

            var count = Math.Min(Categories.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (Name, HighlightState, Icons, ParentName) = Categories[i];
                message.Write(Name);
                message.Write(HighlightState);

                var size = Math.Min(Icons.Count, byte.MaxValue);
                message.Write((byte)size);
                for (var j = 0; j < size; ++j)
                {
                    message.Write(Icons[j]);
                }

                message.Write(ParentName);
            }
        }
    }
}
