using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UpContainer : ClientPacket
    {
        public byte ContainerId { get; set; }

        public UpContainer()
        {
            Type = ClientPacketType.UpContainer;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            ContainerId = message.ReadByte();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UpContainer);
            message.Write(ContainerId);
        }
    }
}
