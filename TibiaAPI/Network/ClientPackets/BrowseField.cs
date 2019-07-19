using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BrowseField : ClientPacket
    {
        public Position Position { get; set; }

        public BrowseField(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.BrowseField;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BrowseField);
            message.Write(Position);
        }
    }
}
