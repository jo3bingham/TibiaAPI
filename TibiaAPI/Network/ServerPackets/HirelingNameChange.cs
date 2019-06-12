using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class HirelingNameChange : ServerPacket
    {
        public HirelingNameChange(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.HirelingNameChange;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.HirelingNameChange);
        }
    }
}
