using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UpdateExivaOptions : ClientPacket
    {
        public UpdateExivaOptions()
        {
            Type = ClientPacketType.UpdateExivaOptions];
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UpdateExivaOptions);
        }
    }
}
