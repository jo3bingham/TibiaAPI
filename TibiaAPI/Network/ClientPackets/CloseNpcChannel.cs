using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CloseNpcChannel : ClientPacket
    {
        public CloseNpcChannel()
        {
            PacketType = ClientPacketType.CloseNpcChannel;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.CloseNpcChannel)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CloseNpcChannel);
        }
    }
}
