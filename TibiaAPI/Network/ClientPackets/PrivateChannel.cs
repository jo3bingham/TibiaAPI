using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PrivateChannel : ClientPacket
    {
        public string PlayerName { get; set; }

        public PrivateChannel()
        {
            Type = ClientPacketType.PrivateChannel;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.PrivateChannel)
            {
                return false;
            }

            PlayerName = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PrivateChannel);
            message.Write(PlayerName);
        }
    }
}
