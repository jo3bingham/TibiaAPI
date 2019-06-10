using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ScreenshotEvent : ServerPacket
    {
        public byte Type { get; set; }

        public ScreenshotEvent(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ScreenshotEvent;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO: Figure out all screenshot types.
            Type = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ScreenshotEvent);
            message.Write(Type);
        }
    }
}
