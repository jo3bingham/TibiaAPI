using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenParentContainer : ClientPacket
    {
        public Position Position { get; set; }

        public OpenParentContainer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenParentContainer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenParentContainer);
            message.Write(Position);
        }
    }
}
