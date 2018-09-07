using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Wait : ServerPacket
    {
        public ushort Delay { get; set; }

        public Wait()
        {
            PacketType = ServerPacketType.Wait;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Wait)
            {
                return false;
            }

            Delay = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Wait);
            message.Write(Delay);
        }
    }
}
