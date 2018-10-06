using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ScreenshotEvent : ServerPacket
    {
        public byte Type { get; set; }

        public ScreenshotEvent()
        {
            PacketType = ServerPacketType.ScreenshotEvent;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ScreenshotEvent)
            {
                return false;
            }

            // TODO: Figure out all screenshot types.
            Type = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ScreenshotEvent);
            message.Write(Type);
        }
    }
}
