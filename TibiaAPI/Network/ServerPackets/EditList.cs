using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class EditList : ServerPacket
    {
        public string Text { get; set; }

        public uint WindowId { get; set; }

        public byte WindowType { get; set; }

        public EditList()
        {
            PacketType = ServerPacketType.EditList;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.EditList)
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
            message.Write((byte)ServerPacketType.EditList);
            message.Write(WindowType);
            message.Write(WindowId);
            message.Write(Text);
        }
    }
}
