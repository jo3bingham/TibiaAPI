using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SeekInContainer : ClientPacket
    {
        public ushort Index { get; set; }

        public byte ContainerId { get; set; }

        public SeekInContainer()
        {
            PacketType = ClientPacketType.SeekInContainer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.SeekInContainer)
            {
                return false;
            }

            ContainerId = message.ReadByte();
            Index = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SeekInContainer);
            message.Write(ContainerId);
            message.Write(Index);
        }
    }
}
