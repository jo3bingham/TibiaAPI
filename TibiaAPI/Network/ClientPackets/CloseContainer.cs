using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CloseContainer : ClientPacket
    {
        public byte ContainerId { get; set; }

        public CloseContainer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.CloseContainer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ContainerId = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CloseContainer);
            message.Write(ContainerId);
        }
    }
}
