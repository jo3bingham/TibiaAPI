using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ClosedImbuingDialog : ClientPacket
    {
        public ClosedImbuingDialog()
        {
            Type = ClientPacketType.ClosedImbuingDialog;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ClosedImbuingDialog);
        }
    }
}
