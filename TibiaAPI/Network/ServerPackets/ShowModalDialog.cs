using System;
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

        public ShowModalDialog(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ShowModalDialog;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
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
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ShowModalDialog);
            message.Write(Id);
            message.Write(Title);
            message.Write(Text);

            var count = Math.Min(FirstChoices.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var choice = FirstChoices[i];
                message.Write(choice.Label);
                message.Write(choice.Value);
            }

            count = Math.Min(SecondChoices.Count, byte.MaxValue);
            message.Write((byte)count);
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
