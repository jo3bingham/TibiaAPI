using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MultiUseDelay : ServerPacket
    {
        public uint Delay { get; set; }

        public MultiUseDelay()
        {
            PacketType = ServerPacketType.MultiUseDelay;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MultiUseDelay)
            {
                return false;
            }

            Delay = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MultiUseDelay);
            message.Write(Delay);
        }
    }
}
