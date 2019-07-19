using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseContainer : ServerPacket
    {
        public byte ContainerId { get; set; }

        public CloseContainer(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CloseContainer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ContainerId = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseContainer);
            message.Write(ContainerId);
        }
    }
}
