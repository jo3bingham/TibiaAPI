﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class EditText : ServerPacket
    {
        public string Author { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }

        public uint WindowId { get; set; }

        public ushort MaxTextLength { get; set; }

        public EditText()
        {
            PacketType = ServerPacketType.EditText;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.EditText)
            {
                return false;
            }

            WindowId = message.ReadUInt32();
            // TODO
            //message.ReadObjectInstance();
            MaxTextLength = message.ReadUInt16();
            Text = message.ReadString();
            Author = message.ReadString();
            Date = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.EditText);
            message.Write(WindowId);
            // TODO
            //message.WriteObjectInstance(Window);
            message.Write(MaxTextLength);
            message.Write(Text);
            message.Write(Author);
            message.Write(Date);
        }
    }
}
