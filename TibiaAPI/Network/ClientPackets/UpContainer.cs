using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UpContainer : ClientPacket
    {
        public byte ContainerId { get; set; }

        public UpContainer()
        {
            PacketType = ClientPacketType.UpContainer;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.UpContainer)
            {
                return false;
            }

            ContainerId = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UpContainer);
            message.Write(ContainerId);
        }
    }
}
