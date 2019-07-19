using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LeaveParty : ClientPacket
    {
        public LeaveParty(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.LeaveParty;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LeaveParty);
        }
    }
}
