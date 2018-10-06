﻿using System;
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

        public EditBuddy()
        {
            PacketType = ClientPacketType.EditBuddy;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.EditBuddy)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            Description = message.ReadString();
            Icon = message.ReadUInt32();
            Notify = message.ReadBool();
            Groups.Capacity = message.ReadByte();
            for (var i = 0; i < Groups.Capacity; ++i)
            {
                Groups.Add(message.ReadByte());
            }
            return true;
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
