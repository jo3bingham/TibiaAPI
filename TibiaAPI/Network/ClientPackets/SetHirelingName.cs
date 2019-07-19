using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SetHirelingName : ClientPacket
    {
        public SetHirelingName(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SetHirelingName;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SetHirelingName);
        }
    }
}
