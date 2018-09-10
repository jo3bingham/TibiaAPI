﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EditList : ClientPacket
    {
        public string Text { get; set; }

        public uint WindowId { get; set; }

        public byte WindowType { get; set; }

        public EditList()
        {
            PacketType = ClientPacketType.EditList;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.EditList)
            {
                return false;
            }

            WindowType = message.ReadByte();
            WindowId = message.ReadUInt32();
            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EditList);
            message.Write(WindowType);
            message.Write(WindowId);
            message.Write(Text);
        }
    }
}
