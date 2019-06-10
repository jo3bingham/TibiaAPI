using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CyclopediaMapAction : ClientPacket
    {
        public CyclopediaMapAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.CyclopediaMapAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CyclopediaMapAction);
        }
    }
}
