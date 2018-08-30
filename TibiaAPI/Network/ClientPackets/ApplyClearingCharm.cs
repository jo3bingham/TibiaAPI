using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ApplyClearingCharm : ClientPacket
    {
        public byte Slot { get; set; }

        public ApplyClearingCharm()
        {
            Type = ClientPacketType.ApplyClearingCharm;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            Slot = message.ReadByte();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ApplyClearingCharm);
            message.Write(Slot);
        }
    }
}
