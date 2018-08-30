using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SeekInContainer : ClientPacket
    {
        public ushort Index { get; set; }

        public byte ContainerId { get; set; }

        public SeekInContainer()
        {
            Type = ClientPacketType.SeekInContainer;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            ContainerId = message.ReadByte();
            Index = message.ReadUInt16();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SeekInContainer);
            message.Write(ContainerId);
            message.Write(Index);
        }
    }
}
