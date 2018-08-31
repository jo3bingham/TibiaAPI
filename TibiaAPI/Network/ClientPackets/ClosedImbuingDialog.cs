using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ClosedImbuingDialog : ClientPacket
    {
        public ClosedImbuingDialog()
        {
            Type = ClientPacketType.ClosedImbuingDialog;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.ClosedImbuingDialog)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ClosedImbuingDialog);
        }
    }
}
