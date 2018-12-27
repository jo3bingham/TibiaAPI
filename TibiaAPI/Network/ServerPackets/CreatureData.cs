using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureData : ServerPacket
    {
        public CreatureData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureData;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreatureData)
            {
                return false;
            }

            // This packet type appears in the Tibia 11 client, but not the Tibia 10, or Flash, client.
            // Structure is currently unknown (assuming it actually exists/is used).
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureData);
        }
    }
}
