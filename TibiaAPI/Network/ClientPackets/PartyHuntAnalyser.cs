using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PartyHuntAnalyser : ClientPacket
    {
        public PartyHuntAnalyser(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PartyHuntAnalyser;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.PartyHuntAnalyser);
        }
    }
}
