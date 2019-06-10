using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ClosedImbuingDialog : ClientPacket
    {
        public ClosedImbuingDialog(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.ClosedImbuingDialog;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ClosedImbuingDialog);
        }
    }
}
