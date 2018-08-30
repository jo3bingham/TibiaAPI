using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CloseContainer : ClientPacket
    {
        public byte ContainerId { get; set; }

        public CloseContainer()
        {
            Type = ClientPacketType.CloseContainer;
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
            message.Write((byte)ClientPacketType.CloseContainer);
            message.Write(ContainerId);
        }
    }
}
