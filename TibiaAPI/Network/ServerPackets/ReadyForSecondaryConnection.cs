using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ReadyForSecondaryConnection : ServerPacket
    {
        public string Text { get; set; }

        public ReadyForSecondaryConnection(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ReadyForSecondaryConnection;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ReadyForSecondaryConnection);
            message.Write(Text);
        }
    }
}
