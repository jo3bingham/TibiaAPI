using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CyclopediaMapAction : ClientPacket
    {
        public CyclopediaMapAction()
        {
            Type = ClientPacketType.CyclopediaMapAction;
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
            message.Write((byte)ClientPacketType.CyclopediaMapAction);
        }
    }
}
