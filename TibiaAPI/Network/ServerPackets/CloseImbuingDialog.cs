using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseImbuingDialog : ServerPacket
    {
        public CloseImbuingDialog()
        {
            PacketType = ServerPacketType.CloseImbuingDialog;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CloseImbuingDialog)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseImbuingDialog);
        }
    }
}
