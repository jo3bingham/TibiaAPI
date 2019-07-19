using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyFreeListRerollAvailability : ServerPacket
    {
        public ushort TimeLeftUntilFreeListReroll { get; set; }

        public byte PreyArrayIndex { get; set; }

        public PreyFreeListRerollAvailability(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PreyFreeListRerollAvailability;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PreyArrayIndex = message.ReadByte();
            TimeLeftUntilFreeListReroll = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyFreeListRerollAvailability);
            message.Write(PreyArrayIndex);
            message.Write(TimeLeftUntilFreeListReroll);
        }
    }
}
