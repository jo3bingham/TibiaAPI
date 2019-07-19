using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PrivateChannel : ClientPacket
    {
        public string PlayerName { get; set; }

        public PrivateChannel(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PrivateChannel;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerName = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PrivateChannel);
            message.Write(PlayerName);
        }
    }
}
