using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EnterWorld : ClientPacket
    {
        public EnterWorld(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.EnterWorld;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.EnterWorld)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EnterWorld);
        }
    }
}
