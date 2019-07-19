using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UpContainer : ClientPacket
    {
        public byte ContainerId { get; set; }

        public UpContainer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.UpContainer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ContainerId = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UpContainer);
            message.Write(ContainerId);
        }
    }
}
