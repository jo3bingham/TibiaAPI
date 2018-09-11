using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyFreeListRerollAvailability : ServerPacket
    {
        public ushort TimeLeftUntilFreeListReroll { get; set; }

        public byte PreyArrayIndex { get; set; }

        public PreyFreeListRerollAvailability()
        {
            PacketType = ServerPacketType.PreyFreeListRerollAvailability;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PreyFreeListRerollAvailability)
            {
                return false;
            }

            PreyArrayIndex = message.ReadByte();
            TimeLeftUntilFreeListReroll = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyFreeListRerollAvailability);
            message.Write(PreyArrayIndex);
            message.Write(TimeLeftUntilFreeListReroll);
        }
    }
}
