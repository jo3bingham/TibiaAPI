using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BlessingsDialog : ClientPacket
    {
        public BlessingsDialog()
        {
            PacketType = ClientPacketType.BlessingsDialog;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.BlessingsDialog)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BlessingsDialog);
        }
    }
}
