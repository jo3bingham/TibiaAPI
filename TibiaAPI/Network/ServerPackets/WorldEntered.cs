using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class WorldEntered : ServerPacket
    {
        public WorldEntered(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.WorldEntered;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.WorldEntered)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.WorldEntered);
        }
    }
}
