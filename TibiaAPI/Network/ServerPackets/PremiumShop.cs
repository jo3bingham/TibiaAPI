using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PremiumShop : ServerPacket
    {
        public PremiumShop(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.StoreCategories;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            var updateCreditBalance = message.ReadBool();
            if (updateCreditBalance)
            {
                var creditBalance = message.ReadInt32();
                var updatedCreditBalance = message.ReadInt32();
            }

            var count = message.ReadUInt16();
            while (count-- > 0)
            {
                message.ReadString();
                message.ReadString();
                message.ReadByte();
                var numberOfIconIdentifiers = message.ReadByte();
                while (numberOfIconIdentifiers-- > 0)
                {
                    message.ReadString();
                }
                message.ReadString();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // message.Write((byte)ServerPacketType.StoreCategories);
        }
    }
}
