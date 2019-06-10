using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SeekInContainer : ClientPacket
    {
        public ushort Index { get; set; }

        public byte ContainerId { get; set; }

        public SeekInContainer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SeekInContainer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ContainerId = message.ReadByte();
            Index = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SeekInContainer);
            message.Write(ContainerId);
            message.Write(Index);
        }
    }
}
