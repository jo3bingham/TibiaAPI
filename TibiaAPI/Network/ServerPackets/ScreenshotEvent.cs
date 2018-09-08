using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ScreenshotEvent : ServerPacket
    {
        public ScreenshotEvent()
        {
            PacketType = ServerPacketType.ScreenshotEvent;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ScreenshotEvent)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ScreenshotEvent);
        }
    }
}
