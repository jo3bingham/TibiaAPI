using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EditBuddy : ClientPacket
    {
        public List<byte> Groups { get; } = new List<byte>();

        public string Description { get; set; }

        public uint Icon { get; set; }
        public uint PlayerId { get; set; }

        public bool Notify { get; set; }

        public EditBuddy(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.EditBuddy;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
            Description = message.ReadString();
            Icon = message.ReadUInt32();
            Notify = message.ReadBool();
            Groups.Capacity = message.ReadByte();
            for (var i = 0; i < Groups.Capacity; ++i)
            {
                Groups.Add(message.ReadByte());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EditBuddy);
            message.Write(PlayerId);
            message.Write(Description);
            message.Write(Icon);
            message.Write(Notify);
            var count = Math.Min(Groups.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Groups[i]);
            }
        }
    }
}
