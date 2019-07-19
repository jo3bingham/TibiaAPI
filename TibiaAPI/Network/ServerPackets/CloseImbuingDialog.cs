using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseImbuingDialog : ServerPacket
    {
        public CloseImbuingDialog(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CloseImbuingDialog;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseImbuingDialog);
        }
    }
}
