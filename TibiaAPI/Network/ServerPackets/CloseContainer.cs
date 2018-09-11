using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseContainer : ServerPacket
    {
        public byte ContainerId { get; set; }

        public CloseContainer()
        {
            PacketType = ServerPacketType.CloseContainer;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CloseContainer)
            {
                return false;
            }

            ContainerId = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseContainer);
            message.Write(ContainerId);
        }
    }
}
