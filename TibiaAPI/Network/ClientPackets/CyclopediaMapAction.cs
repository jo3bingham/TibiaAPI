using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CyclopediaMapAction : ClientPacket
    {
        public CyclopediaMapAction()
        {
            PacketType = ClientPacketType.CyclopediaMapAction;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.CyclopediaMapAction)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CyclopediaMapAction);
        }
    }
}
