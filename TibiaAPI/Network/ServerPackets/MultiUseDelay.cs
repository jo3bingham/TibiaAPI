using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MultiUseDelay : ServerPacket
    {
        public uint Delay { get; set; }

        public MultiUseDelay(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MultiUseDelay;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Delay = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MultiUseDelay);
            message.Write(Delay);
        }
    }
}
