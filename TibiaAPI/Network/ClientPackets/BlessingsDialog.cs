using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BlessingsDialog : ClientPacket
    {
        public BlessingsDialog()
        {
            Type = ClientPacketType.BlessingsDialog;
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
            message.Write((byte)ClientPacketType.BlessingsDialog);
        }
    }
}
