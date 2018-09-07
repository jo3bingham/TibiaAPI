using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CloseContainer : ClientPacket
    {
        public byte ContainerId { get; set; }

        public CloseContainer()
        {
            PacketType = ClientPacketType.CloseContainer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.CloseContainer)
            {
                return false;
            }

            ContainerId = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CloseContainer);
            message.Write(ContainerId);
        }
    }
}
