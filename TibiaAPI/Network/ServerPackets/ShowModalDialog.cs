﻿using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ShowModalDialog : ServerPacket
    {
        public List<(string Label, byte Value)> FirstChoices { get; } = new List<(string Label, byte Value)>();
        public List<(string Label, byte Value)> SecondChoices { get; } = new List<(string Label, byte Value)>();

        public string Text { get; set; }
        public string Title { get; set; }

        public uint Id { get; set; }

        public byte DefaultEnterButtonId { get; set; }
        public byte DefaultEscapeButtonId { get; set; }

        public bool HasPriority { get; set; }

        public ShowModalDialog()
        {
            PacketType = ServerPacketType.ShowModalDialog;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ShowModalDialog)
            {
                return false;
            }

            Id = message.ReadUInt32();
            Title = message.ReadString();
            Text = message.ReadString();

            FirstChoices.Capacity = message.ReadByte();
            for (var i = 0; i < FirstChoices.Capacity; ++i)
            {
                var label = message.ReadString();
                var value = message.ReadByte();
                FirstChoices.Add((label, value));
            }

            SecondChoices.Capacity = message.ReadByte();
            for (var i = 0; i < SecondChoices.Capacity; ++i)
            {
                var label = message.ReadString();
                var value = message.ReadByte();
                SecondChoices.Add((label, value));
            }

            DefaultEscapeButtonId = message.ReadByte();
            DefaultEnterButtonId = message.ReadByte();
            HasPriority = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ShowModalDialog);
            message.Write(Id);
            message.Write(Title);
            message.Write(Text);

            var count = (byte)Math.Min(FirstChoices.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var choice = FirstChoices[i];
                message.Write(choice.Label);
                message.Write(choice.Value);
            }

            count = (byte)Math.Min(SecondChoices.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var choice = SecondChoices[i];
                message.Write(choice.Label);
                message.Write(choice.Value);
            }

            message.Write(DefaultEscapeButtonId);
            message.Write(DefaultEnterButtonId);
            message.Write(HasPriority);
        }
    }
}
