using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ApplyClearingCharm : ClientPacket
    {
        public byte Slot { get; set; }

        public ApplyClearingCharm()
        {
            PacketType = ClientPacketType.ApplyClearingCharm;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.ApplyClearingCharm)
            {
                return false;
            }

            Slot = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ApplyClearingCharm);
            message.Write(Slot);
        }
    }
}
