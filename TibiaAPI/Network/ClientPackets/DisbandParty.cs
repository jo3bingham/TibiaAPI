using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class DisbandParty : ClientPacket
    {
        public DisbandParty(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.DisbandParty;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.DisbandParty)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.DisbandParty);
        }
    }
}
